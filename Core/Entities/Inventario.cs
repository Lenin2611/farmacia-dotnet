using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities;

public class Inventario : BaseEntityVC
{
    public string NombreInventario { get; set; }
    public double PrecioInventario { get; set; }
    public int StockActual { get; set; }
    public int StockMinimo { get; set; }
    public int StockMaximo { get; set; }
    public int IdProductoFk { get; set; }
    public Producto Productos { get; set; }
    public int IdPresentacionFk { get; set; }
    public Presentacion Presentaciones { get; set; }
    public ICollection<DetalleMovimientoInventario> DetalleMovimientoInventarios { get; set; }
}
