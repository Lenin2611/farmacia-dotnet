using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuration;

public class UbicacionPersonaConfiguration : IEntityTypeConfiguration<UbicacionPersona>
{
    public void Configure(EntityTypeBuilder<UbicacionPersona> builder)
    {
        builder.ToTable("ubicacionpersona");

        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id);

        builder.Property(c => c.TipoVia).IsRequired().HasMaxLength(50);

        builder.Property(c => c.NumeroPrincipal).HasColumnType("int");

        builder.Property(c => c.LetraPrincipal).IsRequired().HasMaxLength(50);
    
        builder.Property(c => c.Bis).IsRequired().HasMaxLength(50);
    
        builder.Property(c => c.LetraSecundaria).IsRequired().HasMaxLength(50);

        builder.Property(c => c.CardinalPrimario).IsRequired().HasMaxLength(50);

        builder.Property(c => c.NumeroSecundario).HasColumnType("int");

        builder.Property(c => c.LetraTerciaria).IsRequired().HasMaxLength(50);

        builder.Property(c => c.NumeroTerciario).HasColumnType("int");

        builder.Property(c => c.CardinalSecundario).IsRequired().HasMaxLength(50);

        builder.Property(c => c.Complemento).IsRequired().HasMaxLength(50);

        builder.Property(x => x.IdCiudadFk).HasColumnType("int");
        builder.HasOne(x => x.Ciudades).WithMany(x => x.UbicacionPersonas).HasForeignKey(x => x.IdCiudadFk);

        builder.Property(x => x.IdPersonaFk).HasMaxLength(50);
        builder.HasOne(x => x.Personas).WithOne(x => x.UbicacionPersonas).HasForeignKey<UbicacionPersona>(x => x.IdPersonaFk);
    }
}
