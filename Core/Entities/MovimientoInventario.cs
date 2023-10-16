using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities;

public class MovimientoInventario : BaseEntityVC
{
    public DateOnly FechaMovimientoInventario { get; set; }
    public DateOnly FechaVencimiento { get; set; }
    public string IdPersonaResponsableFk { get; set; }
    public string IdPersonaReceptorFk { get; set; }
    public Persona Personas { get; set; }
    public int IdTipoMovimientoInventarioFk { get; set; }
    public TipoMovimientoInventario TipoMovimientoInventarios { get; set; }
    public int IdFormaPagoFk { get; set; }
    public FormaPago FormaPagos { get; set; }
    public ICollection<DetalleMovimientoInventario> DetalleMovimientoInventarios { get; set; }
}
