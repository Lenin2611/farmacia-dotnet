using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuration;

public class DetalleMovimientoInventarioConfiguration : IEntityTypeConfiguration<DetalleMovimientoInventario>
{
    public void Configure(EntityTypeBuilder<DetalleMovimientoInventario> builder)
    {
        builder.ToTable("detallemovimiento");

        builder.HasKey(c => new {c.IdInventarioFk, c.IdMovimientoInventarioFk});
        builder.Property(c => new {c.IdInventarioFk, c.IdMovimientoInventarioFk});

        builder.Property(x => x.Cantidad).HasColumnType("int");

        builder.Property(x => x.Precio).HasColumnType("double");

        builder.HasOne(x => x.Inventarios).WithMany(x => x.DetalleMovimientoInventarios).HasForeignKey(x => x.IdInventarioFk);
        builder.HasOne(x => x.MovimientoInventarios).WithMany(x => x.DetalleMovimientoInventarios).HasForeignKey(x => x.IdMovimientoInventarioFk);
    }
}
