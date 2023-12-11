using Balta.Localizacao.Core.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Balta.Localizacao.Domain.Entities
{
    internal class Estado : Entity, IAggregateRoot
    {
        public string CodigoUf { get; private set; }
        public string SiglaUf { get; private set; }
        public string NomeUf { get; private set; }
        public List<Municipio> Municipios { get; private set; } = new();

        protected Estado()
        {

        }
        
        public Estado(string codigoUf, string siglaUf, string nomeUf)
        {
            CodigoUf = codigoUf;
            SiglaUf = siglaUf;
            NomeUf = nomeUf;
        }

        public void AdicionarMunicipio(Municipio municipio)
        {

        }

        public override bool EhValido()
        {
            return base.EhValido();
        }
    }
}
