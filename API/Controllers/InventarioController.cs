using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class InventarioController : BaseController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public InventarioController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<InventarioDto>>> Get()
    {
        var result = await _unitOfWork.Inventarios.GetAllAsync();
        return _mapper.Map<List<InventarioDto>>(result);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<InventarioDto>> Get(string id)
    {
        var result = await _unitOfWork.Inventarios.GetByIdAsync(id);
        if (result == null)
        {
            return NotFound();
        }
        return _mapper.Map<InventarioDto>(result);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<InventarioDto>> Post(InventarioDto resultDto)
    {
        var result = _mapper.Map<Inventario>(resultDto);
        _unitOfWork.Inventarios.Add(result);
        await _unitOfWork.SaveAsync();
        if (result == null)
        {
            return BadRequest();
        }
        resultDto.Id = result.Id;
        return CreatedAtAction(nameof(Post), new { id = resultDto.Id }, resultDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<InventarioDto>> Put(string id, [FromBody] InventarioDto resultDto)
    {
        if (resultDto.Id == null)
        {
            resultDto.Id = id;
        }
        if (resultDto.Id != id)
        {
            return NotFound();
        }
        var result = _mapper.Map<Inventario>(resultDto);
        resultDto.Id = result.Id;
        _unitOfWork.Inventarios.Update(result);
        await _unitOfWork.SaveAsync();
        return resultDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(string id)
    {
        var result = await _unitOfWork.Inventarios.GetByIdAsync(id);
        if (result == null)
        {
            return NotFound();
        }
        _unitOfWork.Inventarios.Remove(result);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
}