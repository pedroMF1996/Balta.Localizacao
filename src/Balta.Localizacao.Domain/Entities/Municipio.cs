using Balta.Localizacao.Core.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Balta.Localizacao.Domain.Entities
{
    internal class Municipio : Entity
    {
        public string Codigo { get; private set; }
        public string Nome { get; private set; }
        public string CodigoUf { get; private set; }
        public Estado Estado { get; private set; }

        public Municipio()
        {
        }

        public Municipio(string codigo, string nome, string codigoUf)
        {
            Codigo = codigo;
            Nome = nome;
            CodigoUf = codigoUf;
        }

        public void AssociarEstado(Estado estado)
        {

        }

        public override bool EhValido()
        {
            return base.EhValido();
        }
    }
}
