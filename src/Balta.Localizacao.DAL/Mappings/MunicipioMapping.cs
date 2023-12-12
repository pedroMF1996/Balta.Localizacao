using Balta.Localizacao.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Balta.Localizacao.DAL.Mappings
{
    public class MunicipioMapping : IEntityTypeConfiguration<Municipio>
    {
        public void Configure(EntityTypeBuilder<Municipio> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.CodigoUf)
                .IsRequired()
                .HasMaxLength(2);
            
            builder.Property(e => e.Codigo) 
                .IsRequired()
                .HasMaxLength(15);

            builder.Property(e => e.Nome)
                .IsRequired()
                .HasMaxLength(150);

            builder.HasOne(m => m.Estado)
                .WithMany(e => e.Municipios)
                .HasForeignKey(e => e.CodigoUf)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
