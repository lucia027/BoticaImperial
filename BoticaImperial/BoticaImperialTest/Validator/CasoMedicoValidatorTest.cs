using BoticaImperial.Enum;
using BoticaImperial.Errors;
using BoticaImperial.Models;
using BoticaImperial.Validator;
using FluentAssertions;

namespace BoticaImperialTest.Validator;

[TestFixture]
public class CasoMedicoValidatorTest {

    [TestFixture]
    public sealed class CasosPositivos {

        [SetUp]
        public void SetUp() {
            _validador = new CasoMedicoValidator();
        }

        private CasoMedicoValidator _validador = null!;

        [Test]
        public void Validate_CasoMedicoValido_RetornaSuccess() {
            //Arrange
            var casoMedico = new CasoMedico { TituloCaso = "Caso", Sintomas = "Sintomas", FechaInicio = DateTime.Now, Gravedad = Gravedad.Critica, CausaSospecha = CausaSospecha.Desconocida, SustanciasSospechosas = null, TratamientosAplicados = null, Estado = EstadoCasoMedico.Archivado};
            
            //Act
            var res = _validador.Validate(casoMedico);
            
            //Assert
            res.IsSuccess.Should().BeTrue();
        }

        [TestCase(Gravedad.Nula)]
        [TestCase(Gravedad.Leve)]
        [TestCase(Gravedad.Moderada)]
        [TestCase(Gravedad.Grave)]
        [TestCase(Gravedad.Critica)]
        public void Validate_TiposGravedad_RetornaSuccess(Gravedad gravedad) {
            //Arrange
            var casoMedico = new CasoMedico { TituloCaso = "Caso", Sintomas = "Sintomas", FechaInicio = DateTime.Now, Gravedad = gravedad, CausaSospecha = CausaSospecha.Desconocida, SustanciasSospechosas = null, TratamientosAplicados = null, Estado = EstadoCasoMedico.Archivado};
            
            //Act
            var res = _validador.Validate(casoMedico);
            
            //Assert
            res.IsSuccess.Should().BeTrue();
        }
        
        [TestCase(CausaSospecha.Enfermedad)]
        [TestCase(CausaSospecha.Veneno)]
        [TestCase(CausaSospecha.ReaccionAdversa)]
        [TestCase(CausaSospecha.Desconocida)]
        public void Validate_TiposCausaSospecha_RetornaSuccess(CausaSospecha causaSospecha) {
            //Arrange
            var casoMedico = new CasoMedico { TituloCaso = "Caso", Sintomas = "Sintomas", FechaInicio = DateTime.Now, Gravedad = Gravedad.Critica, CausaSospecha = causaSospecha, SustanciasSospechosas = causaSospecha == CausaSospecha.Veneno ? new HashSet<Veneno>() { new Veneno() } : null, TratamientosAplicados = null, Estado = EstadoCasoMedico.Archivado};
            
            //Act
            var res = _validador.Validate(casoMedico);
            
            //Assert
            res.IsSuccess.Should().BeTrue();
        }
        
        [TestCase(EstadoCasoMedico.Abierto)]
        [TestCase(EstadoCasoMedico.EnInvestigacion)]
        [TestCase(EstadoCasoMedico.Resuelto)]
        [TestCase(EstadoCasoMedico.Archivado)]
        public void Validate_TiposEstados_RetornaSuccess(EstadoCasoMedico estado) {
            //Arrange
            var casoMedico = new CasoMedico { TituloCaso = "Caso", Sintomas = "Sintomas", FechaInicio = DateTime.Now, Gravedad = Gravedad.Critica, CausaSospecha = CausaSospecha.Desconocida, SustanciasSospechosas = null, TratamientosAplicados = null, Estado = estado};
            
            //Act
            var res = _validador.Validate(casoMedico);
            
            //Assert
            res.IsSuccess.Should().BeTrue();
        }
    }

    [TestFixture]
    public sealed class CasosNegativos {
        
        [SetUp]
        public void SetUp() {
            _validador = new CasoMedicoValidator();
        }

        private CasoMedicoValidator _validador = null!;

        [Test]
        public void Validate_TituloBlancoONulo_RetornaFailure() {
            //Arrange
            var casoMedico = new CasoMedico { TituloCaso = "", Sintomas = "Sintomas", FechaInicio = DateTime.Now, Gravedad = Gravedad.Critica, CausaSospecha = CausaSospecha.Desconocida, SustanciasSospechosas = null, TratamientosAplicados = null, Estado = EstadoCasoMedico.Archivado};
            
            //Act
            var res = _validador.Validate(casoMedico);
            
            //Assert
            res.IsFailure.Should().BeTrue();
            res.Error.Should().BeOfType<CasoMedicoError.Validation>();

            var message = res.Error as CasoMedicoError.Validation;
            message!.Errores.Should().Contain("ERROR - Titulo del caso en blanco o nulo invalido.");
        }
        
        [Test]
        public void Validate_SintomasBlancoONulo_RetornaFailure() {
            //Arrange
            var casoMedico = new CasoMedico { TituloCaso = "titulo", Sintomas = "", FechaInicio = DateTime.Now, Gravedad = Gravedad.Critica, CausaSospecha = CausaSospecha.Desconocida, SustanciasSospechosas = null, TratamientosAplicados = null, Estado = EstadoCasoMedico.Archivado};
            
            //Act
            var res = _validador.Validate(casoMedico);
            
            //Assert
            res.IsFailure.Should().BeTrue();
            res.Error.Should().BeOfType<CasoMedicoError.Validation>();

            var message = res.Error as CasoMedicoError.Validation;
            message!.Errores.Should().Contain("ERROR - Sintomas en blanco o nulo invalido.");
        }
        
        [Test]
        public void Validate_FechaInicioAnteriorActual_RetornaFailure() {
            //Arrange
            var casoMedico = new CasoMedico { TituloCaso = "titulo", Sintomas = "sintomas", FechaInicio = DateTime.Now.AddDays(-100), Gravedad = Gravedad.Critica, CausaSospecha = CausaSospecha.Desconocida, SustanciasSospechosas = null, TratamientosAplicados = null, Estado = EstadoCasoMedico.Archivado};
            
            //Act
            var res = _validador.Validate(casoMedico);
            
            //Assert
            res.IsFailure.Should().BeTrue();
            res.Error.Should().BeOfType<CasoMedicoError.Validation>();

            var message = res.Error as CasoMedicoError.Validation;
            message!.Errores.Should().Contain("ERROR - Fecha de inicio anterior a la de hoy invalida.");
        }
        
        [Test]
        public void Validate_TipoGravedadInvalida_RetornaFailure() {
            //Arrange
            var casoMedico = new CasoMedico { TituloCaso = "titulo", Sintomas = "sintomas", FechaInicio = DateTime.Now, Gravedad = (Gravedad)99, CausaSospecha = CausaSospecha.Desconocida, SustanciasSospechosas = null, TratamientosAplicados = null, Estado = EstadoCasoMedico.Archivado};
            
            //Act
            var res = _validador.Validate(casoMedico);
            
            //Assert
            res.IsFailure.Should().BeTrue();
            res.Error.Should().BeOfType<CasoMedicoError.Validation>();

            var message = res.Error as CasoMedicoError.Validation;
            message!.Errores.Should().Contain("ERROR - Tipo de gravedad invalido.");
        }
        
        [Test]
        public void Validate_TipoCausaSospechaInvalida_RetornaFailure() {
            //Arrange
            var casoMedico = new CasoMedico { TituloCaso = "titulo", Sintomas = "sintomas", FechaInicio = DateTime.Now, Gravedad = Gravedad.Critica, CausaSospecha = (CausaSospecha)99, SustanciasSospechosas = null, TratamientosAplicados = null, Estado = EstadoCasoMedico.Archivado};
            
            //Act
            var res = _validador.Validate(casoMedico);
            
            //Assert
            res.IsFailure.Should().BeTrue();
            res.Error.Should().BeOfType<CasoMedicoError.Validation>();

            var message = res.Error as CasoMedicoError.Validation;
            message!.Errores.Should().Contain("ERROR - Tipo de causa de sospecha invalido.");
        }
        
        [Test]
        public void Validate_CausaSospechaVenenoSinSustanciasSospechosas_RetornaFailure() {
            //Arrange
            var casoMedico = new CasoMedico { TituloCaso = "Caso", Sintomas = "Sintomas", FechaInicio = DateTime.Now, Gravedad = Gravedad.Critica, CausaSospecha = CausaSospecha.Veneno, SustanciasSospechosas = null, TratamientosAplicados = null, Estado = EstadoCasoMedico.Archivado};
            
            //Act
            var res = _validador.Validate(casoMedico);
            
            //Assert
            res.IsFailure.Should().BeTrue();
            res.Error.Should().BeOfType<CasoMedicoError.Validation>();

            var message = res.Error as CasoMedicoError.Validation;
            message!.Errores.Should().Contain("ERROR - No hay venenos asociados al caso medico invalido.");            
            
        }
        
        [Test]
        public void Validate_TipoEstadoInvalida_RetornaFailure() {
            //Arrange
            var casoMedico = new CasoMedico { TituloCaso = "titulo", Sintomas = "sintomas", FechaInicio = DateTime.Now, Gravedad = Gravedad.Critica, CausaSospecha = CausaSospecha.Desconocida, SustanciasSospechosas = null, TratamientosAplicados = null, Estado = (EstadoCasoMedico)99};
            
            //Act
            var res = _validador.Validate(casoMedico);
            
            //Assert
            res.IsFailure.Should().BeTrue();
            res.Error.Should().BeOfType<CasoMedicoError.Validation>();

            var message = res.Error as CasoMedicoError.Validation;
            message!.Errores.Should().Contain("ERROR - Tipo de estado invalido.");
        }
    }
}