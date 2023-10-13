using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class TipoDocumentoRepository : GenericRepository<TipoDocumento>,ITipoDocumentoRepository
{
    private readonly FarmaciaCampusContext _context;

    public TipoDocumentoRepository(FarmaciaCampusContext context) : base(context)
    {
        _context = context;
    }

    public override async Task<IEnumerable<TipoDocumento>> GetAllAsync()
    {
        return await _context.TipoDocumentos
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

    public override async Task<(int totalRegistros, IEnumerable<TipoDocumento> registros)> GetAllAsync(
        int pageIndex,
        int pageSize,
        string search
    )
    {
        var query = _context.TipoDocumentos as IQueryable<TipoDocumento>;
    
        if (!string.IsNullOrEmpty(search))
        {
            query = query.Where(p => p.NombreTipoDocumento.ToString().ToLower().Contains(search)); // If necesary add .ToString() after varQuery
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
