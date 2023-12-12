using Balta.Localizacao.ApplicationLayer.Commands.AutenticacaoCommands;
using Bogus;
using Bogus.DataSets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Balta.Localizacao.ApplicationLayer.Testes
{
    [CollectionDefinition(nameof(CommandBogusFixtureCollection))]
    public class CommandBogusFixtureCollection : ICollectionFixture<CommandBogusFixture>
    {

    }
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
    }
}
