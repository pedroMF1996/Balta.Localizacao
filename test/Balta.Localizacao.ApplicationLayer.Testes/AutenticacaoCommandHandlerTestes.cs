using Balta.Localizacao.ApplicationLayer.Commands.AutenticacaoCommands;
using Balta.Localizacao.ApplicationLayer.Commands.AutenticacaoCommands.Validations;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Moq;

namespace Balta.Localizacao.ApplicationLayer.Testes
{
    [Collection(nameof(CommandBogusFixtureCollection))]
    [TestCaseOrderer("Balta.Localizacao.ApplicationLayer.Testes.TestOrderProperty", "Balta.Localizacao.ApplicationLayer.Testes")]
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

        [Fact(DisplayName = "Novo Usuario Deve Ser Adicionado Com Sucesso"), TestOrderProperty(1)]
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
            _userManager.Verify(_userManager => _userManager.CreateAsync(It.IsAny<IdentityUser>(), It.IsAny<string>()), Times.AtLeastOnce);
            _userManager.Verify(_userManager => _userManager.FindByEmailAsync(command.Email), Times.AtLeastOnce);
            _userManager.Verify(_userManager => _userManager.GetClaimsAsync(It.IsAny<IdentityUser>()), Times.AtLeastOnce);
            _userManager.Verify(_userManager => _userManager.GetRolesAsync(It.IsAny<IdentityUser>()), Times.AtLeastOnce);
        }

        [Fact(DisplayName = "Novo Usuario Com Roles Deve Ser Adicionado Com Sucesso"), TestOrderProperty(2)]
        [Trait("Categoria", "Command Handler")]
        public async Task NovoUsuarioCommand_NovoUsuarioComRoles_DeveAdicionarComSucesso()
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
                .ReturnsAsync(_usuarioBogusFixture.GerarListaRolePopulada());


            var command = _commandBogusFixture.GerarNovoUsuarioCommand();
            var commandHandler = new AutenticacaoCommandHandler(It.IsAny<SignInManager<IdentityUser>>(), _userManager.Object, appSetings);

            // Act
            var result = await commandHandler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsValid);
            _userManager.Verify(_userManager => _userManager.CreateAsync(It.IsAny<IdentityUser>(), It.IsAny<string>()), Times.AtLeastOnce);
            _userManager.Verify(_userManager => _userManager.FindByEmailAsync(command.Email), Times.AtLeastOnce);
            _userManager.Verify(_userManager => _userManager.GetClaimsAsync(It.IsAny<IdentityUser>()), Times.AtLeastOnce);
            _userManager.Verify(_userManager => _userManager.GetRolesAsync(It.IsAny<IdentityUser>()), Times.AtLeastOnce);
        }

        [Fact(DisplayName = "Novo Usuario Deve Ser Adicionado Com Falha"), TestOrderProperty(3)]
        [Trait("Categoria", "Command Handler")]
        public async Task NovoUsuarioCommand_NovoUsuario_DeveAdicionarComFalha()
        {
            // Arrange
            var appSetings = Options.Create(_usuarioBogusFixture.GerarAppSettings());

            var _userManager = _usuarioBogusFixture.AutoMocker.GetMock<UserManager<IdentityUser>>();
            var error = new IdentityErrorDescriber().PasswordMismatch();
            var identityresult = IdentityResult.Failed(new IdentityError() { Code = error.Code, Description = error.Description });

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
            Assert.False(result.IsValid);
            _userManager.Verify(_userManager => _userManager.CreateAsync(It.IsAny<IdentityUser>(), It.IsAny<string>()), Times.Once);
        }
        
        [Fact(DisplayName = "Novo Usuario Deve Ser Adicionado Com Falha Por Erros de Command"), TestOrderProperty(4)]
        [Trait("Categoria", "Command Handler")]
        public async Task NovoUsuarioCommand_NovoUsuario_DeveAdicionarComFalhaPorErrosDeCommand()
        {
            // Arrange
            var appSetings = Options.Create(_usuarioBogusFixture.GerarAppSettings());

            var command = new NovoUsuarioCommand()
            {
                Nome = "",
                Email = "",
                Password = "",
                ConfirmPassword = "abcd@123"
            };
            var commandHandler = new AutenticacaoCommandHandler(It.IsAny<SignInManager<IdentityUser>>(), It.IsAny<UserManager<IdentityUser>>(), appSetings);

            // Act
            var result = await commandHandler.Handle(command, CancellationToken.None);

            // Assert
            var erros = result.Errors.Select(e => e.ErrorMessage);
            Assert.False(result.IsValid);
            Assert.Contains(NovoUsuarioCommandValidation.NomeRequiredErrorMessage, erros);
            Assert.Contains(NovoUsuarioCommandValidation.EmailAddressErrorMessage, erros);
            Assert.Contains(NovoUsuarioCommandValidation.EmailRequiredErrorMessage, erros);
            Assert.Contains(NovoUsuarioCommandValidation.PasswordLenghtErrorMessage, erros);
            Assert.Contains(NovoUsuarioCommandValidation.PasswordRequiredErrorMessage, erros);
            Assert.Contains(NovoUsuarioCommandValidation.ConfirmPasswordErrorMessage, erros);
        }

        [Fact(DisplayName = "Login Deve Ser Autenticado Com Sucesso"), TestOrderProperty(5)]
        [Trait("Categoria", "Command Handler")]
        public async Task LoginCommand_AutenticarUsuario_DeveAutenticarComSucesso()
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
            _signInManager.Verify(_signInManager => _signInManager.PasswordSignInAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>()), Times.AtLeastOnce);
            _userManager.Verify(_userManager => _userManager.FindByEmailAsync(command.Email), Times.AtLeastOnce);
            _userManager.Verify(_userManager => _userManager.GetClaimsAsync(It.IsAny<IdentityUser>()), Times.AtLeastOnce);
            _userManager.Verify(_userManager => _userManager.GetRolesAsync(It.IsAny<IdentityUser>()), Times.AtLeastOnce);
        }

        [Fact(DisplayName = "Login Deve Ser Autenticado Com Falha Por Travamento"), TestOrderProperty(6)]
        [Trait("Categoria", "Command Handler")]
        public async Task LoginCommand_AutenticarUsuario_DeveAutenticarComFalhaPorTravamento()
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
            var signinResult = SignInResult.LockedOut;

            _signInManager.Setup(s => s.PasswordSignInAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>()))
                .ReturnsAsync(signinResult);

            var command = _commandBogusFixture.GerarLoginCommand();
            var commandHandler = new AutenticacaoCommandHandler(_signInManager.Object, _userManager.Object, appSetings);

            // Act
            var result = await commandHandler.Handle(command, CancellationToken.None);

            // Assert
            var erros = result.Errors.Select(e => e.ErrorMessage);
            Assert.False(result.IsValid);
            Assert.Contains("Usuario temporariamente bloqueado por tentativas invalidas", erros);
            _signInManager.Verify(_signInManager => _signInManager.PasswordSignInAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>()), Times.AtLeastOnce);
        }

        [Fact(DisplayName = "Login Deve Ser Autenticado Com Falha Por Erro De Credenciais"), TestOrderProperty(7)]
        [Trait("Categoria", "Command Handler")]
        public async Task LoginCommand_AutenticarUsuario_DeveAutenticarComFalhaPorErroDeCredenciais()
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
            var signinResult = SignInResult.Failed;

            _signInManager.Setup(s => s.PasswordSignInAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>()))
                .ReturnsAsync(signinResult);

            var command = _commandBogusFixture.GerarLoginCommand();
            var commandHandler = new AutenticacaoCommandHandler(_signInManager.Object, _userManager.Object, appSetings);

            // Act
            var result = await commandHandler.Handle(command, CancellationToken.None);

            // Assert
            var erros = result.Errors.Select(e => e.ErrorMessage);
            Assert.False(result.IsValid);
            Assert.Contains("Usuario ou senha incorretos", erros);
            _signInManager.Verify(_signInManager => _signInManager.PasswordSignInAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>()), Times.AtLeastOnce);
        }

        [Fact(DisplayName = "Login Deve Ser Autenticado Com Falha Por Erros Do Command"), TestOrderProperty(8)]
        [Trait("Categoria", "Command Handler")]
        public async Task LoginCommand_AutenticarUsuario_DeveRetornarErrosDoCommand()
        {
            // Arrange
            var appSetings = Options.Create(_usuarioBogusFixture.GerarAppSettings());


            var command = new LoginCommand()
            {
                Email = "",
                Password = ""
            };
            var commandHandler = new AutenticacaoCommandHandler(It.IsAny<SignInManager<IdentityUser>>(), It.IsAny<UserManager<IdentityUser>>(), appSetings);

            // Act
            var result = await commandHandler.Handle(command, CancellationToken.None);

            // Assert
            var erros = result.Errors.Select(e => e.ErrorMessage);
            Assert.False(result.IsValid);
            Assert.Contains(LoginCommandValidation.EmailRequiredErrorMessage, erros);
            Assert.Contains(LoginCommandValidation.EmailAddressErrorMessage, erros);
            Assert.Contains(LoginCommandValidation.PasswordLenghtErrorMessage, erros);
            Assert.Contains(LoginCommandValidation.PasswordRequiredErrorMessage, erros);
        }
    }
}
