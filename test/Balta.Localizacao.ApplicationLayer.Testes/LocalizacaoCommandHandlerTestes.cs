using Balta.Localizacao.ApplicationLayer.Commands.LocalizacaoCommands;
using Balta.Localizacao.ApplicationLayer.Commands.LocalizacaoCommands.Validations;
using Balta.Localizacao.Domain.Entities;
using Balta.Localizacao.Domain.Interfaces;
using Moq;
using Moq.AutoMock;

namespace Balta.Localizacao.ApplicationLayer.Testes
{
    [Collection(nameof(CommandBogusFixtureCollection))]
    public class LocalizacaoCommandHandlerTestes : IClassFixture<CommandBogusFixture>
    {
        private readonly CommandBogusFixture _commandBogusFixture;
        private readonly AutoMocker _autoMocker;
        public LocalizacaoCommandHandlerTestes(CommandBogusFixture commandBogusFixture)
        {
            _commandBogusFixture = commandBogusFixture;
            _autoMocker = new AutoMocker();
        }

        [Fact(DisplayName = "Adicionar Estado Com Sucesso")]
        [Trait("Categoria", "Command Handler")]
        public async Task AdicionarEstadoCommand_AdicionarEstado_DeveAdicionarEstadoComSucesso()
        {
            // Arrange
            var command = _commandBogusFixture.GerarAdicionarEstadoCommandValido();
            var commandHandler = _autoMocker.CreateInstance<LocalizacaoCommandHandler>();
            var estadoRepository = _autoMocker.GetMock<IEstadoRepository>();
            //estadoRepository.Setup(r => r.ObterEstadoPorId(It.IsAny<Guid>()))
            //                .Returns(_commandBogusFixture.RetornarEstado());
            estadoRepository.Setup(r => r.unitOfWork.Commit())
                            .Returns(true);
            estadoRepository.Setup(r => r.AdicionarEstado(It.IsAny<Estado>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await commandHandler.Handle(command,CancellationToken.None);

            // Assert
            Assert.True(result.IsValid);
            estadoRepository.Verify(e => e.AdicionarEstado(It.IsAny<Estado>()), Times.Once());
            estadoRepository.Verify(e => e.unitOfWork.Commit(), Times.Once());
        }
        
        [Fact(DisplayName = "Nao Deve Adicionar Estado")]
        [Trait("Categoria", "Command Handler")]
        public async Task AdicionarEstadoCommand_AdicionarEstado_DeveRetornarErrosDeValidacaoCommand()
        {
            // Arrange
            var command = _commandBogusFixture.GerarAdicionarEstadoCommandInvalido();
            var estadoRepository = new Mock<EstadoFakeRepository>();
            var commandHandler = new LocalizacaoCommandHandler(estadoRepository.Object);

            // Act
            var result = await commandHandler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.IsValid);
            estadoRepository.Verify(e => e.AdicionarEstado(It.IsAny<Estado>()), Times.Never());
            estadoRepository.Verify(e => e.unitOfWork.Commit(), Times.Never());
        }

        [Fact(DisplayName = "Editar Estado CodigoUf Com Sucesso")]
        [Trait("Categoria", "Command Handler")]
        public async Task EditarEstadoCommand_EditarCodigoUfEstado_DeveEditarEstadoComSucesso()
        {
            // Arrange
            var command = _commandBogusFixture.GerarEditarEstadoCommandValido();

            var estadoRepository = new Mock<EstadoFakeRepository>();
            estadoRepository.Setup(x => x.ObterEstadoPorId(It.IsAny<Guid>()))
                .Returns(_commandBogusFixture.RetornarEstadoSP());
            estadoRepository.Setup(x => x.unitOfWork.Commit())
                .Returns(true);

            var commandHandler = new LocalizacaoCommandHandler(estadoRepository.Object);

            // Act
            var result = await commandHandler.Handle(command,CancellationToken.None);

            // Assert
            Assert.True(result.IsValid);
            estadoRepository.Verify(e => e.EditarEstado(It.IsAny<Estado>()), Times.Once());
            estadoRepository.Verify(e => e.unitOfWork.Commit(), Times.Once());
        }
        
        [Fact(DisplayName = "Nao Deve Editar CodigoUf Estado")]
        [Trait("Categoria", "Command Handler")]
        public async Task EditarEstadoCommand_EditarCodigoUfEstado_DeveRetornarErrosDeValidacaoCommand()
        {
            // Arrange
            var command = _commandBogusFixture.GerarEditarEstadoCommandInvalido();
            var estadoRepository = new Mock<EstadoFakeRepository>();
            var commandHandler = new LocalizacaoCommandHandler(estadoRepository.Object);

            // Act
            var result = await commandHandler.Handle(command, CancellationToken.None);

            // Assert
            var erros = command.ValidationResult.Errors.Select(e => e.ErrorMessage);
            Assert.False(result.IsValid);
            Assert.Contains(AdicionarEstadoCommandValidation.NomeRequiredErrorMessage, erros);
            Assert.Contains(AdicionarEstadoCommandValidation.CodigoUfLengthErrorMessage, erros);
            Assert.Contains(AdicionarEstadoCommandValidation.CodigoUfRequiredErrorMessage, erros);
            Assert.Contains(AdicionarEstadoCommandValidation.SiglaUfLengthErrorMessage, erros);
            Assert.Contains(AdicionarEstadoCommandValidation.SiglaUfRequiredErrorMessage, erros);
            estadoRepository.Verify(e => e.EditarEstado(It.IsAny<Estado>()), Times.Never());
            estadoRepository.Verify(e => e.unitOfWork.Commit(), Times.Never());
        }
        
        [Fact(DisplayName = "Nao Deve Editar CodigoUf Estado Dados Errados Vindos Do Banco")]
        [Trait("Categoria", "Command Handler")]
        public async Task EditarEstadoCommand_EditarCodigoUfEstado_DeveRetornarErrosDeValidacaoDadosVindosDoBancoCommand()
        {
            // Arrange
            var command = _commandBogusFixture.GerarEditarEstadoCommandValido();
            var estadoRepository = new Mock<EstadoFakeRepository>();
            estadoRepository.Setup(x => x.ObterEstadoPorId(It.IsAny<Guid>()))
             .Returns(_commandBogusFixture.RetornarEstadoInvalido());

            var commandHandler = new LocalizacaoCommandHandler(estadoRepository.Object);

            // Act
            var result = await commandHandler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.IsValid);
            estadoRepository.Verify(e => e.ObterEstadoPorId(It.IsAny<Guid>()), Times.Once());
            estadoRepository.Verify(e => e.EditarEstado(It.IsAny<Estado>()), Times.Never());
            estadoRepository.Verify(e => e.unitOfWork.Commit(), Times.Never());
        }
        
        [Fact(DisplayName = "Nao Deve Editar CodigoUf Estado, Estado Nao Encontrado")]
        [Trait("Categoria", "Command Handler")]
        public async Task EditarEstadoCommand_EditarCodigoUfEstado_DeveRetornarErrosDeValidacaoDadoDoBancoNaoRetornadoCommand()
        {
            // Arrange
            var command = _commandBogusFixture.GerarEditarEstadoCommandValido();
            var estadoRepository = new Mock<EstadoFakeRepository>();
            var commandHandler = new LocalizacaoCommandHandler(estadoRepository.Object);

            // Act
            var result = await commandHandler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.IsValid);
            estadoRepository.Verify(e => e.ObterEstadoPorId(It.IsAny<Guid>()), Times.Once());
            estadoRepository.Verify(e => e.EditarEstado(It.IsAny<Estado>()), Times.Never());
            estadoRepository.Verify(e => e.unitOfWork.Commit(), Times.Never());
        }

        [Fact(DisplayName = "Remover Estado Com Sucesso")]
        [Trait("Categoria", "Command Handler")]
        public async Task RemoverEstadoCommand_RemoverEstado_DeveRemoverEstadoComSucesso()
        {
            // Arrange
            var command = _commandBogusFixture.GerarRemoverEstadoCommandValido();
            var estadoRepository = new Mock<EstadoFakeRepository>();
            estadoRepository.Setup(x => x.ObterEstadoPorId(It.IsAny<Guid>()))
                .Returns(_commandBogusFixture.RetornarEstadoSP());
            estadoRepository.Setup(x => x.unitOfWork.Commit())
                .Returns(true);
            var commandHandler = new LocalizacaoCommandHandler(estadoRepository.Object);

            // Act
            var result = await commandHandler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsValid);
            estadoRepository.Verify(e => e.ExcluirEstado(It.IsAny<Estado>()), Times.Once());
            estadoRepository.Verify(e => e.unitOfWork.Commit(), Times.Once());
        }
        
        [Fact(DisplayName = "Nao Deve Remover Estado")]
        [Trait("Categoria", "Command Handler")]
        public async Task RemoverEstadoCommandInvalido_RemoverEstado_NaoDeveRemoverEstado()
        {
            // Arrange
            var command = _commandBogusFixture.GerarRemoverEstadoCommandInvalido();
            var estadoRepository = new Mock<EstadoFakeRepository>();
            var commandHandler = new LocalizacaoCommandHandler(estadoRepository.Object);

            // Act
            var result = await commandHandler.Handle(command, CancellationToken.None);

            // Assert
            var erros = result.Errors.Select(e => e.ErrorMessage);
            Assert.False(result.IsValid);
            Assert.Contains(RemoverEstadoCommandValidation.IdRequiredErrorMessage, erros);
            estadoRepository.Verify(e => e.ExcluirEstado(It.IsAny<Estado>()), Times.Never());
            estadoRepository.Verify(e => e.unitOfWork.Commit(), Times.Never());
        }

        [Fact(DisplayName = "Nao Deve Remover Estado, Estado Nao Encontrado")]
        [Trait("Categoria", "Command Handler")]
        public async Task RemoverEstadoCommand_RemoverCodigoUfEstado_DeveRetornarErrosDeValidacaoDadoDoBancoNaoRetornadoCommand()
        {
            // Arrange
            var command = _commandBogusFixture.GerarRemoverEstadoCommandValido();
            var estadoRepository = new Mock<EstadoFakeRepository>();
            var commandHandler = new LocalizacaoCommandHandler(estadoRepository.Object);

            // Act
            var result = await commandHandler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.IsValid);
            estadoRepository.Verify(e => e.ObterEstadoPorId(It.IsAny<Guid>()), Times.Once());
            estadoRepository.Verify(e => e.ExcluirEstado(It.IsAny<Estado>()), Times.Never());
            estadoRepository.Verify(e => e.unitOfWork.Commit(), Times.Never());
        }

        [Fact(DisplayName = "Adicionar Municipio Com Sucesso")]
        [Trait("Categoria", "Command Handler")]
        public async Task AdicionarMunicipioCommand_AdicionarMunicipio_DeveAdicionarMunicipioComSucesso()
        {
            // Arrange
            var command = _commandBogusFixture.GerarAdicionarMunicipioCommandValidoSP();
            var estadoRepository = new Mock<EstadoFakeRepository>();
            estadoRepository.Setup(x => x.ObterEstadoPorCodigoUf(It.IsAny<string>()))
                .Returns(_commandBogusFixture.RetornarEstadoSP());
            estadoRepository.Setup(x => x.AdicionarMunicipios(It.IsAny<Municipio>()))
                .Returns(Task.CompletedTask);
            estadoRepository.Setup(x => x.unitOfWork.Commit())
                .Returns(true);
            var commandHandler = new LocalizacaoCommandHandler(estadoRepository.Object);

            // Act
            var result = await commandHandler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsValid);
            estadoRepository.Verify(e => e.ObterEstadoPorCodigoUf(It.IsAny<string>()), Times.Once());
            estadoRepository.Verify(e => e.AdicionarMunicipios(It.IsAny<Municipio>()), Times.Once());
            estadoRepository.Verify(e => e.unitOfWork.Commit(), Times.Once());
        }

        [Fact(DisplayName = "Nao Deve Adicionar Municipio Estado Referido Nao Encontrado")]
        [Trait("Categoria", "Command Handler")]
        public async Task AdicionarMunicipioCommand_AdicionarMunicipio_NaoDeveAdicionarMunicipioEstadoReferidoNaoEncontrado()
        {
            // Arrange
            var command = _commandBogusFixture.GerarAdicionarMunicipioCommandValidoSP();
            var estadoRepository = new Mock<EstadoFakeRepository>();
            var commandHandler = new LocalizacaoCommandHandler(estadoRepository.Object);

            // Act
            var result = await commandHandler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.IsValid);
            estadoRepository.Verify(e => e.ObterEstadoPorCodigoUf(It.IsAny<string>()), Times.Once());
            estadoRepository.Verify(e => e.AdicionarMunicipios(It.IsAny<Municipio>()), Times.Never());
            estadoRepository.Verify(e => e.unitOfWork.Commit(), Times.Never());
        }
        
        
        [Fact(DisplayName = "Nao Deve Adicionar Municipio Command Invalido")]
        [Trait("Categoria", "Command Handler")]
        public async Task AdicionarMunicipioCommandInvalido_AdicionarMunicipio_NaoDeveAdicionarMunicipioInvalido()
        {
            // Arrange
            var command = _commandBogusFixture.GerarAdicionarMunicipioCommandInvalido();
            var estadoRepository = new Mock<EstadoFakeRepository>();
            var commandHandler = new LocalizacaoCommandHandler(estadoRepository.Object);

            // Act
            var result = await commandHandler.Handle(command, CancellationToken.None);

            // Assert
            var erros = command.ValidationResult.Errors.Select(e => e.ErrorMessage);
            Assert.False(result.IsValid);
            Assert.Contains(AdicionarMunicipioCommandValidation.NomeRequiredErrorMessage, erros);
            Assert.Contains(AdicionarMunicipioCommandValidation.CodigoUfLengthErrorMessage, erros);
            Assert.Contains(AdicionarMunicipioCommandValidation.CodigoUfRequiredErrorMessage, erros);
            Assert.Contains(AdicionarMunicipioCommandValidation.CodigoRequiredErrorMessage, erros);
            Assert.Contains(AdicionarMunicipioCommandValidation.CodigoLengthErrorMessage, erros);
            estadoRepository.Verify(e => e.ObterEstadoPorCodigoUf(It.IsAny<string>()), Times.Never());
            estadoRepository.Verify(e => e.AdicionarMunicipios(It.IsAny<Municipio>()), Times.Never());
            estadoRepository.Verify(e => e.unitOfWork.Commit(), Times.Never());
        }
        
        
        [Fact(DisplayName = "Nao Deve Adicionar Municipio CodigoUf Incompativel Invalido")]
        [Trait("Categoria", "Command Handler")]
        public async Task AdicionarMunicipioCommandInvalido_AdicionarMunicipio_NaoDeveAdicionarMunicipioCodigoUfIncompativelInvalido()
        {
            // Arrange
            var command = _commandBogusFixture.GerarAdicionarMunicipioCommandInvalido();
            var estadoRepository = new Mock<EstadoFakeRepository>();
            estadoRepository.Setup(x => x.ObterEstadoPorCodigoUf(It.IsAny<string>()))
                .Returns(_commandBogusFixture.RetornarEstadoSP());
            var commandHandler = new LocalizacaoCommandHandler(estadoRepository.Object);

            // Act
            var result = await commandHandler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.IsValid);
            estadoRepository.Verify(e => e.ObterEstadoPorCodigoUf(It.IsAny<string>()), Times.Never());
            estadoRepository.Verify(e => e.AdicionarMunicipios(It.IsAny<Municipio>()), Times.Never());
            estadoRepository.Verify(e => e.unitOfWork.Commit(), Times.Never());
        }

        [Fact(DisplayName = "Nao Deve Adicionar Municipio Estado Referido Veio Incompleto Do Banco")]
        [Trait("Categoria", "Command Handler")]
        public async Task AdicionarMunicipioCommand_AdicionarMunicipio_NaoDeveAdicionarMunicipioEstadoReferidoVeioIncompletoDoBanco()
        {
            // Arrange
            var command = _commandBogusFixture.GerarAdicionarMunicipioCommandValidoSP();
            
            var estadoRepository = new Mock<EstadoFakeRepository>();
            estadoRepository.Setup(x => x.AdicionarMunicipios(It.IsAny<Municipio>()))
                            .Returns(Task.CompletedTask);
            estadoRepository.Setup(x => x.unitOfWork.Commit())
                            .Returns(true);
            estadoRepository.Setup(x => x.ObterEstadoPorCodigoUf(It.IsAny<string>()))
                            .Returns(_commandBogusFixture.RetornarEstadoInvalido());

            var commandHandler = new LocalizacaoCommandHandler(estadoRepository.Object);

            // Act 
            var result = await commandHandler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.IsValid);
            estadoRepository.Verify(e => e.ObterEstadoPorCodigoUf(It.IsAny<string>()), Times.Once());
            estadoRepository.Verify(e => e.AdicionarMunicipios(It.IsAny<Municipio>()), Times.Never());
            estadoRepository.Verify(e => e.unitOfWork.Commit(), Times.Never());
        }

        [Fact(DisplayName = "Editar Municipio Com Sucesso")]
        [Trait("Categoria", "Command Handler")]
        public async Task EditarMunicipioCommand_EditarMunicipio_DeveEditarMunicipioComSucesso()
        {
            // Arrange
            var command = _commandBogusFixture.GerarEditarMunicipioCommandValido();
            
            var estadoRepository = new Mock<EstadoFakeRepository>();
            estadoRepository.Setup(x => x.ObterEstadoPorCodigoUf(It.IsAny<string>()))
                .Returns(_commandBogusFixture.RetornarEstadoSP());
            estadoRepository.Setup(x => x.ObterMunicipioPorId(It.IsAny<Guid>()))
                .Returns(_commandBogusFixture.RetornarMunicipioSP());
            estadoRepository.Setup(x => x.unitOfWork.Commit())
                .Returns(true);

            var commandHandler = new LocalizacaoCommandHandler(estadoRepository.Object);

            // Act
            var result = await commandHandler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsValid);
            estadoRepository.Verify(e => e.ObterEstadoPorCodigoUf(It.IsAny<string>()), Times.Once());
            estadoRepository.Verify(e => e.EditarMunicipios(It.IsAny<Municipio>()), Times.Once());
            estadoRepository.Verify(e => e.unitOfWork.Commit(), Times.Once());
        }
        
        [Fact(DisplayName = "Nao Editar Municipio Com Command Invalido")]
        [Trait("Categoria", "Command Handler")]
        public async Task EditarMunicipioCommandInvalido_EditarMunicipio_NaoDeveEditarMunicipio()
        {
            // Arrange
            var command = _commandBogusFixture.GerarEditarMunicipioCommandInvalido();
            
            var estadoRepository = new Mock<EstadoFakeRepository>();
            var commandHandler = new LocalizacaoCommandHandler(estadoRepository.Object);

            // Act
            var result = await commandHandler.Handle(command, CancellationToken.None);

            // Assert
            var erros = command.ValidationResult.Errors.Select(e => e.ErrorMessage);
            Assert.False(result.IsValid);
            Assert.Contains(EditarMunicipioCommandValidation.NomeRequiredErrorMessage, erros);
            Assert.Contains(EditarMunicipioCommandValidation.CodigoUfLengthErrorMessage, erros);
            Assert.Contains(EditarMunicipioCommandValidation.CodigoUfRequiredErrorMessage, erros);
            Assert.Contains(EditarMunicipioCommandValidation.CodigoRequiredErrorMessage, erros);
            Assert.Contains(EditarMunicipioCommandValidation.CodigoLengthErrorMessage, erros);
            estadoRepository.Verify(e => e.ObterEstadoPorCodigoUf(It.IsAny<string>()), Times.Never());
            estadoRepository.Verify(e => e.EditarMunicipios(It.IsAny<Municipio>()), Times.Never());
            estadoRepository.Verify(e => e.unitOfWork.Commit(), Times.Never());
        }

        [Fact(DisplayName = "Nao Deve Editar Municipio Com Erro No Estado Vindo Do Banco")]
        [Trait("Categoria", "Command Handler")]
        public async Task EditarMunicipioCommand_EditarMunicipio_NaoDeveEditarMunicipioComErroNoEstadoVindoDoBanco()
        {
            // Arrange
            var command = _commandBogusFixture.GerarEditarMunicipioCommandValido();

            var estadoRepository = new Mock<EstadoFakeRepository>();
            estadoRepository.Setup(x => x.ObterEstadoPorCodigoUf(It.IsAny<string>()))
                .Returns(_commandBogusFixture.RetornarEstadoInvalido());
            estadoRepository.Setup(x => x.ObterMunicipioPorId(It.IsAny<Guid>()))
                .Returns(_commandBogusFixture.RetornarMunicipioSP());
            estadoRepository.Setup(x => x.unitOfWork.Commit())
                .Returns(true);

            var commandHandler = new LocalizacaoCommandHandler(estadoRepository.Object);

            // Act
            var result = await commandHandler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.IsValid);
            estadoRepository.Verify(e => e.ObterEstadoPorCodigoUf(It.IsAny<string>()), Times.Once());
            estadoRepository.Verify(e => e.EditarMunicipios(It.IsAny<Municipio>()), Times.Never());
            estadoRepository.Verify(e => e.unitOfWork.Commit(), Times.Never());
        }
        
        [Fact(DisplayName = "Nao Deve Editar Municipio Com Erro No Municipio Vindo Do Banco")]
        [Trait("Categoria", "Command Handler")]
        public async Task EditarMunicipioCommand_EditarMunicipio_NaoDeveEditarMunicipioComErroNoMunicipioVindoDoBanco()
        {
            // Arrange
            var command = _commandBogusFixture.GerarEditarMunicipioCommandValido();

            var estadoRepository = new Mock<EstadoFakeRepository>();
            estadoRepository.Setup(x => x.ObterEstadoPorCodigoUf(It.IsAny<string>()))
                            .Returns(_commandBogusFixture.RetornarEstadoSP());
            estadoRepository.Setup(x => x.ObterMunicipioPorId(It.IsAny<Guid>()))
                            .Returns(_commandBogusFixture.RetornarMunicipioInvalido());
            estadoRepository.Setup(x => x.unitOfWork.Commit())
                            .Returns(true);

            var commandHandler = new LocalizacaoCommandHandler(estadoRepository.Object);

            // Act
            var result = await commandHandler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.IsValid);
            estadoRepository.Verify(e => e.ObterEstadoPorCodigoUf(It.IsAny<string>()), Times.Once());
            estadoRepository.Verify(e => e.EditarMunicipios(It.IsAny<Municipio>()), Times.Never());
            estadoRepository.Verify(e => e.unitOfWork.Commit(), Times.Never());
        }
        
        [Fact(DisplayName = "Nao Deve Editar Municipio Sem Municipio Vindo Do Banco")]
        [Trait("Categoria", "Command Handler")]
        public async Task EditarMunicipioCommand_EditarMunicipio_NaoDeveEditarMunicipioSemMunicipioVindoDoBanco()
        {
            // Arrange
            var command = _commandBogusFixture.GerarEditarMunicipioCommandValido();

            var estadoRepository = new Mock<EstadoFakeRepository>();
            estadoRepository.Setup(x => x.ObterEstadoPorCodigoUf(It.IsAny<string>()))
                            .Returns(_commandBogusFixture.RetornarEstadoSP());
            estadoRepository.Setup(x => x.unitOfWork.Commit())
                            .Returns(true);

            var commandHandler = new LocalizacaoCommandHandler(estadoRepository.Object);

            // Act
            var result = await commandHandler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.IsValid);
            estadoRepository.Verify(e => e.ObterEstadoPorCodigoUf(It.IsAny<string>()), Times.Once());
            estadoRepository.Verify(e => e.EditarMunicipios(It.IsAny<Municipio>()), Times.Never());
            estadoRepository.Verify(e => e.unitOfWork.Commit(), Times.Never());
        }

        [Fact(DisplayName = "Remover Municipio Com Sucesso")]
        [Trait("Categoria", "Command Handler")]
        public async Task RemoverMunicipioCommand_RemoverMunicipio_DeveRemoverMunicipioComSucesso()
        {
            // Arrange
            var command = _commandBogusFixture.GerarRemoverMunicipioCommandValido();

            var estadoRepository = new Mock<EstadoFakeRepository>();
            estadoRepository.Setup(x => x.ObterMunicipioPorId(It.IsAny<Guid>()))
                .Returns(_commandBogusFixture.RetornarMunicipioSP());
            estadoRepository.Setup(x => x.unitOfWork.Commit())
                .Returns(true);

            var commandHandler = new LocalizacaoCommandHandler(estadoRepository.Object);

            // Act
            var result = await commandHandler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsValid);
            estadoRepository.Verify(e => e.ExcluirMunicipio(It.IsAny<Municipio>()), Times.Once());
            estadoRepository.Verify(e => e.unitOfWork.Commit(), Times.Once());
        }
        
        [Fact(DisplayName = "Remover Municipio Com Falhas De Validacao")]
        [Trait("Categoria", "Command Handler")]
        public async Task RemoverMunicipioCommand_RemoverMunicipio_NaoDeveRemoverMunicipioComCommandInvalido()
        {
            // Arrange
            var command = _commandBogusFixture.GerarRemoverMunicipioCommandInvalido();

            var estadoRepository = new Mock<EstadoFakeRepository>();
            var commandHandler = new LocalizacaoCommandHandler(estadoRepository.Object);

            // Act
            var result = await commandHandler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.IsValid);
            var erros = result.Errors.Select(e => e.ErrorMessage);
            Assert.Contains(RemoverEstadoCommandValidation.IdRequiredErrorMessage, erros);
            estadoRepository.Verify(e => e.ExcluirMunicipio(It.IsAny<Municipio>()), Times.Never());
            estadoRepository.Verify(e => e.unitOfWork.Commit(), Times.Never());
        }

        [Fact(DisplayName = "Nao Remover Municipio Com Municipio Nao Encontrado")]
        [Trait("Categoria", "Command Handler")]
        public async Task RemoverMunicipioCommand_RemoverMunicipio_NaoDeveRemoverMunicipioComMunicipioNaoEncontrado()
        {
            // Arrange
            var command = _commandBogusFixture.GerarRemoverMunicipioCommandValido();

            var estadoRepository = new Mock<EstadoFakeRepository>();

            var commandHandler = new LocalizacaoCommandHandler(estadoRepository.Object);

            // Act
            var result = await commandHandler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.IsValid);
            estadoRepository.Verify(e => e.ExcluirMunicipio(It.IsAny<Municipio>()), Times.Never());
            estadoRepository.Verify(e => e.unitOfWork.Commit(), Times.Never());
        }
    }
}
