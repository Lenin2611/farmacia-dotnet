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

public class TipoContactoController : BaseController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public TipoContactoController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<TipoContactoDto>>> Get()
    {
        var result = await _unitOfWork.TipoContactos.GetAllAsync();
        return _mapper.Map<List<TipoContactoDto>>(result);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TipoContactoDto>> Get(int id)
    {
        var result = await _unitOfWork.TipoContactos.GetByIdAsync(id);
        if (result == null)
        {
            return NotFound();
        }
        return _mapper.Map<TipoContactoDto>(result);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<TipoContactoDto>> Post(TipoContactoDto resultDto)
    {
        var result = _mapper.Map<TipoContacto>(resultDto);
        _unitOfWork.TipoContactos.Add(result);
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
    public async Task<ActionResult<TipoContactoDto>> Put(int id, [FromBody] TipoContactoDto resultDto)
    {
        if (resultDto.Id == 0)
        {
            resultDto.Id = id;
        }
        if (resultDto.Id != id)
        {
            return NotFound();
        }
        var result = _mapper.Map<TipoContacto>(resultDto);
        resultDto.Id = result.Id;
        _unitOfWork.TipoContactos.Update(result);
        await _unitOfWork.SaveAsync();
        return resultDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _unitOfWork.TipoContactos.GetByIdAsync(id);
        if (result == null)
        {
            return NotFound();
        }
        _unitOfWork.TipoContactos.Remove(result);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
}