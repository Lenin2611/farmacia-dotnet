using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos;

public class FacturaDto
{
    public int Id { get; set; }
    public int FacturaActual { get; set; }
    public int FacturaInicial { get; set; }
    public int FacturaFinal { get; set; }
    public string NumeroResolucion { get; set; }
    public string IdPersonaFk { get; set; }
    public int IdDetalleMovimientoInventarioFk { get; set; }
}
