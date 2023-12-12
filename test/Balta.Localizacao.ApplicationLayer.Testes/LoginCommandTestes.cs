using Balta.Localizacao.ApplicationLayer.Commands.AutenticacaoCommands;
using Balta.Localizacao.ApplicationLayer.Commands.AutenticacaoCommands.Validations;

namespace Balta.Localizacao.ApplicationLayer.Testes
{
    [Collection(nameof(CommandBogusFixtureCollection))]
    public class LoginCommandTestes : IClassFixture<CommandBogusFixture>
    {
        private readonly CommandBogusFixture _commandBogusFixture;

        public LoginCommandTestes(CommandBogusFixture commandBogusFixture)
        {
            _commandBogusFixture = commandBogusFixture;
        }

        [Fact(DisplayName = "LoginCommand instanciado sem erros")]
        [Trait("Categoria", "Command")]
        public void LoginCommand_CriarCommand_CriarCommandComSucesso()
        {
            // Arrange
            var command = _commandBogusFixture.GerarLoginCommand();

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
            var command = new LoginCommand()
            {
                Email = "",
                Password = ""
            };


            // Act
            var result = command.EhValido();
            var erros = command.ValidationResult.Errors.Select(e => e.ErrorMessage);

            // Assert
            Assert.False(result);
            Assert.Contains(LoginCommandValidation.EmailRequiredErrorMessage, erros);
            Assert.Contains(LoginCommandValidation.EmailAddressErrorMessage, erros);
            Assert.Contains(LoginCommandValidation.PasswordLenghtErrorMessage, erros);
            Assert.Contains(LoginCommandValidation.PasswordRequiredErrorMessage, erros);
        }
    }
}
