using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities;

public class ContactoPersona : BaseEntity
{
    public int IdPersonaFk { get; set; }
    public Persona Personas { get; set; }
    public int IdTipoContactoFk { get; set; }
    public TipoContacto TipoContactos { get; set; }
}
