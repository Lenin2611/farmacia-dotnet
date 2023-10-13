using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Repositories;

namespace Infrastructure.UnitOfWork;

public class UnitOfWork : IUnitOfWork,IDisposable
{
    private readonly FarmaciaCampusContext _context;
    private ICiudadRepository _Ciudades;
    private IContactoPersonaRepository _ContactoPersonas;
    private IDepartamentoRepository _Departamentos;
    private IDetalleMovimientoInventarioRepository _DetalleMovimientoInventarios;
    private IFacturaRepository _Facturas;
    private IFormaPagoRepository _FormaPagos;
    private IInventarioRepository _Inventarios;
    private IMarcaRepository _Marcas;
    private IMovimientoInventarioRepository _MovimientoInventarios;
    private IPaisRepository _Paises;
    private IPersonaRepository _Personas;
    private IPresentacionRepository _Presentaciones;
    private IProductoRepository _Productos;
    private IRolPersonaRepository _RolPersonas;
    private ITipoContactoRepository _TipoContactos;
    private ITipoDocumentoRepository _TipoDocumentos;
    private ITipoMovimientoInventarioRepository _TipoMovimientoInventarios;
    private ITipoPersonaRepository _TipoPersonas;
    private IUbicacionPersonaRepository _UbicacionPersonas;

    public UnitOfWork(FarmaciaCampusContext context)
    {
        _context = context;
    }

    public ICiudadRepository Ciudades
    {
        get
        {
            if (_Ciudades == null)
            {
                _Ciudades = new CiudadRepository (_context); // Remember putting the base in the repository of this entity
            }
            return _Ciudades;
        }
    }
    public IContactoPersonaRepository ContactoPersonas
    {
        get
        {
            if (_ContactoPersonas == null)
            {
                _ContactoPersonas = new ContactoPersonaRepository (_context); // Remember putting the base in the repository of this entity
            }
            return _ContactoPersonas;
        }
    }
    public IDepartamentoRepository Departamentos
    {
        get
        {
            if (_Departamentos == null)
            {
                _Departamentos = new DepartamentoRepository (_context); // Remember putting the base in the repository of this entity
            }
            return _Departamentos;
        }
    }
    public IDetalleMovimientoInventarioRepository DetalleMovimientoInventarios
    {
        get
        {
            if (_DetalleMovimientoInventarios == null)
            {
                _DetalleMovimientoInventarios = new DetalleMovimientoInventarioRepository (_context); // Remember putting the base in the repository of this entity
            }
            return _DetalleMovimientoInventarios;
        }
    }
    public IFacturaRepository Facturas
    {
        get
        {
            if (_Facturas == null)
            {
                _Facturas = new FacturaRepository (_context); // Remember putting the base in the repository of this entity
            }
            return _Facturas;
        }
    }
    public IFormaPagoRepository FormaPagos
    {
        get
        {
            if (_FormaPagos == null)
            {
                _FormaPagos = new FormaPagoRepository (_context); // Remember putting the base in the repository of this entity
            }
            return _FormaPagos;
        }
    }
    public IInventarioRepository Inventarios
    {
        get
        {
            if (_Inventarios == null)
            {
                _Inventarios = new InventarioRepository (_context); // Remember putting the base in the repository of this entity
            }
            return _Inventarios;
        }
    }
    public IMarcaRepository Marcas
    {
        get
        {
            if (_Marcas == null)
            {
                _Marcas = new MarcaRepository (_context); // Remember putting the base in the repository of this entity
            }
            return _Marcas;
        }
    }
    public IMovimientoInventarioRepository MovimientoInventarios
    {
        get
        {
            if (_MovimientoInventarios == null)
            {
                _MovimientoInventarios = new MovimientoInventarioRepository (_context); // Remember putting the base in the repository of this entity
            }
            return _MovimientoInventarios;
        }
    }
    public IPaisRepository Paises
    {
        get
        {
            if (_Paises == null)
            {
                _Paises = new PaisRepository (_context); // Remember putting the base in the repository of this entity
            }
            return _Paises;
        }
    }
    public IPersonaRepository Personas
    {
        get
        {
            if (_Personas == null)
            {
                _Personas = new PersonaRepository (_context); // Remember putting the base in the repository of this entity
            }
            return _Personas;
        }
    }
    public IPresentacionRepository Presentaciones
    {
        get
        {
            if (_Presentaciones == null)
            {
                _Presentaciones = new PresentacionRepository (_context); // Remember putting the base in the repository of this entity
            }
            return _Presentaciones;
        }
    }
    public IProductoRepository Productos
    {
        get
        {
            if (_Productos == null)
            {
                _Productos = new ProductoRepository (_context); // Remember putting the base in the repository of this entity
            }
            return _Productos;
        }
    }
    public IRolPersonaRepository RolPersonas
    {
        get
        {
            if (_RolPersonas == null)
            {
                _RolPersonas = new RolPersonaRepository (_context); // Remember putting the base in the repository of this entity
            }
            return _RolPersonas;
        }
    }
    public ITipoContactoRepository TipoContactos
    {
        get
        {
            if (_TipoContactos == null)
            {
                _TipoContactos = new TipoContactoRepository (_context); // Remember putting the base in the repository of this entity
            }
            return _TipoContactos;
        }
    }
    public ITipoDocumentoRepository TipoDocumentos
    {
        get
        {
            if (_TipoDocumentos == null)
            {
                _TipoDocumentos = new TipoDocumentoRepository (_context); // Remember putting the base in the repository of this entity
            }
            return _TipoDocumentos;
        }
    }
    public ITipoMovimientoInventarioRepository TipoMovimientoInventarios
    {
        get
        {
            if (_TipoMovimientoInventarios == null)
            {
                _TipoMovimientoInventarios = new TipoMovimientoInventarioRepository (_context); // Remember putting the base in the repository of this entity
            }
            return _TipoMovimientoInventarios;
        }
    }
    public ITipoPersonaRepository TipoPersonas
    {
        get
        {
            if (_TipoPersonas == null)
            {
                _TipoPersonas = new TipoPersonaRepository (_context); // Remember putting the base in the repository of this entity
            }
            return _TipoPersonas;
        }
    }
    public IUbicacionPersonaRepository UbicacionPersonas
    {
        get
        {
            if (_UbicacionPersonas == null)
            {
                _UbicacionPersonas = new UbicacionPersonaRepository (_context); // Remember putting the base in the repository of this entity
            }
            return _UbicacionPersonas;
        }
    }

    public Task<int> SaveAsync()
    {
        return _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
