using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class DepartamentoRepository : GenericRepository<Departamento>,IDepartamentoRepository
{
    private readonly FarmaciaCampusContext _context;

    public DepartamentoRepository(FarmaciaCampusContext context) : base(context)
    {
        _context = context;
    }

    public override async Task<IEnumerable<Departamento>> GetAllAsync()
    {
        return await _context.Departamentos
                    .Include(c => c.Ciudades)
                    .ThenInclude(c => c.UbicacionPersonas)
                    .ToListAsync();
    }

    public override async Task<(int totalRegistros, IEnumerable<Departamento> registros)> GetAllAsync(
        int pageIndex,
        int pageSize,
        string search
    )
    {
        var query = _context.Departamentos as IQueryable<Departamento>;
    
        if (!string.IsNullOrEmpty(search))
        {
            query = query.Where(p => p.NombreDepartamento.ToLower().Contains(search)); // If necesary add .ToString() after varQuery
        }
        query = query.OrderBy(p => p.Id);
    
        var totalRegistros = await query.CountAsync();
        var registros = await query
                        .Include(p => p.Ciudades)
                        .Skip((pageIndex - 1) * pageSize)
                        .Take(pageSize)
                        .ToListAsync();
        return (totalRegistros, registros);
    }
}
