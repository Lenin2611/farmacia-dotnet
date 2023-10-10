using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities;

public class FormaPago : BaseEntity
{
    public string NombreFormaPago { get; set; }
    public ICollection<MovimientoInventario> MovimientoInventarios { get; set; }
}
