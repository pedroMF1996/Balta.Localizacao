using Balta.Localizacao.Domain.Entities;
using Balta.Localizacao.Domain.Entities.Validations;
using Xunit;

namespace Balta.Localizacao.Domain.Testes
{
    public class MunicipioTestes
    {
        [Fact(DisplayName = "Instanciar Municipio Com Falha Sem Estado")]
        [Trait("Categoria", "Entity")]
        public void InstanciarMunicipio_NovoMunicipio_DeveInstanciarMunicipioComFalhaSemEstado()
        {
            // Arrange
            var municipio = new Municipio("1100015", "Alta Floresta D'Oeste");

            // Act
            var result = municipio.EhValido();

            // Assert
            var erros = municipio.ValidationResult.Errors.Select(e => e.ErrorMessage);
            Assert.False(result);
            Assert.Contains(MunicipioValidation.CodigoUfLengthErrorMessage, erros);
            Assert.Contains(MunicipioValidation.CodigoUfRequiredErrorMessage, erros);
        }
        
        [Fact(DisplayName = "Instanciar Municipio Com Erros De Validacao")]
        [Trait("Categoria", "Entity")]
        public void InstanciarMunicipio_NovoMunicipio_DeveInstanciarMunicipioComErrosDeValidacao()
        {
            // Arrange
            var municipio = new Municipio("", "");

            // Act
            var result = municipio.EhValido();

            // Assert
            var erros = municipio.ValidationResult.Errors.Select(e => e.ErrorMessage);
            Assert.False(result);
            Assert.Contains(MunicipioValidation.CodigoLengthErrorMessage, erros);
            Assert.Contains(MunicipioValidation.CodigoRequiredErrorMessage, erros);
            Assert.Contains(MunicipioValidation.NomeRequiredErrorMessage, erros);
            Assert.Contains(MunicipioValidation.CodigoUfLengthErrorMessage, erros);
            Assert.Contains(MunicipioValidation.CodigoUfRequiredErrorMessage, erros);
        }

        [Fact(DisplayName = "Associar Estado A Municipio Com Sucesso")]
        [Trait("Categoria", "Entity")]
        public void AssociarEstadoAMunicipio_AssociarEstado_DeveAssociarEstadoComSucesso()
        {
            // Arrange
            var estado = new Estado("11", "RO", "Rondônia");
            var municipio = new Municipio("1100015", "Alta Floresta D'Oeste");

            // Act
            municipio.AssociarEstado(estado);

            // Assert
            Assert.True(municipio.EhValido());
        }
    }
}