using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class MovimientoInventarioRepository : GenericRepositoryVC<MovimientoInventario>,IMovimientoInventarioRepository
{
    private readonly FarmaciaCampusContext _context;

    public MovimientoInventarioRepository(FarmaciaCampusContext context) : base(context)
    {
        _context = context;
    }

    public override async Task<IEnumerable<MovimientoInventario>> GetAllAsync()
    {
        return await _context.MovimientoInventarios
                    .Include(c => c.DetalleMovimientoInventarios)
                    .ThenInclude(c => c.Facturas)
                    .ToListAsync();
    }

    public override async Task<(int totalRegistros, IEnumerable<MovimientoInventario> registros)> GetAllAsync(
        int pageIndex,
        int pageSize,
        string search
    )
    {
        var query = _context.MovimientoInventarios as IQueryable<MovimientoInventario>;
    
        if (!string.IsNullOrEmpty(search))
        {
            query = query.Where(p => p.FechaMovimientoInventario.ToString().ToLower().Contains(search)); // If necesary add .ToString() after varQuery
        }
        query = query.OrderBy(p => p.Id);
    
        var totalRegistros = await query.CountAsync();
        var registros = await query
                        .Include(p => p.DetalleMovimientoInventarios)
                        .Skip((pageIndex - 1) * pageSize)
                        .Take(pageSize)
                        .ToListAsync();
        return (totalRegistros, registros);
    }
}
