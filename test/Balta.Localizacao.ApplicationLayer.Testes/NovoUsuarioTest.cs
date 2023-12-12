using Balta.Localizacao.ApplicationLayer.Commands.AutenticacaoCommands;
using Balta.Localizacao.ApplicationLayer.Commands.AutenticacaoCommands.Validations;
using Bogus.DataSets;
using Bogus;

namespace Balta.Localizacao.ApplicationLayer.Testes
{
    [Collection(nameof(CommandBogusFixtureCollection))]
    public class NovoUsuarioTest : IClassFixture<CommandBogusFixture>
    {
        private readonly CommandBogusFixture _commandBogusFixture;

        public NovoUsuarioTest(CommandBogusFixture commandBogusFixture)
        {
            _commandBogusFixture = commandBogusFixture;
        }

        [Fact(DisplayName = "NovoUsuarioCommand instanciado sem erros")]
        [Trait("Categoria", "Command")]
        public void NovoUsuarioCommand_CriarCommand_CriarCommandComSucesso()
        {
            // Arrange
            var command = _commandBogusFixture.GerarNovoUsuarioCommand();


            // Act
            var result = command.EhValido();


            // Assert
            Assert.True(result);
        }

        [Fact(DisplayName = "NovoUsuarioCommand instanciado com erros")]
        [Trait("Categoria", "Command")]
        public void NovoUsuarioCommand_CriarCommand_CriarCommandComFalha()
        {
            // Arrange
            var command = new NovoUsuarioCommand()
            {
                Nome = "",
                Email = "",
                Password = "",
                ConfirmPassword = "abcd@123"
            };


            // Act
            var result = command.EhValido();
            var erros = command.ValidationResult.Errors.Select(e => e.ErrorMessage);

            // Assert
            Assert.False(result);
            Assert.Contains(NovoUsuarioCommandValidation.NomeRequiredErrorMessage, erros);
            Assert.Contains(NovoUsuarioCommandValidation.EmailAddressErrorMessage, erros);
            Assert.Contains(NovoUsuarioCommandValidation.EmailRequiredErrorMessage, erros);
            Assert.Contains(NovoUsuarioCommandValidation.PasswordLenghtErrorMessage, erros);
            Assert.Contains(NovoUsuarioCommandValidation.PasswordRequiredErrorMessage, erros);
            Assert.Contains(NovoUsuarioCommandValidation.ConfirmPasswordErrorMessage, erros);
        }
    }
}