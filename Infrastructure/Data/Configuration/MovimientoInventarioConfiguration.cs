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

        builder.Property(x => x.IdPersonaResponsableFk).HasMaxLength(50);
        builder.HasOne(x => x.Personas).WithMany(p => p.MovimientoInventarios).HasForeignKey(x => x.IdPersonaResponsableFk);
        
        builder.Property(x => x.IdPersonaReceptorFk).HasMaxLength(50);
        builder.HasOne(x => x.Personas).WithMany(p => p.MovimientoInventarios).HasForeignKey(x => x.IdPersonaReceptorFk);
        
        builder.Property(x => x.IdTipoMovimientoInventarioFk).HasColumnType("int");
        builder.HasOne(x => x.TipoMovimientoInventarios).WithMany(p => p.MovimientoInventarios).HasForeignKey(x => x.IdTipoMovimientoInventarioFk);
        
        builder.Property(x => x.IdFormaPagoFk).HasColumnType("int");
        builder.HasOne(x => x.FormaPagos).WithMany(p => p.MovimientoInventarios).HasForeignKey(x => x.IdFormaPagoFk);
    }
}
