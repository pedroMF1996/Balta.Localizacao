using Balta.Localizacao.ApplicationLayer.Commands.AutenticacaoCommands;
using Balta.Localizacao.ApplicationLayer.Commands.AutenticacaoCommands.Validations;

namespace Balta.Localizacao.ApplicationLayer.Testes
{
    public class NovoUsuarioTest
    {
        [Fact(DisplayName = "NovoUsuarioCommand instanciado sem erros")]
        [Trait("Categoria", "Application Layer")]
        public void NovoUsuarioCommand_CriarCommand_CriarCommandComSucesso()
        {
            // Arrange
            var command = new NovoUsuarioCommand()
            {
                Nome = "Pedro",
                Email = "pmfrp@hotmail.com",
                Password = "abcd@123",
                ConfirmPassword = "abcd@123"
            };


            // Act
            var result = command.EhValido();


            // Assert
            Assert.True(result);
        }

        [Fact(DisplayName = "NovoUsuarioCommand instanciado com erros")]
        [Trait("Categoria", "Application Layer")]
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