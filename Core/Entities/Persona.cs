using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities;

public class Persona
{
    public string Id { get; set; }
    public string NombrePersona { get; set; }
    public DateTime FechaRegistroPersona { get; set; }
    public int IdRolPersonaFk { get; set; }
    public RolPersona RolPersonas { get; set; }
    public int IdTipoDocumentoFk { get; set; }
    public TipoDocumento TipoDocumentos { get; set; }
    public int IdTipoPersonaFk { get; set; }
    public TipoPersona TipoPersonas { get; set; }
    public ICollection<ContactoPersona> ContactoPersonas { get; set; }
    public ICollection<MovimientoInventario> MovimientoInventarios { get; set; }
    public ICollection<Factura> Facturas { get; set; }
    public UbicacionPersona UbicacionPersonas { get; set; }
}
