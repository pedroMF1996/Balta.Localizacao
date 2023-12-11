using Balta.Localizacao.ApplicationLayer.ViewModels;
using Balta.Localizacao.Core.Identity;
using Balta.Localizacao.Core.Messages;
using Balta.Localizacao.Domain.DomainObjects;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Balta.Localizacao.ApplicationLayer.Commands.AutenticacaoCommands
{
    public class AutenticacaoCommandHandler : CommandHandler,
                                              IRequestHandler<NovoUsuarioCommand, ValidationResult>,
                                              IRequestHandler<LoginCommand, ValidationResult>
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly AppSetings _appSettings;

        public AutenticacaoCommandHandler(SignInManager<IdentityUser> signInManager,
                                          UserManager<IdentityUser> userManager,
                                          IOptions<AppSetings> appSettings)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _appSettings = appSettings.Value;
        }

        public async Task<ValidationResult> Handle(NovoUsuarioCommand message, CancellationToken cancellationToken)
        {
            if (!message.EhValido())
                return message.ValidationResult;

            var user = new IdentityUser()
            {
                UserName = message.Email,
                Email = message.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, message.Password);

            if (UsuarioCriado(result))
            {
                var jwt = await GerarJWT(message.Email);

                return ValidationResult;
            }

            foreach (var error in result.Errors)
            {
                AdicionarErro(error.Description);
            }

            return ValidationResult;
        }

        public async Task<ValidationResult> Handle(LoginCommand message, CancellationToken cancellationToken)
        {
            if (!message.EhValido())
                return message.ValidationResult;

            var result = await _signInManager.PasswordSignInAsync(message.Email, message.Password, false, true);

            if (result.Succeeded)
            {
                await GerarJWT(message.Email);
            }

            if (result.IsLockedOut)
            {
                AdicionarErro("Usuario temporariamente bloqueado por tentativas invalidas");
                return ValidationResult;
            }

            AdicionarErro("Usuario ou senha incorretos");

            return ValidationResult;
        }

        #region Metodos_Privados
        private async Task<LoginResponseViewModel> GerarJWT(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            var claims = await _userManager.GetClaimsAsync(user);

            var identityClaims = await ObterClaimsUsuarioAsync(user, claims);

            var encodedToken = CodificarToken(identityClaims);

            return ObterRespostaToken(encodedToken, user, claims);
        }

        private async Task<ClaimsIdentity> ObterClaimsUsuarioAsync(IdentityUser user, IList<Claim> claims)
        {
            var roles = await _userManager.GetRolesAsync(user);

            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, ToUnixEpochDate(DateTime.UtcNow).ToString())); 
            claims.Add(new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString(), ClaimValueTypes.Integer64)); 

            foreach (var role in roles)
            {
                claims.Add(new Claim("role", role));
            }

            return new ClaimsIdentity(claims);
        }

        private string CodificarToken(ClaimsIdentity identityClaims)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);

            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor()
            {
                Issuer = _appSettings.Emissor,
                Audience = _appSettings.ValidoEm,
                Subject = identityClaims,
                Expires = DateTime.UtcNow.AddHours(_appSettings.ExpiracaoHoras),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            });

            return tokenHandler.WriteToken(token);
        }

        private LoginResponseViewModel ObterRespostaToken(string encodedToken, IdentityUser user, IEnumerable<Claim> claims)
        {
            return new LoginResponseViewModel()
            {
                AccessToken = encodedToken,
                ExpiresIn = TimeSpan.FromHours(_appSettings.ExpiracaoHoras).TotalSeconds,
                UserToken = new UserTokenModel()
                {
                    Id = user.Id,
                    Email = user.Email,
                    Claims = claims.Select(c => new ClaimModel() { Type = c.Type, Value = c.Value }).ToList()
                }
            };
        }

        private static long ToUnixEpochDate(DateTime utcNow)
        {
            return (long)Math.Round((utcNow.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);
        }

        public bool UsuarioCriado(IdentityResult result)
        {
            //return result.Succeeded;
            return true;
        }
        #endregion
    }
}
