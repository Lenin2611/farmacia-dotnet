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

public class MarcaController : BaseController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public MarcaController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<MarcaDto>>> Get()
    {
        var result = await _unitOfWork.Marcas.GetAllAsync();
        return _mapper.Map<List<MarcaDto>>(result);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<MarcaDto>> Get(int id)
    {
        var result = await _unitOfWork.Marcas.GetByIdAsync(id);
        if (result == null)
        {
            return NotFound();
        }
        return _mapper.Map<MarcaDto>(result);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<MarcaDto>> Post(MarcaDto resultDto)
    {
        var result = _mapper.Map<Marca>(resultDto);
        _unitOfWork.Marcas.Add(result);
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
    public async Task<ActionResult<MarcaDto>> Put(int id, [FromBody] MarcaDto resultDto)
    {
        if (resultDto.Id == 0)
        {
            resultDto.Id = id;
        }
        if (resultDto.Id != id)
        {
            return NotFound();
        }
        var result = _mapper.Map<Marca>(resultDto);
        resultDto.Id = result.Id;
        _unitOfWork.Marcas.Update(result);
        await _unitOfWork.SaveAsync();
        return resultDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _unitOfWork.Marcas.GetByIdAsync(id);
        if (result == null)
        {
            return NotFound();
        }
        _unitOfWork.Marcas.Remove(result);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
}