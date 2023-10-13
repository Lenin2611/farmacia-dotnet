using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos;

public class ProductoDto
{
    public string Id { get; set; }
    public string NombreProducto { get; set; }
    public int IdMarcaFk { get; set; }
}
