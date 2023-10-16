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

public class MovimientoInventarioController : BaseController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public MovimientoInventarioController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<MovimientoInventarioDto>>> Get()
    {
        var result = await _unitOfWork.MovimientoInventarios.GetAllAsync();
        return _mapper.Map<List<MovimientoInventarioDto>>(result);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<MovimientoInventarioDto>> Get(string id)
    {
        var result = await _unitOfWork.MovimientoInventarios.GetByIdAsync(id);
        if (result == null)
        {
            return NotFound();
        }
        return _mapper.Map<MovimientoInventarioDto>(result);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<MovimientoInventarioDto>> Post(MovimientoInventarioDto resultDto)
    {
        var result = _mapper.Map<MovimientoInventario>(resultDto);
        _unitOfWork.MovimientoInventarios.Add(result);
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
    public async Task<ActionResult<MovimientoInventarioDto>> Put(string id, [FromBody] MovimientoInventarioDto resultDto)
    {
        if (resultDto.Id == null)
        {
            resultDto.Id = id;
        }
        if (resultDto.Id != id)
        {
            return NotFound();
        }
        var result = _mapper.Map<MovimientoInventario>(resultDto);
        resultDto.Id = result.Id;
        _unitOfWork.MovimientoInventarios.Update(result);
        await _unitOfWork.SaveAsync();
        return resultDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(string id)
    {
        var result = await _unitOfWork.MovimientoInventarios.GetByIdAsync(id);
        if (result == null)
        {
            return NotFound();
        }
        _unitOfWork.MovimientoInventarios.Remove(result);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
}