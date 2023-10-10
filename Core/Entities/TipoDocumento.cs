using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities;

public class TipoDocumento : BaseEntity
{
    public string NombreTipoDocumento { get; set; }
    public ICollection<Persona> Personas { get; set; }
}
