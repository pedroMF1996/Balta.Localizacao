using Balta.Localizacao.ApplicationLayer.Commands.LocalizacaoCommands;
using Balta.Localizacao.ApplicationLayer.Commands.LocalizacaoCommands.Validations;

namespace Balta.Localizacao.ApplicationLayer.Testes
{
    [Collection(nameof(CommandBogusFixtureCollection))]
    public class EstadoCommandTestes : IClassFixture<CommandBogusFixture>
    {
        private readonly CommandBogusFixture _commandBogusFixture;

        public EstadoCommandTestes(CommandBogusFixture commandBogusFixture)
        {
            _commandBogusFixture = commandBogusFixture;
        }

        [Fact(DisplayName = "AdicionarEstadoCommand Instanciado Com Sucesso")]
        [Trait("Categoria", "Command")]
        public void AdicionarEstadoCommand_CriarCommand_CriarCommandComSucesso()
        {
            // Arrange
            var command = new AdicionarEstadoCommand()
            {
                CodigoUf = "35",
                SiglaUf = "SP",
                NomeUf = "Sao Paulo"
            };

            // Act
            var result = command.EhValido();

            // Assert
            Assert.True(result);
        }

        [Fact(DisplayName = "AdicionarEstadoCommand Instanciado Com Falha")]
        [Trait("Categoria", "Command")]
        public void AdicionarEstadoCommand_CriarCommand_CriarCommandComFalha()
        {
            // Arrange
            var command = new AdicionarEstadoCommand()
            {
                CodigoUf = "",
                SiglaUf = "",
                NomeUf = ""
            };

            // Act
            var result = command.EhValido();

            // Assert
            var erros = command.ValidationResult.Errors.Select(e => e.ErrorMessage);
            Assert.False(result);
            Assert.Contains(AdicionarEstadoCommandValidation.NomeRequiredErrorMessage, erros);
            Assert.Contains(AdicionarEstadoCommandValidation.CodigoUfLengthErrorMessage, erros);
            Assert.Contains(AdicionarEstadoCommandValidation.CodigoUfRequiredErrorMessage, erros);
            Assert.Contains(AdicionarEstadoCommandValidation.SiglaUfLengthErrorMessage, erros);
            Assert.Contains(AdicionarEstadoCommandValidation.SiglaUfRequiredErrorMessage, erros);
        }

        [Fact(DisplayName = "EditarEstadoCommand Instanciado Com Sucesso")]
        [Trait("Categoria", "Command")]
        public void EditarEstadoCommand_CriarCommand_CriarCommandComSucesso()
        {
            // Arrange
            var command = new EditarEstadoCommand()
            {
                Id = Guid.NewGuid(),
                CodigoUf = "35",
                SiglaUf = "SP",
                NomeUf = "Sao Paulo"
            };

            // Act
            var result = command.EhValido();

            // Assert
            Assert.True(result);
        }

        [Fact(DisplayName = "EditarEstadoCommand Instanciado Com Falha")]
        [Trait("Categoria", "Command")]
        public void EditarEstadoCommand_CriarCommand_CriarCommandComFalha()
        {
            // Arrange
            var command = new EditarEstadoCommand()
            {
                Id = Guid.Empty,
                CodigoUf = "",
                SiglaUf = "",
                NomeUf = ""
            };

            // Act
            var result = command.EhValido();

            // Assert
            var erros = command.ValidationResult.Errors.Select(e => e.ErrorMessage);
            Assert.False(result);
            Assert.Contains(EditarEstadoCommandValidation.IdRequiredErrorMessage, erros);
            Assert.Contains(EditarEstadoCommandValidation.NomeRequiredErrorMessage, erros);
            Assert.Contains(EditarEstadoCommandValidation.CodigoUfLengthErrorMessage, erros);
            Assert.Contains(EditarEstadoCommandValidation.CodigoUfRequiredErrorMessage, erros);
            Assert.Contains(EditarEstadoCommandValidation.SiglaUfLengthErrorMessage, erros);
            Assert.Contains(EditarEstadoCommandValidation.SiglaUfRequiredErrorMessage, erros);
        }

        [Fact(DisplayName = "RemoverEstadoCommand Instanciado Com Sucesso")]
        [Trait("Categoria", "Command")]
        public void RemoverEstadoCommand_CriarCommand_CriarCommandComSucesso()
        {
            // Arrange
            var command = new RemoverEstadoCommand()
            {
                Id = Guid.NewGuid()
            };

            // Act
            var result = command.EhValido();

            // Assert
            Assert.True(result);
        }

        [Fact(DisplayName = "RemoverEstadoCommand Instanciado Com Falha")]
        [Trait("Categoria", "Command")]
        public void RemoverEstadoCommand_CriarCommand_CriarCommandComFalha()
        {
            // Arrange
            var command = new RemoverEstadoCommand();

            // Act
            var result = command.EhValido();

            // Assert
            var erros = command.ValidationResult.Errors.Select(e => e.ErrorMessage);
            Assert.False(result);
            Assert.Contains(RemoverEstadoCommandValidation.IdRequiredErrorMessage, erros);
        }
    }
}
