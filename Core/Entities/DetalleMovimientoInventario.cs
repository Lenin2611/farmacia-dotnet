using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities;

public class DetalleMovimientoInventario
{
    public int Cantidad { get; set; }
    public double Precio { get; set; }
    public int IdInventarioFk { get; set; }
    public Inventario Inventarios { get; set; }
    public int IdMovimientoInventarioFk { get; set; }
    public MovimientoInventario MovimientoInventarios { get; set; }
    public ICollection<Factura> Facturas { get; set; }
}
