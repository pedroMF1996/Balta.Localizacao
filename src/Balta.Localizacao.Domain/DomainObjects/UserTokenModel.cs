﻿namespace Balta.Localizacao.Domain.DomainObjects
{
    public class UserTokenModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public List<ClaimModel> Claims { get; set; }

        public UserTokenModel()
        {
            
        }
    }
}
