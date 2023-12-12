using Balta.Localizacao.ApplicationLayer.Commands.AutenticacaoCommands;
using Balta.Localizacao.Core.Identity;
using Bogus;
using Bogus.DataSets;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Moq;

namespace Balta.Localizacao.ApplicationLayer.Testes
{
    [Collection(nameof(CommandBogusFixtureCollection))]
    public class AutenticacaoCommandHandlerTestes : IClassFixture<UsuarioBogusFixture>,
                                                    IClassFixture<CommandBogusFixture>
    {
        private readonly UsuarioBogusFixture _usuarioBogusFixture;
        private readonly CommandBogusFixture _commandBogusFixture;

        public AutenticacaoCommandHandlerTestes(UsuarioBogusFixture usuarioBogusFixture, CommandBogusFixture commandBogusFixture)
        {
            _usuarioBogusFixture = usuarioBogusFixture;
            _commandBogusFixture = commandBogusFixture;
        }

        [Fact(DisplayName = "Novo Usuario Deve Adicionar Com Sucesso")]
        [Trait("Categoria", "Command Handler")]
        public async Task NovoUsuarioCommand_NovoUsuario_DeveAdicionarComSucesso()
        {
            // Arrange
            var appSetings = Options.Create(_usuarioBogusFixture.GerarAppSettings());
            
            var _userManager = _usuarioBogusFixture.AutoMocker.GetMock<UserManager<IdentityUser>>();
            var identityresult = _usuarioBogusFixture.AutoMocker.GetMock<IdentityResult>().Object;
            
            _userManager.Setup(u => u.CreateAsync(It.IsAny<IdentityUser>(), It.IsAny<string>()))
                .ReturnsAsync(identityresult);

            _userManager.Setup(u => u.FindByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(_usuarioBogusFixture.GerarUsuariosValidos(10).FirstOrDefault());
            
            _userManager.Setup(u => u.GetClaimsAsync(It.IsAny<IdentityUser>()))
                .ReturnsAsync(_usuarioBogusFixture.GerarListaClaimVazia());
            
            _userManager.Setup(u => u.GetRolesAsync(It.IsAny<IdentityUser>()))
                .ReturnsAsync(_usuarioBogusFixture.GerarListaRoleVazia());


            var command = _commandBogusFixture.GerarNovoUsuarioCommand();
            var commandHandler = new AutenticacaoCommandHandler(It.IsAny<SignInManager<IdentityUser>>(), _userManager.Object,  appSetings );

            // Act
            var result = await commandHandler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsValid);
            _userManager.Verify(_userManager => _userManager.CreateAsync(It.IsAny<IdentityUser>(), It.IsAny<string>()), Times.Once);
            _userManager.Verify(_userManager => _userManager.FindByEmailAsync(command.Email), Times.Once);
            _userManager.Verify(_userManager => _userManager.GetClaimsAsync(It.IsAny<IdentityUser>()), Times.Once);
            _userManager.Verify(_userManager => _userManager.GetRolesAsync(It.IsAny<IdentityUser>()), Times.Once);
        }
    }
}
