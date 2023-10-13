using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class RolPersonaRepository : GenericRepository<RolPersona>,IRolPersonaRepository
{
    private readonly FarmaciaCampusContext _context;

    public RolPersonaRepository(FarmaciaCampusContext context) : base(context)
    {
        _context = context;
    }

    public override async Task<IEnumerable<RolPersona>> GetAllAsync()
    {
        return await _context.RolPersonas
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

    public override async Task<(int totalRegistros, IEnumerable<RolPersona> registros)> GetAllAsync(
        int pageIndex,
        int pageSize,
        string search
    )
    {
        var query = _context.RolPersonas as IQueryable<RolPersona>;
    
        if (!string.IsNullOrEmpty(search))
        {
            query = query.Where(p => p.NombreRolPersona.ToString().ToLower().Contains(search)); // If necesary add .ToString() after varQuery
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
