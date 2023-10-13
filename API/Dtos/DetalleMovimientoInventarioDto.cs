using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos;

public class DetalleMovimientoInventarioDto
{
    public int Id { get; set; }
    public int Cantidad { get; set; }
    public double Precio { get; set; }
    public string IdInventarioFk { get; set; }
    public string IdMovimientoInventarioFk { get; set; }
}
