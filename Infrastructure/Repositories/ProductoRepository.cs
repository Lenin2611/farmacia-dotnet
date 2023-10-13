using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ProductoRepository : GenericRepositoryVC<Producto>,IProductoRepository
{
    private readonly FarmaciaCampusContext _context;

    public ProductoRepository(FarmaciaCampusContext context) : base(context)
    {
        _context = context;
    }

    public override async Task<IEnumerable<Producto>> GetAllAsync()
    {
        return await _context.Productos
                    .Include(c => c.Inventarios)
                    .ThenInclude(c => c.DetalleMovimientoInventarios)
                    .ThenInclude(x => x.Facturas)
                    .ToListAsync();
    }

    public override async Task<(int totalRegistros, IEnumerable<Producto> registros)> GetAllAsync(
        int pageIndex,
        int pageSize,
        string search
    )
    {
        var query = _context.Productos as IQueryable<Producto>;
    
        if (!string.IsNullOrEmpty(search))
        {
            query = query.Where(p => p.NombreProducto.ToString().ToLower().Contains(search)); // If necesary add .ToString() after varQuery
        }
        query = query.OrderBy(p => p.Id);
    
        var totalRegistros = await query.CountAsync();
        var registros = await query
                        .Include(c => c.Inventarios)
                        .Skip((pageIndex - 1) * pageSize)
                        .Take(pageSize)
                        .ToListAsync();
        return (totalRegistros, registros);
    }
}
