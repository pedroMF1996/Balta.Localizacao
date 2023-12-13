using Balta.Localizacao.ApplicationLayer.Commands.LocalizacaoCommands;
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
            var commandHandler = _autoMocker.CreateInstance<LocalizacaoCommandHandler>();
            var estadoRepository = _autoMocker.GetMock<IEstadoRepository>();

            // Act
            var result = await commandHandler.Handle(command,CancellationToken.None);

            // Assert
            Assert.False(result.IsValid);
            estadoRepository.Verify(e => e.AdicionarEstado(It.IsAny<Estado>()), Times.Never());
            estadoRepository.Verify(e => e.unitOfWork.Commit(), Times.Never());
        }

        [Fact(DisplayName = "Adicionar Estado Com Sucesso")]
        [Trait("Categoria", "Command Handler")]
        public async Task EditarEstadoCommand_EditarCodigoUfEstado_DeveEditarEstadoComSucesso()
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
        public async Task EditarEstadoCommand_EditarCodigoUfEstado_DeveRetornarErrosDeValidacaoCommand()
        {
            // Arrange
            var command = _commandBogusFixture.GerarAdicionarEstadoCommandInvalido();
            var commandHandler = _autoMocker.CreateInstance<LocalizacaoCommandHandler>();
            var estadoRepository = _autoMocker.GetMock<IEstadoRepository>();

            // Act
            var result = await commandHandler.Handle(command,CancellationToken.None);

            // Assert
            Assert.False(result.IsValid);
            estadoRepository.Verify(e => e.AdicionarEstado(It.IsAny<Estado>()), Times.Never());
            estadoRepository.Verify(e => e.unitOfWork.Commit(), Times.Never());
        }
    }
}
