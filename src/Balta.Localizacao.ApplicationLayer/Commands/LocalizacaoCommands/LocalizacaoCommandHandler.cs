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

            var municipio = CriarMunicipio(message.Codigo, message.Nome, estado);

            if (PossuiErros())
                return ValidationResult;

            await _estadoRepository.AdicionarMunicipios(municipio);

            return PersistirDados(_estadoRepository);
        }

        public async Task<ValidationResult> Handle(EditarMunicipioCommand message, CancellationToken cancellationToken)
        {
            if (!message.EhValido())
                return message.ValidationResult;

            var estado = ObterEstadoPorCodigoUf(message.CodigoUf);

            if (PossuiErros())
                return ValidationResult;
            
            var municipio = ObterMunicipioPorId(message.Id);

            if (PossuiErros())
                return ValidationResult;

            if(PossuiErros())
                return ValidationResult;

            municipio.AlterarMunicipio(message.Codigo, message.Nome, estado.CodigoUf);

            _estadoRepository.EditarMunicipios(municipio);

            return PersistirDados(_estadoRepository);
        }
        
        public async Task<ValidationResult> Handle(RemoverMunicipioCommand message, CancellationToken cancellationToken)
        {
            if (!message.EhValido())
                return message.ValidationResult;

            var municipio = ObterMunicipioPorId(message.Id);

            if (PossuiErros())
                return ValidationResult;

            _estadoRepository.ExcluirMunicipio(municipio);

            return PersistirDados(_estadoRepository);
        }

        public async Task<ValidationResult> Handle(AdicionarEstadoCommand message, CancellationToken cancellationToken)
        {
            if (!message.EhValido())
                return message.ValidationResult;

            var estado = CriarNovoEstado(message.CodigoUf, message.SiglaUf, message.NomeUf);

            await _estadoRepository.AdicionarEstado(estado);

            return PersistirDados(_estadoRepository);
        }

        public async Task<ValidationResult> Handle(EditarEstadoCommand message, CancellationToken cancellationToken)
        {
            if (!message.EhValido())
                return message.ValidationResult;

            var estado = ObterEstadoPorId(message.Id);

            if (PossuiErros())
                return ValidationResult;

            estado.AlterarEstado(message.CodigoUf, message.SiglaUf, message.NomeUf);

            _estadoRepository.EditarEstado(estado);

            return PersistirDados(_estadoRepository);
        }

        public async Task<ValidationResult> Handle(RemoverEstadoCommand message, CancellationToken cancellationToken)
        {
            if (!message.EhValido())
                return message.ValidationResult;

            var estado = ObterEstadoPorId(message.Id);

            if (PossuiErros())
                return ValidationResult;

            _estadoRepository.ExcluirEstado(estado);

            return PersistirDados(_estadoRepository);
        }

        #region Metodos_Privados
       
        #region Estado

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

        private Estado CriarNovoEstado(string codigoUf, string siglaUf, string nomeUf)
        {
            var novoEstado = new Estado(codigoUf, siglaUf, nomeUf);

            if (!novoEstado.EhValido())
                ObterErrosValidacao(novoEstado);

            return novoEstado;
        }

        #endregion
        #region Municipio

        private Municipio ObterMunicipioPorId(Guid id)
        {
            var municipio = _estadoRepository.ObterMunicipioPorId(id);

            if (municipio == null)
                AdicionarErro("Municipio nao encontrado");

            if (!municipio.EhValido())
                ObterErrosValidacao(municipio);

            return municipio;
        }

        private Municipio CriarMunicipio(string codigo, string nome, Estado estado)
        {
            var novoMunicipio = new Municipio(codigo, nome);

            estado.AdicionarMunicipio(novoMunicipio);

            if (!novoMunicipio.EhValido())
                ObterErrosValidacao(novoMunicipio);

            return novoMunicipio;
        }

        #endregion

        #endregion
    }
}
