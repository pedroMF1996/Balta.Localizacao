using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Balta.Localizacao.Core.DomainObjects
{
    public abstract class Entity
    {
        public Guid Id { get; private set; }
        public ValidationResult ValidationResult { get; private set; }

        protected Entity()
        {
            Id = Guid.NewGuid();
            ValidationResult = new ValidationResult();
        }

        public virtual bool EhValido()
        {
            return ValidationResult.IsValid;
        }
    }
}
