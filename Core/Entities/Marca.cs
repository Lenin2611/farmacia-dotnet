using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities;

public class Marca : BaseEntity
{
    public string NombreMarca { get; set; }
    public ICollection<Producto> Productos { get; set; }
}
