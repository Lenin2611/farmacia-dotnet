using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class CiudadRepository : GenericRepository<Ciudad>
{
    private readonly FarmaciaCampusContext _context;

    public CiudadRepository(FarmaciaCampusContext context) : base(context)
    {
        _context = context;
    }

    public override async Task<IEnumerable<Ciudad>> GetAllAsync()
    {
        return await _context.Ciudades
                    .Include(c => c.UbicacionPersonas)
                    .ToListAsync();
    }
}
