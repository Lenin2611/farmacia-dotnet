using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class PresentacionRepository : GenericRepository<Presentacion>,IPresentacionRepository
{
    private readonly FarmaciaCampusContext _context;

    public PresentacionRepository(FarmaciaCampusContext context) : base(context)
    {
        _context = context;
    }

    public override async Task<IEnumerable<Presentacion>> GetAllAsync()
    {
        return await _context.Presentaciones
                    .Include(c => c.Inventarios)
                    .ThenInclude(c => c.DetalleMovimientoInventarios)
                    .ThenInclude(c => c.Facturas)
                    .ToListAsync();
    }

    public override async Task<(int totalRegistros, IEnumerable<Presentacion> registros)> GetAllAsync(
        int pageIndex,
        int pageSize,
        string search
    )
    {
        var query = _context.Presentaciones as IQueryable<Presentacion>;
    
        if (!string.IsNullOrEmpty(search))
        {
            query = query.Where(p => p.NombrePresentacion.ToLower().Contains(search)); // If necesary add .ToString() after varQuery
        }
        query = query.OrderBy(p => p.Id);
    
        var totalRegistros = await query.CountAsync();
        var registros = await query
                        .Include(p => p.Inventarios)
                        .Skip((pageIndex - 1) * pageSize)
                        .Take(pageSize)
                        .ToListAsync();
        return (totalRegistros, registros);
    }
}
