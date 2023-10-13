using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class TipoContactoRepository : GenericRepository<TipoContacto>,ITipoContactoRepository
{
    private readonly FarmaciaCampusContext _context;

    public TipoContactoRepository(FarmaciaCampusContext context) : base(context)
    {
        _context = context;
    }

    public override async Task<IEnumerable<TipoContacto>> GetAllAsync()
    {
        return await _context.TipoContactos
        .Include(c => c.ContactoPersonas)
        .ToListAsync();
    }

    public override async Task<(int totalRegistros, IEnumerable<TipoContacto> registros)> GetAllAsync(
        int pageIndex,
        int pageSize,
        string search
    )
    {
        var query = _context.TipoContactos as IQueryable<TipoContacto>;
    
        if (!string.IsNullOrEmpty(search))
        {
            query = query.Where(p => p.NombreTipoContacto.ToLower().Contains(search)); // If necesary add .ToString() after varQuery
        }
        query = query.OrderBy(p => p.Id);
    
        var totalRegistros = await query.CountAsync();
        var registros = await query
                        .Include(c => c.ContactoPersonas)
                        .Skip((pageIndex - 1) * pageSize)
                        .Take(pageSize)
                        .ToListAsync();
        return (totalRegistros, registros);
    }
}
