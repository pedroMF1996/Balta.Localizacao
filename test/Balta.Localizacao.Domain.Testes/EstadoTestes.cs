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
    }
}