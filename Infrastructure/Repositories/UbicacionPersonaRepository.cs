using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UbicacionPersonaRepository : GenericRepository<UbicacionPersona>,IUbicacionPersonaRepository
{
    private readonly FarmaciaCampusContext _context;

    public UbicacionPersonaRepository(FarmaciaCampusContext context) : base(context)
    {
        _context = context;
    }

    public override async Task<IEnumerable<UbicacionPersona>> GetAllAsync()
    {
        return await _context.UbicacionPersonas.ToListAsync();
    }

    public override async Task<(int totalRegistros, IEnumerable<UbicacionPersona> registros)> GetAllAsync(
        int pageIndex,
        int pageSize,
        string search
    )
    {
        var query = _context.UbicacionPersonas as IQueryable<UbicacionPersona>;
    
        if (!string.IsNullOrEmpty(search))
        {
            query = query.Where(p => p.TipoVia.ToLower().Contains(search)); // If necesary add .ToString() after varQuery
        }
        query = query.OrderBy(p => p.Id);
    
        var totalRegistros = await query.CountAsync();
        var registros = await query
                        .Skip((pageIndex - 1) * pageSize)
                        .Take(pageSize)
                        .ToListAsync();
        return (totalRegistros, registros);
    }
}