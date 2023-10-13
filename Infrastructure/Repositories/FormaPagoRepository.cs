using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class FormaPagoRepository : GenericRepository<FormaPago>,IFormaPagoRepository
{
    private readonly FarmaciaCampusContext _context;

    public FormaPagoRepository(FarmaciaCampusContext context) : base(context)
    {
        _context = context;
    }

    public override async Task<IEnumerable<FormaPago>> GetAllAsync()
    {
        return await _context.FormaPagos
                    .Include(c => c.MovimientoInventarios)
                    .ThenInclude(c => c.DetalleMovimientoInventarios)
                    .ToListAsync();
    }

    public override async Task<(int totalRegistros, IEnumerable<FormaPago> registros)> GetAllAsync(
        int pageIndex,
        int pageSize,
        string search
    )
    {
        var query = _context.FormaPagos as IQueryable<FormaPago>;
    
        if (!string.IsNullOrEmpty(search))
        {
            query = query.Where(p => p.NombreFormaPago.ToLower().Contains(search)); // If necesary add .ToString() after varQuery
        }
        query = query.OrderBy(p => p.Id);
    
        var totalRegistros = await query.CountAsync();
        var registros = await query
                        .Include(p => p.MovimientoInventarios)
                        .Skip((pageIndex - 1) * pageSize)
                        .Take(pageSize)
                        .ToListAsync();
        return (totalRegistros, registros);
    }
}
