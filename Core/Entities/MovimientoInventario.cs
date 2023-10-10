using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities;

public class MovimientoInventario
{
    public string Id { get; set; }
    public DateTime FechaMovimientoInventario { get; set; }
    public DateTime FechaVencimiento { get; set; }
    public int IdPersonaResponsableFk { get; set; }
    public int IdPersonaReceptorFk { get; set; }
    public Persona Personas { get; set; }
    public int IdTipoMovimientoInventarioFk { get; set; }
    public TipoMovimientoInventario TipoMovimientoInventarios { get; set; }
    public int IdFormaPagoFk { get; set; }
    public FormaPago FormaPagos { get; set; }
    public ICollection<DetalleMovimientoInventario> DetalleMovimientoInventarios { get; set; }
}
