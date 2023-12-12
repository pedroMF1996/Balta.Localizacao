using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Balta.Localizacao.Domain.Entities.Validations
{
    internal class MunicipioValidation : AbstractValidator<Municipio>
    {
        public MunicipioValidation()
        {
            RuleFor(x => x.Codigo).NotNull().NotEmpty().Length(7).WithMessage("O código do município deve conter 7 caracteres.");
            RuleFor(x => x.Nome).NotNull().NotEmpty();
            RuleFor(x => x.CodigoUf).NotNull().NotEmpty().Length(2).WithMessage("O código UF deve conter 2 caracteres");
        }
    }
}
