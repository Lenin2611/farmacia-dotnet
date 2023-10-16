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

public class DetalleMovimientoInventarioController : BaseController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public DetalleMovimientoInventarioController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<DetalleMovimientoInventarioDto>>> Get()
    {
        var result = await _unitOfWork.DetalleMovimientoInventarios.GetAllAsync();
        return _mapper.Map<List<DetalleMovimientoInventarioDto>>(result);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<DetalleMovimientoInventarioDto>> Get(int id)
    {
        var result = await _unitOfWork.DetalleMovimientoInventarios.GetByIdAsync(id);
        if (result == null)
        {
            return NotFound();
        }
        return _mapper.Map<DetalleMovimientoInventarioDto>(result);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<DetalleMovimientoInventarioDto>> Post(DetalleMovimientoInventarioDto resultDto)
    {
        var result = _mapper.Map<DetalleMovimientoInventario>(resultDto);
        _unitOfWork.DetalleMovimientoInventarios.Add(result);
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
    public async Task<ActionResult<DetalleMovimientoInventarioDto>> Put(int id, [FromBody] DetalleMovimientoInventarioDto resultDto)
    {
        if (resultDto.Id == 0)
        {
            resultDto.Id = id;
        }
        if (resultDto.Id != id)
        {
            return NotFound();
        }
        var result = _mapper.Map<DetalleMovimientoInventario>(resultDto);
        resultDto.Id = result.Id;
        _unitOfWork.DetalleMovimientoInventarios.Update(result);
        await _unitOfWork.SaveAsync();
        return resultDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _unitOfWork.DetalleMovimientoInventarios.GetByIdAsync(id);
        if (result == null)
        {
            return NotFound();
        }
        _unitOfWork.DetalleMovimientoInventarios.Remove(result);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
}