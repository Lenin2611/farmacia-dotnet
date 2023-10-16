using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos;

public class MovimientoInventarioDto
{
    public string Id { get; set; }
    public DateOnly FechaMovimientoInventario { get; set; }
    public DateOnly FechaVencimiento { get; set; }
    public string IdPersonaResponsableFk { get; set; }
    public string IdPersonaReceptorFk { get; set; }
    public int IdTipoMovimientoInventarioFk { get; set; }
    public int IdFormaPagoFk { get; set; }
}
