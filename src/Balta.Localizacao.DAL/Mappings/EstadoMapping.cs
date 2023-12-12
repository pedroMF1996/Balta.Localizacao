using Balta.Localizacao.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Balta.Localizacao.DAL.Mappings
{
    public class EstadoMapping : IEntityTypeConfiguration<Estado>
    {
        public void Configure(EntityTypeBuilder<Estado> builder)
        {
            builder.HasKey(e => e.Id);
            
            builder.Property(e => e.CodigoUf)
                .IsRequired()
                .HasMaxLength(2);
            
            builder.Property(e => e.SiglaUf) 
                .IsRequired()
                .HasMaxLength(2);

            builder.Property(e => e.NomeUf)
                .IsRequired()
                .HasMaxLength(150);

            builder.HasMany(e => e.Municipios)
                .WithOne(m => m.Estado)
                .HasForeignKey(m => m.CodigoUf)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
