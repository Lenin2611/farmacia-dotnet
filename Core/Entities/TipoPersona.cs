using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities;

public class TipoPersona : BaseEntity
{
    public string NombreTipoPersona { get; set; }
    public ICollection<Persona> Personas { get; set; }
}
