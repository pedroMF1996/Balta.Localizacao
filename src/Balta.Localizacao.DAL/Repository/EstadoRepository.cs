﻿using Balta.Localizacao.Core.Data;
using Balta.Localizacao.DAL.DbContexts;
using Balta.Localizacao.Domain.Entities;
using Balta.Localizacao.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Balta.Localizacao.DAL.Repository
{
    public class EstadoRepository : IEstadoRepository
    {

        private readonly LocalizacaoDbContext _localizacaoDbContext;

        public EstadoRepository(LocalizacaoDbContext localizacaoDbContext)
        {
            _localizacaoDbContext = localizacaoDbContext;
        }

        public IUnitOfWork unitOfWork => _localizacaoDbContext;

        public async Task AdicionarEstado(Estado estado)
        {
            await _localizacaoDbContext.Estados.AddAsync(estado);
        }

        public async Task AdicionarMunicipios(Municipio municipio)
        {
            await _localizacaoDbContext.Municipios.AddAsync(municipio);
        }

        public void EditarEstado(Estado estado)
        {
            _localizacaoDbContext.Estados.Update(estado);

            if(estado.Municipios.Count  > 0)
                estado.Municipios.ForEach(m => _localizacaoDbContext.Municipios.Update(m));
        }

        public void EditarMunicipios(Municipio municipio)
        {
            _localizacaoDbContext.Municipios.Update(municipio);
        }

        public void ExcluirEstado(Estado estado)
        {
            _localizacaoDbContext.Estados.Remove(estado);
        }

        public void ExcluirMunicipio(Municipio municipio)
        {
            _localizacaoDbContext.Municipios.Remove(municipio);
        }

        public Estado ObterEstadoPorCodigoUf(string codigoUf)
        {
            return _localizacaoDbContext.Estados.FirstOrDefault(e => e.CodigoUf == codigoUf);
        }

        public Estado ObterEstadoPorId(Guid id)
        {
            return _localizacaoDbContext.Estados.Include(e => e.Municipios).FirstOrDefault(e => e.Id == id);
        }

        public Municipio ObterMunicipioPorId(Guid id)
        {
            return _localizacaoDbContext.Municipios.FirstOrDefault(e => e.Id == id);
        }
    }
}
