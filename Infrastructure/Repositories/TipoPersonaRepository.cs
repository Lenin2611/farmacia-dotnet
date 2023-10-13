using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class TipoPersonaRepository : GenericRepository<TipoPersona>,ITipoPersonaRepository
{
    private readonly FarmaciaCampusContext _context;

    public TipoPersonaRepository(FarmaciaCampusContext context) : base(context)
    {
        _context = context;
    }

    public override async Task<IEnumerable<TipoPersona>> GetAllAsync()
    {
        return await _context.TipoPersonas
                    .Include(c => c.Personas)
                    .ThenInclude(c => c.ContactoPersonas)
                    .Include(c => c.Personas)
                    .ThenInclude(x => x.MovimientoInventarios)
                    .ThenInclude(c => c.DetalleMovimientoInventarios)
                    .ThenInclude(c => c.Facturas)
                    .Include(c => c.Personas)
                    .ThenInclude(x => x.Facturas)
                    .Include(c => c.Personas)
                    .ThenInclude(x => x.UbicacionPersonas)
                    .ToListAsync();
    }

    public override async Task<(int totalRegistros, IEnumerable<TipoPersona> registros)> GetAllAsync(
        int pageIndex,
        int pageSize,
        string search
    )
    {
        var query = _context.TipoPersonas as IQueryable<TipoPersona>;
    
        if (!string.IsNullOrEmpty(search))
        {
            query = query.Where(p => p.NombreTipoPersona.ToString().ToLower().Contains(search)); // If necesary add .ToString() after varQuery
        }
        query = query.OrderBy(p => p.Id);
    
        var totalRegistros = await query.CountAsync();
        var registros = await query
                        .Include(c => c.Personas)
                        .Skip((pageIndex - 1) * pageSize)
                        .Take(pageSize)
                        .ToListAsync();
        return (totalRegistros, registros);
    }
}