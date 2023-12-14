using Balta.Localizacao.Core.DomainObjects;
using Balta.Localizacao.Domain.Entities;
using Balta.Localizacao.Domain.Entities.Validations;

namespace Balta.Localizacao.Domain.Testes
{
    public class EstadoTestes
    {
        [Fact(DisplayName = "Instanciar Estado Com Sucesso")]
        [Trait("Categoria", "Entity")]
        public void InstanciarEstado_NovoEstado_DeveInstanciarEstadoComSucesso()
        {
            // Arrange
            var estado = new Estado("11", "RO", "Rondônia");

            // Act
            var result = estado.EhValido();

            // Assert
            Assert.True(result);
        }
        
        [Fact(DisplayName = "Instanciar Estado Vazio Com Sucesso")]
        [Trait("Categoria", "Entity")]
        public void InstanciarEstado_NovoEstado_DeveInstanciarEstadoVazioComSucesso()
        {
            // Arrange
            var estado = Estado.EstadoFactory.CriarEstadoVazio();

            // Act
            var result = estado.EhValido();

            // Assert
            Assert.False(result);
        }
        
        [Fact(DisplayName = "Instanciar Estado Com Erros De Validacao")]
        [Trait("Categoria", "Entity")]
        public void InstanciarEstado_NovoEstado_DeveInstanciarEstadoComErrosDeValidacao()
        {
            // Arrange
            var estado = new Estado("", "", "");

            // Act
            var result = estado.EhValido();

            // Assert
            var erros = estado.ValidationResult.Errors.Select(e => e.ErrorMessage);
            Assert.False(result);
            Assert.Contains(EstadoValidation.SiglaUfLengthErrorMessage, erros);
            Assert.Contains(EstadoValidation.SiglaUfRequiredErrorMessage, erros);
            Assert.Contains(EstadoValidation.NomeRequiredErrorMessage, erros);
            Assert.Contains(EstadoValidation.CodigoUfLengthErrorMessage, erros);
            Assert.Contains(EstadoValidation.CodigoUfRequiredErrorMessage, erros);
        }
        
        [Fact(DisplayName = "Adicionar Municipio A Estado Com Sucesso")]
        [Trait("Categoria", "Entity")]
        public void AdicionarMunicipioAEstado_NovoEstado_DeveInstanciarEstadoComSucesso()
        {
            // Arrange
            var municipio = new Municipio("1100015", "Alta Floresta D'Oeste");
            var estado = new Estado("11", "RO", "Rondônia");

            // Act
            estado.AdicionarMunicipio(municipio);

            // Assert
            Assert.True(estado.EhValido());
        }
        
        
        [Fact(DisplayName = "Nao Deve Adicionar Municipio Repetido")]
        [Trait("Categoria", "Entity")]
        public void AdicionarMunicipioAEstado_MunicipioRepetido_NaoDeveAdicionarMunicipioRepetido()
        {
            // Arrange
            var municipio = new Municipio("1100015", "Alta Floresta D'Oeste");
            var estado = new Estado("11", "RO", "Rondônia");
            estado.AdicionarMunicipio(municipio);

            // Act 
            estado.AdicionarMunicipio(municipio);
            
            // Assert
            Assert.Equal(1, estado.Municipios.Count);
        }

        [Fact(DisplayName = "Editar Estado SiglaUf Com Sucesso")]
        [Trait("Categoria", "Entity")]
        public void EditarEstadoSiglaUf_EditarEstado_DeveEditarEstadoComSucesso()
        {
            // Arrange
            var estado = new Estado("11", "RO", "Rondônia");

            // Act
            estado.AlterarEstado("11", "SP", "Rondônia");

            // Assert
            Assert.Equal(estado.SiglaUf, "SP");
        }
        
        [Fact(DisplayName = "Nao Deve Editar Estado SiglaUf")]
        [Trait("Categoria", "Entity")]
        public void EditarEstadoSiglaUf_EditarEstado_NaoDeveEditarEstado()
        {
            // Arrange
            var estado = new Estado("11", "RO", "Rondônia");

            // Act
            estado.AlterarEstado("11", "", "Rondônia");

            // Assert
            Assert.NotEqual(estado.SiglaUf, "");
        }

        [Fact(DisplayName = "Editar Estado CodigoUf Com Sucesso")]
        [Trait("Categoria", "Entity")]
        public void EditarEstadoCodigoUf_EditarEstado_DeveEditarEstadoComSucesso()
        {
            // Arrange
            var estado = new Estado("13", "RO", "Rondônia");

            // Act
            estado.AlterarEstado("11", "RO", "Rondônia");

            // Assert
            Assert.Equal(estado.CodigoUf, "11");
        }
        [Fact(DisplayName = "Nao Deve Editar Estado CodigoUf")]
        [Trait("Categoria", "Entity")]
        public void EditarEstadoCodigoUf_EditarEstado_NaoDeveEditarEstado()
        {
            // Arrange
            var estado = new Estado("11", "RO", "Rondônia");

            // Act
            estado.AlterarEstado("11", "RO", "Rondônia");

            // Assert
            Assert.NotEqual(estado.CodigoUf, "");
        }
        
        [Fact(DisplayName = "Editar Estado NomeUf Com Sucesso")]
        [Trait("Categoria", "Entity")]
        public void EditarEstadoNomeUf_EditarEstado_DeveEditarEstadoComSucesso()
        {
            // Arrange
            var estado = new Estado("11", "RO", "Rndônia");

            // Act
            estado.AlterarEstado("11", "RO", "Rondônia");

            // Assert
            Assert.Equal(estado.NomeUf, "Rondônia");
        }
        
    }
}