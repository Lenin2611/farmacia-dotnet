using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class PaisRepository : GenericRepository<Pais>,IPaisRepository
{
    private readonly FarmaciaCampusContext _context;

    public PaisRepository(FarmaciaCampusContext context) : base(context)
    {
        _context = context;
    }

    public override async Task<IEnumerable<Pais>> GetAllAsync()
    {
        return await _context.Paises
                    .Include(c => c.Departamentos)
                    .ThenInclude(c => c.Ciudades)
                    .ThenInclude(c => c.UbicacionPersonas)
                    .ToListAsync();
    }

    public override async Task<(int totalRegistros, IEnumerable<Pais> registros)> GetAllAsync(
        int pageIndex,
        int pageSize,
        string search
    )
    {
        var query = _context.Paises as IQueryable<Pais>;
    
        if (!string.IsNullOrEmpty(search))
        {
            query = query.Where(p => p.NombrePais.ToLower().Contains(search)); // If necesary add .ToString() after varQuery
        }
        query = query.OrderBy(p => p.Id);
    
        var totalRegistros = await query.CountAsync();
        var registros = await query
                        .Include(p => p.Departamentos)
                        .Skip((pageIndex - 1) * pageSize)
                        .Take(pageSize)
                        .ToListAsync();
        return (totalRegistros, registros);
    }
}
