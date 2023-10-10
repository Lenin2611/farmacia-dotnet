using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities;

public class RolPersona : BaseEntity
{
    public string NombreRolPersona { get; set; }
    public ICollection<Persona> Personas { get; set; }
}
