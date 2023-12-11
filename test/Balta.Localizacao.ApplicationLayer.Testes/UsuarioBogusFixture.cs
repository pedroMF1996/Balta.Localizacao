using Balta.Localizacao.Core.Identity;
using Bogus;
using Bogus.DataSets;
using Microsoft.AspNetCore.Identity;
using Moq.AutoMock;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Balta.Localizacao.ApplicationLayer.Testes
{
    [CollectionDefinition(nameof(UusarioBogusCollection))]
    public class UusarioBogusCollection : ICollectionFixture<UsuarioBogusFixture>
    {

    }
    public class UsuarioBogusFixture : IDisposable
    {
        public readonly AutoMocker AutoMocker;

        public UsuarioBogusFixture()
        {
            AutoMocker = new AutoMocker();
        }

        public IdentityResult GerarIdentityResult()
        {
            var identityresult = AutoMocker.GetMock<IdentityResult>();
            return identityresult.Object;
        }

        public IEnumerable<IdentityUser> GerarUsuariosValidos(int quantidade)
        {
            var genero = new Faker().PickRandom<Name.Gender>();
            var usuarios = new Faker<IdentityUser>().CustomInstantiator(f => new IdentityUser()
            {
                UserName = f.Name.FirstName(genero),
                AccessFailedCount = 0,
                Email = "",
                Id = Guid.NewGuid().ToString(),
                LockoutEnabled = false,
                EmailConfirmed = true,
            }).RuleFor(c => c.Email, (f, c) => f.Internet.Email(c.UserName.ToLower()));

            return usuarios.Generate(quantidade);
        }

        public IList<Claim> GerarListaClaimVazia()
        {
            return new List<Claim>();
        }
        
        public IList<string> GerarListaRoleVazia()
        {
            return new List<string>();
        }

        public List<Claim> GerarListaClaimValidas(IdentityUser user)
        {
            var claims = new Faker<List<Claim>>().CustomInstantiator(f => new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Nbf, ToUnixEpochDate(DateTime.UtcNow).ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString(), ClaimValueTypes.Integer64)
            });

            return claims;
        }

        public AppSetings GerarAppSettings()
        {
            return new AppSetings()
            {
                Secret = "MINHASENHASUPERSECRETA9Y058ABRACADAVER",
                ExpiracaoHoras = 2,
                Emissor = "MeuSistema",
                ValidoEm = "https://localhost"
            };
        }

        public string ObterSecret()
        {
            return GerarAppSettings().Secret;
        }

        private static long ToUnixEpochDate(DateTime utcNow)
        {
            return (long)Math.Round((utcNow.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);
        }


        public void Dispose()
        {
        }
    }
}
