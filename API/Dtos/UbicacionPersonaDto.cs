using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos;

public class UbicacionPersonaDto
{
    public int Id { get; set; }
    public string TipoVia { get; set; }
    public int NumeroPrincipal { get; set; }
    public string LetraPrincipal { get; set; }
    public string Bis { get; set; }
    public string LetraSecundaria { get; set; }
    public string CardinalPrimario { get; set; }
    public int NumeroSecundario { get; set; }
    public string LetraTerciaria { get; set; }
    public int NumeroTerciario { get; set; }
    public string CardinalSecundario { get; set; }
    public string Complemento { get; set; }
    public string IdPersonaFk { get; set; }
    public int IdCiudadFk { get; set; }
}
