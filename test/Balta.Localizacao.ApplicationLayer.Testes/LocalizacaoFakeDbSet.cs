using Balta.Localizacao.Core.Data;
using Balta.Localizacao.DAL.DbContexts;
using Balta.Localizacao.Domain.Entities;
using Balta.Localizacao.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Moq.AutoMock;
using System.Collections;
using System.Collections.ObjectModel;
using System.Linq.Expressions;

namespace Balta.Localizacao.ApplicationLayer.Testes
{
    public class LocalizacaoFakeDbSet<T> : DbSet<T> where T : class
    {
        ObservableCollection<T> _data;
        IQueryable _query;
        public override IEntityType EntityType => throw new NotImplementedException();

        public LocalizacaoFakeDbSet()
        {
            _data = new ObservableCollection<T>();
            _query = _data.AsQueryable();
        }

        public virtual T Find(params object[] keyValues)
        {
            throw new NotImplementedException("Derive from FakeDbSet<T> and override Find");
        }

        public virtual T Add(T item)
        {
            _data.Add(item);
            return item;
        }
        
        public virtual async Task AddAsync(T item)
        {
            _data.Add(item);
        }

        public virtual T Remove(T item)
        {
            _data.Remove(item);
            return item;
        }
        
        public virtual void Update(T item)
        {
            
        }

        public virtual T Attach(T item)
        {
            _data.Add(item);
            return item;
        }

        public virtual T Detach(T item)
        {
            _data.Remove(item);
            return item;
        }

        public virtual T Create()
        {
            return Activator.CreateInstance<T>();
        }

        public virtual TDerivedEntity Create<TDerivedEntity>() where TDerivedEntity : class, T
        {
            return Activator.CreateInstance<TDerivedEntity>();
        }

        public virtual ObservableCollection<T> Local
        {
            get { return _data; }
        }

        Type ElementType
        {
            get { return _query.ElementType; }
        }

        Expression Expression
        {
            get { return _query.Expression; }
        }

        IQueryProvider Provider
        {
            get { return _query.Provider; }
        }

        IEnumerator GetEnumerator()
        {
            return _data.GetEnumerator();
        }
    }

    public class LocalizacaoFakeDbContext : IUnitOfWork
    {

        public LocalizacaoFakeDbSet<Estado> Estados { get; set; } = new();
        public LocalizacaoFakeDbSet<Municipio> Municipios { get; set; } = new();

        public bool Commit()
        {
            return true;
        }
    }

    public class EstadoFakeRepository : IEstadoRepository
    {
        private readonly LocalizacaoFakeDbContext _localizacaoFakeDbContext;
        public EstadoFakeRepository()
        {
            var autoMocker = new AutoMocker();
            _localizacaoFakeDbContext = autoMocker.CreateInstance<LocalizacaoFakeDbContext>();
        }

        public virtual IUnitOfWork unitOfWork => _localizacaoFakeDbContext;

        public virtual async Task AdicionarEstado(Estado estado)
        {
            await _localizacaoFakeDbContext.Estados.AddAsync(estado);
        }

        public virtual async Task AdicionarMunicipios(Municipio municipio)
        {
            await _localizacaoFakeDbContext.Municipios.AddAsync(municipio);
        }

        public virtual void EditarEstado(Estado estado)
        {
            _localizacaoFakeDbContext.Estados.Update(estado);

            if (estado.Municipios.Count > 0)
                estado.Municipios.ForEach(m => _localizacaoFakeDbContext.Municipios.Update(m));
        }

        public virtual void EditarMunicipios(Municipio municipio)
        {
            _localizacaoFakeDbContext.Municipios.Update(municipio);
        }

        public virtual void ExcluirEstado(Estado estado)
        {
            _localizacaoFakeDbContext.Estados.Remove(estado);
        }

        public virtual void ExcluirMunicipio(Municipio municipio)
        {
            _localizacaoFakeDbContext.Municipios.Remove(municipio);
        }

        public virtual Estado ObterEstadoPorCodigoUf(string codigoUf)
        {
            return _localizacaoFakeDbContext.Estados.FirstOrDefault(e => e.CodigoUf == codigoUf);
        }

        public virtual Estado ObterEstadoPorId(Guid id)
        {
            return _localizacaoFakeDbContext.Estados.Include(e => e.Municipios).FirstOrDefault(e => e.Id == id);
        }

        public virtual Municipio ObterMunicipioPorId(Guid id)
        {
            return _localizacaoFakeDbContext.Municipios.FirstOrDefault(e => e.Id == id);
        }
    }
}
