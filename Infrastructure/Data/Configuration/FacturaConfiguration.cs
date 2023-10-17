using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuration;

public class FacturaConfiguration : IEntityTypeConfiguration<Factura>
{
    public void Configure(EntityTypeBuilder<Factura> builder)
    {
        builder.ToTable("factura");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id);

        builder.Property(x => x.FacturaActual).HasColumnType("int");

        builder.Property(x => x.FacturaInicial).HasColumnType("int");
        
        builder.Property(x => x.FacturaFinal).HasColumnType("int");
    
        builder.Property(x => x.NumeroResolucion).IsRequired().HasMaxLength(50);
    
        builder.Property(x => x.IdPersonaFk).HasMaxLength(50);
        builder.HasOne(x => x.Personas).WithMany(x => x.Facturas).HasForeignKey(x => x.IdPersonaFk);

        builder.Property(x => x.IdDetalleMovimientoInventarioFk).HasColumnType("int");
        builder.HasOne(x => x.DetalleMovimientoInventarios).WithMany(x => x.Facturas).HasForeignKey(x => x.IdDetalleMovimientoInventarioFk);
    }
}
