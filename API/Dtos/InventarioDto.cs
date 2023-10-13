using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos;

public class InventarioDto
{
    public string Id { get; set; }
    public string NombreInventario { get; set; }
    public double PrecioInventario { get; set; }
    public int StockActual { get; set; }
    public int StockMinimo { get; set; }
    public int StockMaximo { get; set; }
    public string IdProductoFk { get; set; }
    public int IdPresentacionFk { get; set; }
}
