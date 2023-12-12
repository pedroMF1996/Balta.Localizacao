using FluentValidation.Results;
using MediatR;

namespace Balta.Localizacao.Core.Messages
{
    public abstract class Command : Message, IRequest<ValidationResult>
    {
        public DateTime TimeStamp { get; private set; }
        public ValidationResult ValidationResult { get;  set; }
        protected Command() : base(nameof(Command))
        {
            ValidationResult = new ValidationResult();
        }

        public virtual bool EhValido()
        {
            return ValidationResult.IsValid;
        }
    }

}
