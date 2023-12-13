using Balta.Localizacao.Core.DomainObjects;
using Balta.Localizacao.Domain.Entities;
using Balta.Localizacao.Domain.Entities.Validations;

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
        
        [Fact(DisplayName = "Instanciar Municipio Com Falha")]
        [Trait("Categoria", "Entity")]
        public void InstanciarMunicipio_NovoMunicipio_DeveInstanciarMunicipioComFalha()
        {
            // Arrange
            var municipio = Municipio.MunicipioFactory.CriarMunicipioVazio();

            // Act
            var result = municipio.EhValido();

            // Assert
            Assert.False(result);
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

        [Fact(DisplayName = "Editar Municipio Editar Codigo Com Sucesso")]
        [Trait("Categoria", "Entity")]
        public void EditarMunicipio_EditarCodigo_DeveEditarCodigoComSucesso()
        {
            // Arrange
            var estado = new Estado("11", "RO", "Rondônia");
            var municipio = new Municipio("1100000", "Alta Floresta D'Oeste");
            municipio.AssociarEstado(estado);

            var novoMunicipio = new Municipio("1100015", "Alta Floresta D'Oeste");

            // Act
            municipio.AlterarMunicipio(novoMunicipio);

            // Assert
            Assert.Equal(municipio.Codigo, novoMunicipio.Codigo);
        }
        
        [Fact(DisplayName = "Editar Municipio Editar Codigo Com Falha")]
        [Trait("Categoria", "Entity")]
        public void EditarMunicipio_EditarCodigo_DeveRetornarException()
        {
            // Arrange
            var estado = new Estado("11", "RO", "Rondônia");
            var municipio = new Municipio("1100015", "Alta Floresta D'Oeste");
            municipio.AssociarEstado(estado);

            var novoMunicipio = new Municipio("3500015", "Alta Floresta D'Oeste");

            // Act && Assert
            var exception = Assert.Throws<DomainException>(() => municipio.AlterarMunicipio(novoMunicipio));
            Assert.Equal("Codigo municipio e codigo estado nao sao compativeis.", exception.Message);
        }

        [Fact(DisplayName = "Editar Municipio Editar Nome Com Sucesso")]
        [Trait("Categoria", "Entity")]
        public void EditarMunicipio_EditarNome_DeveEditarNomeComSucesso()
        {
            // Arrange
            var estado = new Estado("11", "RO", "Rondônia");
            var municipio = new Municipio("1100015", "Alta Floresta");
            municipio.AssociarEstado(estado);

            var novoMunicipio = new Municipio("1100015", "Alta Floresta D'Oeste");

            // Act
            municipio.AlterarMunicipio(novoMunicipio);

            // Assert
            Assert.Equal(municipio.Nome, novoMunicipio.Nome);
        }

        [Fact(DisplayName = "Editar Municipio Editar Nome Com Falha")]
        [Trait("Categoria", "Entity")]
        public void EditarMunicipio_EditarNome_NaoDeveEditarNome()
        {
            // Arrange
            var estado = new Estado("11", "RO", "Rondônia");
            var municipio = new Municipio("1100015", "Alta Floresta");
            municipio.AssociarEstado(estado);

            var novoMunicipio = new Municipio("1100015", "");

            // Act
            municipio.AlterarMunicipio(novoMunicipio);

            // Assert
            Assert.NotEqual(municipio.Nome, novoMunicipio.Nome);
        }

        [Fact(DisplayName = "Editar Municipio Editar CodigoUf Com Sucesso")]
        [Trait("Categoria", "Entity")]
        public void EditarMunicipio_EditarNome_DeveEditarCodigoUfComSucesso()
        {
            // Arrange
            var estado = new Estado("11", "RO", "Rondônia");
            var municipio = new Municipio("1100015", "Alta Floresta");
            municipio.AssociarEstado(estado);

            var novoEstado = new Estado("35", "SP", "Sao Paulo");
            var novoMunicipio = new Municipio("3500105", "Adamantina");

            novoEstado.AdicionarMunicipio(novoMunicipio);

            // Act
            municipio.AlterarMunicipio(novoMunicipio);

            // Assert
            Assert.Equal(novoMunicipio.CodigoUf, municipio.CodigoUf);
        }
    }
}