using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Interfaces;

public interface IUnitOfWork
{
    public ICiudadRepository Ciudades { get; }
    public IContactoPersonaRepository ContactoPersonas { get; }
    public IDepartamentoRepository Departamentos { get; }
    public IDetalleMovimientoInventarioRepository DetalleMovimientoInventarios { get; }
    public IFacturaRepository Facturas { get; }
    public IFormaPagoRepository FormaPagos { get; }
    public IInventarioRepository Inventarios { get; }
    public IMarcaRepository Marcas { get; }
    public IMovimientoInventarioRepository MovimientoInventarios { get; }
    public IPaisRepository Paises { get; }
    public IPersonaRepository Personas { get; }
    public IPresentacionRepository Presentaciones { get; }
    public IProductoRepository Productos { get; }
    public IRolPersonaRepository RolPersonas { get; }
    public ITipoContactoRepository TipoContactos { get; }
    public ITipoDocumentoRepository TipoDocumentos { get; }
    public ITipoMovimientoInventarioRepository TipoMovimientoInventarios { get; }
    public ITipoPersonaRepository TipoPersonas { get; }
    public IUbicacionPersonaRepository UbicacionPersonas { get; }

    Task<int> SaveAsync();
}
