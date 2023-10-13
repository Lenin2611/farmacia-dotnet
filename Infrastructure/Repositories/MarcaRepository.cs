using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class MarcaRepository : GenericRepository<Marca>,IMarcaRepository
{
    private readonly FarmaciaCampusContext _context;

    public MarcaRepository(FarmaciaCampusContext context) : base(context)
    {
        _context = context;
    }

    public override async Task<IEnumerable<Marca>> GetAllAsync()
    {
        return await _context.Marcas
                    .Include(c => c.Productos)
                    .ThenInclude(c => c.Inventarios)
                    .ThenInclude(c => c.DetalleMovimientoInventarios)
                    .ThenInclude(c => c.Facturas)
                    .ToListAsync();
    }

    public override async Task<(int totalRegistros, IEnumerable<Marca> registros)> GetAllAsync(
        int pageIndex,
        int pageSize,
        string search
    )
    {
        var query = _context.Marcas as IQueryable<Marca>;
    
        if (!string.IsNullOrEmpty(search))
        {
            query = query.Where(p => p.NombreMarca.ToLower().Contains(search)); // If necesary add .ToString() after varQuery
        }
        query = query.OrderBy(p => p.Id);
    
        var totalRegistros = await query.CountAsync();
        var registros = await query
                        .Include(p => p.Productos)
                        .Skip((pageIndex - 1) * pageSize)
                        .Take(pageSize)
                        .ToListAsync();
        return (totalRegistros, registros);
    }
}
