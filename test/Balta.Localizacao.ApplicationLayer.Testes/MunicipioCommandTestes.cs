using Balta.Localizacao.ApplicationLayer.Commands.LocalizacaoCommands.Validations;

namespace Balta.Localizacao.ApplicationLayer.Testes
{
    [Collection(nameof(CommandBogusFixtureCollection))]
    public class MunicipioCommandTestes : IClassFixture<CommandBogusFixture>
    {
        private readonly CommandBogusFixture _commandBogusFixture;

        public MunicipioCommandTestes(CommandBogusFixture commandBogusFixture)
        {
            _commandBogusFixture = commandBogusFixture;
        }

        [Fact(DisplayName = "AdicionarMunicipioCommand Instanciado Com Sucesso")]
        [Trait("Categoria", "Command")]
        public void AdicionarMunicipioCommand_CriarCommand_CriarCommandComSucesso()
        {
            // Arrange
            var command = _commandBogusFixture.GerarAdicionarMunicipioCommandValidoSP();

            // Act
            var result = command.EhValido();

            // Assert
            Assert.True(result);
        }

        [Fact(DisplayName = "AdicionarMunicipioCommand Instanciado Com Falha")]
        [Trait("Categoria", "Command")]
        public void AdicionarMunicipioCommand_CriarCommand_CriarCommandComFalha()
        {
            // Arrange
            var command = _commandBogusFixture.GerarAdicionarMunicipioCommandInvalido();

            // Act
            var result = command.EhValido();

            // Assert
            var erros = command.ValidationResult.Errors.Select(e => e.ErrorMessage);
            Assert.False(result);
            Assert.Contains(AdicionarMunicipioCommandValidation.NomeRequiredErrorMessage, erros);
            Assert.Contains(AdicionarMunicipioCommandValidation.CodigoUfLengthErrorMessage, erros);
            Assert.Contains(AdicionarMunicipioCommandValidation.CodigoUfRequiredErrorMessage, erros);
            Assert.Contains(AdicionarMunicipioCommandValidation.CodigoRequiredErrorMessage, erros);
            Assert.Contains(AdicionarMunicipioCommandValidation.CodigoLengthErrorMessage, erros);
        }

        [Fact(DisplayName = "AdicionarMunicipioCommand Instanciado Com Falha Na Compatibilidade Entre Codigos")]
        [Trait("Categoria", "Command")]
        public void AdicionarMunicipioCommand_CriarCommand_CriarCommandComFalhaDeCompatibilidadeEntreCodigoECodigoUf()
        {
            // Arrange
            var command = _commandBogusFixture.GerarAdicionarMunicipioCommandInvalidoCodigoCodigoUfIncompativeis();

            // Act
            var result = command.EhValido();

            // Assert
            var erros = command.ValidationResult.Errors.Select(e => e.ErrorMessage);
            Assert.False(result);
            Assert.Contains(AdicionarMunicipioCommandValidation.CodigoCompatibleCodigoUfErrorMessage, erros);
        }

        [Fact(DisplayName = "EditarMunicipioCommand Instanciado Com Sucesso")]
        [Trait("Categoria", "Command")]
        public void EditarMunicipioCommand_CriarCommand_CriarCommandComSucesso()
        {
            // Arrange
            var command = _commandBogusFixture.GerarEditarMunicipioCommandValido();

            // Act
            var result = command.EhValido();

            // Assert
            Assert.True(result);
        }

        [Fact(DisplayName = "EditarMunicipioCommand Instanciado Com Falha")]
        [Trait("Categoria", "Command")]
        public void EditarMunicipioCommand_CriarCommand_CriarCommandComFalha()
        {
            // Arrange
            var command = _commandBogusFixture.GerarEditarMunicipioCommandInvalido();

            // Act
            var result = command.EhValido();

            // Assert
            var erros = command.ValidationResult.Errors.Select(e => e.ErrorMessage);
            Assert.False(result);
            Assert.Contains(EditarMunicipioCommandValidation.NomeRequiredErrorMessage, erros);
            Assert.Contains(EditarMunicipioCommandValidation.CodigoUfLengthErrorMessage, erros);
            Assert.Contains(EditarMunicipioCommandValidation.CodigoUfRequiredErrorMessage, erros);
            Assert.Contains(EditarMunicipioCommandValidation.CodigoRequiredErrorMessage, erros);
            Assert.Contains(EditarMunicipioCommandValidation.CodigoLengthErrorMessage, erros);
        }
        
        [Fact(DisplayName = "EditarMunicipioCommand Instanciado Com Falha De Compatibilidade Entre Codigo E CodigoUf")]
        [Trait("Categoria", "Command")]
        public void EditarMunicipioCommand_CriarCommand_CriarCommandComFalhaDeCompatibilidadeEntreCodigoECodigoUf()
        {
            // Arrange
            var command = _commandBogusFixture.GerarEditarMunicipioCommandInvalidoCodigoCodigoUfIncompativeis();

            // Act
            var result = command.EhValido();

            // Assert
            var erros = command.ValidationResult.Errors.Select(e => e.ErrorMessage);
            Assert.False(result);
            Assert.Contains(EditarMunicipioCommandValidation.CodigoCompatibleCodigoUfErrorMessage, erros);
        }

        [Fact(DisplayName = "RemoverMunicipioCommand Instanciado Com Sucesso")]
        [Trait("Categoria", "Command")]
        public void RemoverMunicipioCommand_CriarCommand_CriarCommandComSucesso()
        {
            // Arrange
            var command = _commandBogusFixture.GerarRemoverMunicipioCommandValido();

            // Act
            var result = command.EhValido();

            // Assert
            Assert.True(result);
        }
        
        [Fact(DisplayName = "RemoverMunicipioCommand Instanciado Com Falha")]
        [Trait("Categoria", "Command")]
        public void RemoverMunicipioCommand_CriarCommand_CriarCommandComFalha()
        {
            // Arrange
            var command = _commandBogusFixture.GerarRemoverMunicipioCommandInvalido();

            // Act
            var result = command.EhValido();

            // Assert
            var erros = command.ValidationResult.Errors.Select(e => e.ErrorMessage);
            Assert.False(result);
            Assert.Contains(RemoverEstadoCommandValidation.IdRequiredErrorMessage, erros);
        }
    }
}
