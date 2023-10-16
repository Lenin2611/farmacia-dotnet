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

public class PersonaController : BaseController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public PersonaController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<PersonaDto>>> Get()
    {
        var result = await _unitOfWork.Personas.GetAllAsync();
        return _mapper.Map<List<PersonaDto>>(result);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PersonaDto>> Get(string id)
    {
        var result = await _unitOfWork.Personas.GetByIdAsync(id);
        if (result == null)
        {
            return NotFound();
        }
        return _mapper.Map<PersonaDto>(result);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PersonaDto>> Post(PersonaDto resultDto)
    {
        var result = _mapper.Map<Persona>(resultDto);
        if (resultDto.FechaRegistroPersona == DateOnly.MinValue)
        {
            resultDto.FechaRegistroPersona = DateOnly.FromDateTime(DateTime.Now);
            result.FechaRegistroPersona = DateOnly.FromDateTime(DateTime.Now);
        }
        _unitOfWork.Personas.Add(result);
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
    public async Task<ActionResult<PersonaDto>> Put(string id, [FromBody] PersonaDto resultDto)
    {
        if (resultDto.Id == null)
        {
            resultDto.Id = id;
        }
        if (resultDto.Id != id)
        {
            return NotFound();
        }
        var result = _mapper.Map<Persona>(resultDto);
        if (resultDto.FechaRegistroPersona == DateOnly.MinValue)
        {
            resultDto.FechaRegistroPersona = DateOnly.FromDateTime(DateTime.Now);
            result.FechaRegistroPersona = DateOnly.FromDateTime(DateTime.Now);
        }
        resultDto.Id = result.Id;
        _unitOfWork.Personas.Update(result);
        await _unitOfWork.SaveAsync();
        return resultDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(string id)
    {
        var result = await _unitOfWork.Personas.GetByIdAsync(id);
        if (result == null)
        {
            return NotFound();
        }
        _unitOfWork.Personas.Remove(result);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
}