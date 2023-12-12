using Balta.Localizacao.Core.Data;
using Balta.Localizacao.Domain.Entities;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;

namespace Balta.Localizacao.DAL.DbContexts
{
    public class LocalizacaoDbContext : DbContext, IUnitOfWork
    {
        public DbSet<Estado> Estados { get; set; }
        public DbSet<Municipio> Municipios { get; set; }

        public LocalizacaoDbContext(DbContextOptions<LocalizacaoDbContext> options) 
            : base(options)
        {
        }

        public bool Commit()
        {
            var success = SaveChanges() > 0;
            return success;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<ValidationResult>();
            
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(LocalizacaoDbContext).Assembly);
        }
    }
}
