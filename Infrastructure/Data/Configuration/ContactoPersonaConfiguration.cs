using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuration;

public class ContactoPersonaConfiguration : IEntityTypeConfiguration<ContactoPersona>
{
    public void Configure(EntityTypeBuilder<ContactoPersona> builder)
    {
        builder.ToTable("contactopersona");

        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id);

        builder.Property(x => x.IdPersonaFk).HasMaxLength(50);
        builder.HasOne(c => c.Personas).WithMany(c => c.ContactoPersonas).HasForeignKey(c => c.IdPersonaFk);

        builder.Property(x => x.IdTipoContactoFk).HasColumnType("int");
        builder.HasOne(c => c.TipoContactos).WithMany(c => c.ContactoPersonas).HasForeignKey(c => c.IdTipoContactoFk);
    }
}
