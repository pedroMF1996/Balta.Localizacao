using Balta.Localizacao.Core.Data;
using Balta.Localizacao.Core.DomainObjects;
using FluentValidation.Results;

namespace Balta.Localizacao.Core.Messages
{
    public abstract class CommandHandler
    {
        public ValidationResult ValidationResult { get; private set; }

        protected CommandHandler() 
        {
            ValidationResult = new ValidationResult();
        }

        public virtual void AdicionarErro(string msgErro) 
        {
            ValidationResult.Errors.Add(new ValidationFailure(nameof(CommandHandler), msgErro));
        }

        public virtual bool PersistirDados<T>(IRepository<T> repository) where T : IAggregateRoot
        {
            if (!repository.unitOfWork.Commit())
                AdicionarErro("Erro ao persistir dados");

            return true;
        }
    }

}
