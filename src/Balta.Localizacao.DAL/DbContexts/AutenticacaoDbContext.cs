using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Balta.Localizacao.DAL.DbContexts
{
    public class AutenticacaoDbContext : IdentityDbContext
    {
        public AutenticacaoDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
