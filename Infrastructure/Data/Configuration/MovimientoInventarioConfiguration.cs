using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuration;

public class MovimientoInventarioConfiguration : IEntityTypeConfiguration<MovimientoInventario>
{
    public void Configure(EntityTypeBuilder<MovimientoInventario> builder)
    {
        builder.ToTable("movimientoinventario");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasMaxLength(50);

        builder.Property(x => x.FechaMovimientoInventario).HasColumnType("date");

        builder.Property(x => x.FechaVencimiento).HasColumnType("date");

        builder.HasOne(x => x.Personas).WithMany(p => p.MovimientoInventarios).HasForeignKey(x => x.IdPersonaResponsableFk);
        builder.HasOne(x => x.Personas).WithMany(p => p.MovimientoInventarios).HasForeignKey(x => x.IdPersonaReceptorFk);
        builder.HasOne(x => x.TipoMovimientoInventarios).WithMany(p => p.MovimientoInventarios).HasForeignKey(x => x.IdTipoMovimientoInventarioFk);
        builder.HasOne(x => x.FormaPagos).WithMany(p => p.MovimientoInventarios).HasForeignKey(x => x.IdFormaPagoFk);
    }
}
