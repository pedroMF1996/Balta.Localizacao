using Balta.Localizacao.ApplicationLayer.Commands.AutenticacaoCommands;
using Balta.Localizacao.ApplicationLayer.Commands.AutenticacaoCommands.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Balta.Localizacao.ApplicationLayer.Testes
{
    public class LoginCommandTestes
    {
        [Fact(DisplayName = "LoginCommand instanciado sem erros")]
        [Trait("Categoria", "Application Layer")]
        public void LoginCommand_CriarCommand_CriarCommandComSucesso()
        {
            // Arrange
            var command = new LoginCommand()
            {
                Email = "pmfrp@hotmail.com",
                Password = "abcd@1234"
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
