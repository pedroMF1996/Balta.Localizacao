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
            var identityresult = IdentityResult.Success;

            _userManager.Setup(u => u.CreateAsync(It.IsAny<IdentityUser>(), It.IsAny<string>()))
                .ReturnsAsync(identityresult);

            _userManager.Setup(u => u.FindByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(_usuarioBogusFixture.GerarUsuariosValidos(10).FirstOrDefault());

            _userManager.Setup(u => u.GetClaimsAsync(It.IsAny<IdentityUser>()))
                .ReturnsAsync(_usuarioBogusFixture.GerarListaClaimVazia());

            _userManager.Setup(u => u.GetRolesAsync(It.IsAny<IdentityUser>()))
                .ReturnsAsync(_usuarioBogusFixture.GerarListaRoleVazia());


            var command = _commandBogusFixture.GerarNovoUsuarioCommand();
            var commandHandler = new AutenticacaoCommandHandler(It.IsAny<SignInManager<IdentityUser>>(), _userManager.Object, appSetings);

            // Act
            var result = await commandHandler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsValid);
            _userManager.Verify(_userManager => _userManager.CreateAsync(It.IsAny<IdentityUser>(), It.IsAny<string>()), Times.Once);
            _userManager.Verify(_userManager => _userManager.FindByEmailAsync(command.Email), Times.Once);
            _userManager.Verify(_userManager => _userManager.GetClaimsAsync(It.IsAny<IdentityUser>()), Times.Once);
            _userManager.Verify(_userManager => _userManager.GetRolesAsync(It.IsAny<IdentityUser>()), Times.Once);
        }

        [Fact(DisplayName = "Login Deve Autenticar Com Sucesso")]
        [Trait("Categoria", "Command Handler")]
        public async Task LoginCommand_NovoUsuario_DeveAdicionarComSucesso()
        {
            // Arrange
            var appSetings = Options.Create(_usuarioBogusFixture.GerarAppSettings());

            var _userManager = _usuarioBogusFixture.AutoMocker.GetMock<UserManager<IdentityUser>>();

            _userManager.Setup(u => u.FindByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(_usuarioBogusFixture.GerarUsuariosValidos(10).FirstOrDefault());

            _userManager.Setup(u => u.GetClaimsAsync(It.IsAny<IdentityUser>()))
                .ReturnsAsync(_usuarioBogusFixture.GerarListaClaimVazia());

            _userManager.Setup(u => u.GetRolesAsync(It.IsAny<IdentityUser>()))
                .ReturnsAsync(_usuarioBogusFixture.GerarListaRoleVazia());


            var _signInManager = _usuarioBogusFixture.AutoMocker.GetMock<SignInManager<IdentityUser>>();
            var signinResult = SignInResult.Success;

            _signInManager.Setup(s => s.PasswordSignInAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>()))
                .ReturnsAsync(signinResult);

            var command = _commandBogusFixture.GerarLoginCommand();
            var commandHandler = new AutenticacaoCommandHandler(_signInManager.Object, _userManager.Object, appSetings);

            // Act
            var result = await commandHandler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsValid);
            _signInManager.Verify(_signInManager => _signInManager.PasswordSignInAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>()), Times.Once);
            _userManager.Verify(_userManager => _userManager.FindByEmailAsync(command.Email), Times.Once);
            _userManager.Verify(_userManager => _userManager.GetClaimsAsync(It.IsAny<IdentityUser>()), Times.Once);
            _userManager.Verify(_userManager => _userManager.GetRolesAsync(It.IsAny<IdentityUser>()), Times.Once);
        }
    }
}
