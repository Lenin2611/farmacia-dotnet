using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities;

public class TipoMovimientoInventario : BaseEntity
{
    public string NombreTipoMovimientoInventario { get; set; }
    public ICollection<MovimientoInventario> MovimientoInventarios { get; set; }
}
