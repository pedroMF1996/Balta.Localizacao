using Balta.Localizacao.Domain.DomainObjects;

namespace Balta.Localizacao.ApplicationLayer.ViewModels
{
    public class LoginResponseViewModel
    {
        public string AccessToken { get; set; }
        public double ExpiresIn { get; set; }
        public UserTokenModel UserToken { get; set; }
    }
}
