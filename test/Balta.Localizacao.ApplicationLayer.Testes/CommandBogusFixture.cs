using Balta.Localizacao.ApplicationLayer.Commands.AutenticacaoCommands;
using Balta.Localizacao.ApplicationLayer.Commands.LocalizacaoCommands;
using Balta.Localizacao.Domain.Entities;
using Bogus;
using Bogus.DataSets;

namespace Balta.Localizacao.ApplicationLayer.Testes
{
    [CollectionDefinition(nameof(CommandBogusFixtureCollection))]
    public class CommandBogusFixtureCollection : ICollectionFixture<CommandBogusFixture>
    { }
    
    public class CommandBogusFixture
    {
        public NovoUsuarioCommand GerarNovoUsuarioCommand()
        {
            var genero = new Faker().PickRandom<Name.Gender>();
            var password = new Faker().Internet.Password();
            var command = new Faker<NovoUsuarioCommand>().CustomInstantiator(f => new NovoUsuarioCommand()
            {
                Nome = f.Name.FirstName(genero),
                Email = "",
                Password = password,
                ConfirmPassword = password,

            }).RuleFor(c => c.Email, (f, c) => f.Internet.Email(c.Nome.ToLower())).Generate();

            return command;
        }

        public LoginCommand GerarLoginCommand()
        {
            var genero = new Faker().PickRandom<Name.Gender>();
            var command = new Faker<LoginCommand>().CustomInstantiator(f => new LoginCommand()
            {

                Email = "",
                Password = f.Internet.Password(),


            }).RuleFor(c => c.Email, (f, c) => f.Internet.Email(f.Name.FirstName(genero))).Generate();

            return command;
        }

        public AdicionarEstadoCommand GerarAdicionarEstadoCommandValido()
        {
            return new AdicionarEstadoCommand()
            {
                CodigoUf = "35",
                SiglaUf = "SP",
                NomeUf = "Sao Paulo"
            };
        }

        public AdicionarEstadoCommand GerarAdicionarEstadoCommandInvalido()
        {
            return new AdicionarEstadoCommand()
            {
                CodigoUf = "",
                SiglaUf = "",
                NomeUf = ""
            };
        }

        public EditarEstadoCommand GerarEditarEstadoCommandValido()
        {
            return new EditarEstadoCommand()
            {
                Id = Guid.NewGuid(),
                CodigoUf = "35",
                SiglaUf = "SP",
                NomeUf = "Sao Paulo"
            };
        }

        public EditarEstadoCommand GerarEditarEstadoCommandInvalido()
        {
            return new EditarEstadoCommand()
            {
                Id = Guid.Empty,
                CodigoUf = "",
                SiglaUf = "",
                NomeUf = ""
            };
        }

        public RemoverEstadoCommand GerarRemoverEstadoCommandValido()
        {
            return new RemoverEstadoCommand()
            {
                Id = Guid.NewGuid()
            };
        }

        public RemoverEstadoCommand GerarRemoverEstadoCommandInvalido()
        {
            return new RemoverEstadoCommand();
        }

        public AdicionarMunicipioCommand GerarAdicionarMunicipioCommandValido()
        {
            return new AdicionarMunicipioCommand()
            {
                Codigo = "3500105",
                CodigoUf = "35",
                Nome = "Adamantina"
            };
        }

        public AdicionarMunicipioCommand GerarAdicionarMunicipioCommandInvalido()
        {
            return new AdicionarMunicipioCommand()
            {
                Codigo = "",
                CodigoUf = "",
                Nome = ""
            };
        }

        public EditarMunicipioCommand GerarEditarMunicipioCommandValido()
        {
            return new EditarMunicipioCommand()
            {
                Id = Guid.NewGuid(),
                Codigo = "3500105",
                CodigoUf = "35",
                Nome = "Adamantina"
            };
        }

        public EditarMunicipioCommand GerarEditarMunicipioCommandInvalido()
        {
            return new EditarMunicipioCommand()
            {
                Id = Guid.Empty,
                Codigo = "",
                CodigoUf = "",
                Nome = ""
            };
        }

        public RemoverMunicipioCommand GerarRemoverMunicipioCommandValido()
        {
            return new RemoverMunicipioCommand()
            {
                Id = Guid.NewGuid()
            };
        }

        public RemoverMunicipioCommand GerarRemoverMunicipioCommandInvalido()
        {
            return new RemoverMunicipioCommand();
        }

        public Estado RetornarEstado()
        {
            return new Estado("11", "RO", "Rondônia");
        }
    }
}
