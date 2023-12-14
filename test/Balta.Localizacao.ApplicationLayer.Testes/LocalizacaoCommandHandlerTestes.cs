using Balta.Localizacao.ApplicationLayer.Commands.LocalizacaoCommands;
using Balta.Localizacao.ApplicationLayer.Commands.LocalizacaoCommands.Validations;
using Balta.Localizacao.DAL.DbContexts;
using Balta.Localizacao.DAL.Repository;
using Balta.Localizacao.Domain.Entities;
using Balta.Localizacao.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
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
                .Returns(_commandBogusFixture.RetornarEstado());
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
            estadoRepository.Setup(x => x.ObterEstadoPorId(It.IsAny<Guid>()))
                .Returns(_commandBogusFixture.RetornarEstado());
            estadoRepository.Setup(x => x.unitOfWork.Commit())
                .Returns(true);
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

        [Fact(DisplayName = "Remover Estado Com Sucesso")]
        [Trait("Categoria", "Command Handler")]
        public async Task RemoverEstadoCommand_RemoverEstado_DeveRemoverEstadoComSucesso()
        {
            // Arrange
            var command = _commandBogusFixture.GerarRemoverEstadoCommandValido();
            var estadoRepository = new Mock<EstadoFakeRepository>();
            estadoRepository.Setup(x => x.ObterEstadoPorId(It.IsAny<Guid>()))
                .Returns(_commandBogusFixture.RetornarEstado());
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
    }
}
