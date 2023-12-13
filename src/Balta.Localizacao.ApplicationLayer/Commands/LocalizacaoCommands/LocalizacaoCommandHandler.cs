using Balta.Localizacao.Core.DomainObjects;
using Balta.Localizacao.Core.Messages;
using Balta.Localizacao.Domain.Entities;
using Balta.Localizacao.Domain.Interfaces;
using FluentValidation.Results;
using MediatR;

namespace Balta.Localizacao.ApplicationLayer.Commands.LocalizacaoCommands
{
    public class LocalizacaoCommandHandler : CommandHandler,
                                             IRequestHandler<AdicionarMunicipioCommand, ValidationResult>,
                                             IRequestHandler<EditarMunicipioCommand, ValidationResult>,
                                             IRequestHandler<RemoverMunicipioCommand, ValidationResult>,
                                             IRequestHandler<AdicionarEstadoCommand, ValidationResult>,
                                             IRequestHandler<EditarEstadoCommand, ValidationResult>,
                                             IRequestHandler<RemoverEstadoCommand, ValidationResult>
    {
        private readonly IEstadoRepository _estadoRepository;

        public LocalizacaoCommandHandler(IEstadoRepository estadoRepository)
        {
            _estadoRepository = estadoRepository;
        }

        public async Task<ValidationResult> Handle(AdicionarMunicipioCommand message, CancellationToken cancellationToken)
        {
            if (!message.EhValido())
                return message.ValidationResult;

            var estado = ObterEstadoPorCodigoUf(message.CodigoUf);

            if (PossuiErros())
                return ValidationResult;

            var municipio = new Municipio(message.Codigo, message.Nome);

            estado.AdicionarMunicipio(municipio);

            if (!municipio.EhValido())
                ObterErrosValidacao(municipio);

            await _estadoRepository.AdicionarMunicipios(municipio);

            return PersistirDados(_estadoRepository);
        }

        public async Task<ValidationResult> Handle(EditarMunicipioCommand message, CancellationToken cancellationToken)
        {
            if (!message.EhValido())
                return ValidationResult;

            var estado = ObterEstadoPorCodigoUf(message.CodigoUf);

            if (PossuiErros())
                return ValidationResult;
            
            var municipio = ObterMunicipioPorId(message.Id);

            if (PossuiErros())
                return ValidationResult;

            if(municipio.Codigo != message.Codigo)
                municipio.AlterarCodigo(message.Codigo);


            if (municipio.CodigoUf != message.CodigoUf)
                estado.AdicionarMunicipio(municipio);
            
            
            if(municipio.Nome != message.Nome)
                municipio.AlterarNome(message.Nome);

            _estadoRepository.EditarMunicipios(municipio);

            return PersistirDados(_estadoRepository);
        }

        public async Task<ValidationResult> Handle(RemoverMunicipioCommand message, CancellationToken cancellationToken)
        {
            if (!message.EhValido())
                return ValidationResult;

            var municipio = ObterMunicipioPorId(message.Id);

            if (PossuiErros())
                return ValidationResult;

            _estadoRepository.ExcluirMunicipio(municipio);

            return PersistirDados(_estadoRepository);
        }

        public async Task<ValidationResult> Handle(AdicionarEstadoCommand message, CancellationToken cancellationToken)
        {
            if (!message.EhValido())
                return ValidationResult;

            var estado = new Estado(message.CodigoUf,message.SiglaUf,message.NomeUf);

            if (!estado.EhValido())
                ObterErrosValidacao(estado);

            await _estadoRepository.AdicionarEstado(estado);

            return PersistirDados(_estadoRepository);
        }

        public async Task<ValidationResult> Handle(EditarEstadoCommand message, CancellationToken cancellationToken)
        {
            if (!message.EhValido())
                return ValidationResult;

            var estado = ObterEstadoPorId(message.Id);

            if (PossuiErros())
                return ValidationResult;

            if (estado.CodigoUf != message.CodigoUf)
                estado.AlterarCodigoUf(message.CodigoUf);

            if(estado.SiglaUf != message.SiglaUf)
                estado.AlterarSiglaUf(message.SiglaUf);
            
            if(estado.NomeUf != message.NomeUf)
                estado.AlterarNomeUf(message.NomeUf);

            if (!estado.EhValido())
                ObterErrosValidacao(estado);

            _estadoRepository.EditarEstado(estado);

            return PersistirDados(_estadoRepository);
        }

        public async Task<ValidationResult> Handle(RemoverEstadoCommand message, CancellationToken cancellationToken)
        {
            if (!message.EhValido())
                return ValidationResult;

            var estado = ObterEstadoPorId(message.Id);

            if (PossuiErros())
                return ValidationResult;

            _estadoRepository.ExcluirEstado(estado);

            return PersistirDados(_estadoRepository);
        }

        #region Metodos_Privados

        private Estado ObterEstadoPorCodigoUf(string codigoUf)
        {
            var estado = _estadoRepository.ObterEstadoPorCodigoUf(codigoUf);

            if (estado == null)
                AdicionarErro("Estado referido nao encontrado");

            if (!estado.EhValido())
                ObterErrosValidacao(estado);

            return estado;
        }

        private Estado ObterEstadoPorId(Guid id)
        {
            var estado = _estadoRepository.ObterEstadoPorId(id);

            if (estado == null)
                AdicionarErro("Municipio nao encontrado");

            if (!estado.EhValido())
                ObterErrosValidacao(estado);

            return estado;
        }

        private void ObterErrosValidacao<T>(T entidade) where T : Entity
        {
            entidade.ValidationResult.Errors.Select(e => e.ErrorMessage).ToList().ForEach(e => AdicionarErro(e));
        }

        private Municipio ObterMunicipioPorId(Guid id)
        {
            var municipio = _estadoRepository.ObterMunicipioPorId(id);

            if (municipio == null)
                AdicionarErro("Municipio nao encontrado");

            if (!municipio.EhValido())
                ObterErrosValidacao(municipio);

            return municipio;
        }

        #endregion
    }
}
