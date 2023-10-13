using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class InventarioRepository : GenericRepositoryVC<Inventario>,IInventarioRepository
{
    private readonly FarmaciaCampusContext _context;

    public InventarioRepository(FarmaciaCampusContext context) : base(context)
    {
        _context = context;
    }

    public override async Task<IEnumerable<Inventario>> GetAllAsync()
    {
        return await _context.Inventarios
                    .Include(c => c.DetalleMovimientoInventarios)
                    .ThenInclude(x => x.Facturas)
                    .ToListAsync();
    }

    public override async Task<(int totalRegistros, IEnumerable<Inventario> registros)> GetAllAsync(
        int pageIndex,
        int pageSize,
        string search
    )
    {
        var query = _context.Inventarios as IQueryable<Inventario>;
    
        if (!string.IsNullOrEmpty(search))
        {
            query = query.Where(p => p.NombreInventario.ToLower().Contains(search)); // If necesary add .ToString() after varQuery
        }
        query = query.OrderBy(p => p.Id);
    
        var totalRegistros = await query.CountAsync();
        var registros = await query
                        .Include(c => c.DetalleMovimientoInventarios)
                        .Skip((pageIndex - 1) * pageSize)
                        .Take(pageSize)
                        .ToListAsync();
        return (totalRegistros, registros);
    }
}
