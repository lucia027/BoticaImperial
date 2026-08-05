using BoticaImperial.Enum;
using BoticaImperial.Errors;
using BoticaImperial.Models;
using BoticaImperial.Validator;
using FluentAssertions;

namespace BoticaImperialTest.Validator;

[TestFixture]
public class SustanciaValidatorTest {
    
    
    [TestFixture]
    public sealed class CasosPositivos {
        
        [SetUp]
        public void SetUp() {
            _validator = new SustanciaValidator();
        }
    
        private SustanciaValidator _validator = null!;

        [Test]
        public void Validate_MedicinaValida_RetornaSuccess() {
            //Arrange
            var sustancia = new Medicina{Id = 0, Nombre = "Sustancia", Descripcion = "Descripcion", Precio = 5, Disponibilidad = Disponibilidad.Rara, NivelPeligro = NivelDePeligro.Bajo, Sintomas = "Sintomas", DosisRecomendada = 2, EfectosSecundarios = "Efectos secundarios", TiempoDeEfecto = 300};
            
            //Act
            var res = _validator.Validate(sustancia);
            
            //Assert
            res.IsSuccess.Should().BeTrue();
        }

        [Test]
        public void Validate_AfrodisiacoValido_RetornaSuccess() {
            //Arrange
            var sustancia = new Afrodisiaco{Id = 0, Nombre = "Sustancia", Descripcion = "Descripcion", Precio = 5, Disponibilidad = Disponibilidad.Rara, NivelPeligro = NivelDePeligro.Bajo, IntensidadDelEfecto = 5, Duracion = 2, ContraIndicaciones = "contra indicaciones", RiegoUsoExcesivo = "riesgo de uso"};

            //Act
            var res = _validator.Validate(sustancia);
            
            //Assert
            res.IsSuccess.Should().BeTrue();
        }

        [Test]
        public void Validate_VenenoValido_RetornaSuccess() {
            //Arrange
            var sustancia = new Veneno{Id = 0, Nombre = "Sustancia", Descripcion = "Descripcion", Precio = 5, Disponibilidad = Disponibilidad.Rara, NivelPeligro = NivelDePeligro.Bajo, ViaDeAdministracion = ViaDeAdministracion.Contacto, TiempoAparicionSintomas = 3, Antidoto = null, GradoDeToxicidad = 5};
            
            //Act
            var res = _validator.Validate(sustancia);
            
            //Assert
            res.IsSuccess.Should().BeTrue();
        }

        [TestCase(Disponibilidad.Comun)]
        [TestCase(Disponibilidad.Rara)]
        [TestCase(Disponibilidad.MuyRara)]
        public void Validate_TiposDisponibilidad_RetornaSuccess(Disponibilidad disponibilidad) {
            //Arrange
            var sustancia = new Medicina{Id = 0, Nombre = "Sustancia", Descripcion = "Descripcion", Precio = 5, Disponibilidad = disponibilidad, NivelPeligro = NivelDePeligro.Bajo, Sintomas = "Sintomas", DosisRecomendada = 2, EfectosSecundarios = "Efectos secundarios", TiempoDeEfecto = 300};
            
            //Act
            var res = _validator.Validate(sustancia);
            
            //Assert
            res.IsSuccess.Should().BeTrue();
        }

        [TestCase(NivelDePeligro.Nulo)]
        [TestCase(NivelDePeligro.Bajo)]
        [TestCase(NivelDePeligro.Medio)]
        [TestCase(NivelDePeligro.Alto)]
        [TestCase(NivelDePeligro.Critico)]
        public void Validate_TiposNivelPeligro_RetornaSuccess(NivelDePeligro nivelDePeligro) {
            //Arrange
            var sustancia = new Medicina{Id = 0, Nombre = "Sustancia", Descripcion = "Descripcion", Precio = 5, Disponibilidad = Disponibilidad.Rara, NivelPeligro = nivelDePeligro, Sintomas = "Sintomas", DosisRecomendada = 2, EfectosSecundarios = "Efectos secundarios", TiempoDeEfecto = 300};
            
            //Act
            var res = _validator.Validate(sustancia);
            
            //Assert
            res.IsSuccess.Should().BeTrue();
        }

        [TestCase(ViaDeAdministracion.Oral)]
        [TestCase(ViaDeAdministracion.Contacto)]
        [TestCase(ViaDeAdministracion.Inhalacion)]
        [TestCase(ViaDeAdministracion.Desconocida)]
        public void Validate_TiposViaDeAdministracion(ViaDeAdministracion viaDeAdministracion) {
            //Arrange
            var sustancia = new Veneno{Id = 0, Nombre = "Sustancia", Descripcion = "Descripcion", Precio = 5, Disponibilidad = Disponibilidad.Rara, NivelPeligro = NivelDePeligro.Bajo, ViaDeAdministracion = viaDeAdministracion, TiempoAparicionSintomas = 3, Antidoto = null, GradoDeToxicidad = 5};
            
            //Act
            var res = _validator.Validate(sustancia);
            
            //Assert
            res.IsSuccess.Should().BeTrue();
        }
    }

    [TestFixture]
    public sealed class CasosNegativos {

        [SetUp]
        public void SetUp() {
            _validator = new SustanciaValidator();
        }

        private SustanciaValidator _validator = null!;

        [Test]
        public void Validate_NombreVacio_RetornaFailure() {
            //Arrange
            var sustancia = new Medicina {
                Id = 0, Nombre = "", Descripcion = "Descripcion", Precio = 5, Disponibilidad = Disponibilidad.Rara,
                NivelPeligro = NivelDePeligro.Bajo, Sintomas = "Sintomas", DosisRecomendada = 2,
                EfectosSecundarios = "Efectos secundarios", TiempoDeEfecto = 300
            };

            //Act
            var res = _validator.Validate(sustancia);

            //Assert
            res.IsFailure.Should().BeTrue();
            res.Error.Should().BeOfType<SustanciaError.Validation>();

            var message = res.Error as SustanciaError.Validation;
            message!.Errores.Should().Contain("ERROR - Nombre en blanco o nulo invalido.");
        }

        [Test]
        public void Validate_DescripcionVacia_RetornaFailure() {
            //Arrange
            var sustancia = new Medicina {
                Id = 0, Nombre = "Nombre", Descripcion = "", Precio = 5, Disponibilidad = Disponibilidad.Rara,
                NivelPeligro = NivelDePeligro.Bajo, Sintomas = "Sintomas", DosisRecomendada = 2,
                EfectosSecundarios = "Efectos secundarios", TiempoDeEfecto = 300
            };

            //Act
            var res = _validator.Validate(sustancia);

            //Assert
            res.IsFailure.Should().BeTrue();
            res.Error.Should().BeOfType<SustanciaError.Validation>();

            var message = res.Error as SustanciaError.Validation;
            message!.Errores.Should().Contain("ERROR - Descripcion en blanco o nulo invalido.");
        }

        [Test]
        public void Validate_PrecioNegativo_RetornaFailure() {
            //Arrange
            var sustancia = new Medicina {
                Id = 0, Nombre = "Nombre", Descripcion = "Descripcion", Precio = -5,
                Disponibilidad = Disponibilidad.Rara, NivelPeligro = NivelDePeligro.Bajo, Sintomas = "Sintomas",
                DosisRecomendada = 2, EfectosSecundarios = "Efectos secundarios", TiempoDeEfecto = 300
            };

            //Act
            var res = _validator.Validate(sustancia);

            //Assert
            res.IsFailure.Should().BeTrue();
            res.Error.Should().BeOfType<SustanciaError.Validation>();

            var message = res.Error as SustanciaError.Validation;
            message!.Errores.Should().Contain("ERROR - Precio negativo invalido.");
        }

        [Test]
        public void Validate_TipoDisponibilidadNoValida_RetornaFailure() {
            //Arrange
            var sustancia = new Medicina {
                Id = 0, Nombre = "Nombre", Descripcion = "Descripcion", Precio = 5, Disponibilidad = (Disponibilidad)99,
                NivelPeligro = NivelDePeligro.Bajo, Sintomas = "Sintomas", DosisRecomendada = 2,
                EfectosSecundarios = "Efectos secundarios", TiempoDeEfecto = 300
            };

            //Act
            var res = _validator.Validate(sustancia);

            //Assert
            res.IsFailure.Should().BeTrue();
            res.Error.Should().BeOfType<SustanciaError.Validation>();

            var message = res.Error as SustanciaError.Validation;
            message!.Errores.Should().Contain("ERROR - Tipo de disponibilidad invalido.");
        }

        [Test]
        public void Validate_TipoNivelPeligroNoValida_RetornaFailure() {
            //Arrange
            var sustancia = new Medicina {
                Id = 0, Nombre = "Nombre", Descripcion = "Descripcion", Precio = 5,
                Disponibilidad = Disponibilidad.Comun, NivelPeligro = (NivelDePeligro)66, Sintomas = "Sintomas",
                DosisRecomendada = 2, EfectosSecundarios = "Efectos secundarios", TiempoDeEfecto = 300
            };

            //Act
            var res = _validator.Validate(sustancia);

            //Assert
            res.IsFailure.Should().BeTrue();
            res.Error.Should().BeOfType<SustanciaError.Validation>();

            var message = res.Error as SustanciaError.Validation;
            message!.Errores.Should().Contain("ERROR - Tipo de nivel de peligro invalido.");
        }

        [Test]
        public void Validate_SintomasVacio_RetornaFailure() {
            //Arrange
            var sustancia = new Medicina {
                Id = 0, Nombre = "Nombre", Descripcion = "Descripcion", Precio = 5,
                Disponibilidad = Disponibilidad.Comun, NivelPeligro = NivelDePeligro.Alto, Sintomas = "",
                DosisRecomendada = 2, EfectosSecundarios = "Efectos secundarios", TiempoDeEfecto = 300
            };

            //Act
            var res = _validator.Validate(sustancia);

            //Assert
            res.IsFailure.Should().BeTrue();
            res.Error.Should().BeOfType<SustanciaError.Validation>();

            var message = res.Error as SustanciaError.Validation;
            message!.Errores.Should().Contain("ERROR - Sintomas en blanco o nulo invalidos.");
        }

        [Test]
        public void Validate_DosisNegativa_RetornaFailure() {
            //Arrange
            var sustancia = new Medicina {
                Id = 0, Nombre = "Nombre", Descripcion = "Descripcion", Precio = 5,
                Disponibilidad = Disponibilidad.Comun, NivelPeligro = NivelDePeligro.Alto, Sintomas = "Sintomas",
                DosisRecomendada = -2, EfectosSecundarios = "Efectos secundarios", TiempoDeEfecto = 300
            };

            //Act
            var res = _validator.Validate(sustancia);

            //Assert
            res.IsFailure.Should().BeTrue();
            res.Error.Should().BeOfType<SustanciaError.Validation>();

            var message = res.Error as SustanciaError.Validation;
            message!.Errores.Should().Contain("ERROR - Dosis negativa invalida.");
        }

        [Test]
        public void Validate_EfectosSecundariosVacio_RetornaFailure() {
            //Arrange
            var sustancia = new Medicina {
                Id = 0, Nombre = "Nombre", Descripcion = "Descripcion", Precio = 5,
                Disponibilidad = Disponibilidad.Comun, NivelPeligro = NivelDePeligro.Alto, Sintomas = "Sintomas",
                DosisRecomendada = 2, EfectosSecundarios = "", TiempoDeEfecto = 300
            };

            //Act
            var res = _validator.Validate(sustancia);

            //Assert
            res.IsFailure.Should().BeTrue();
            res.Error.Should().BeOfType<SustanciaError.Validation>();

            var message = res.Error as SustanciaError.Validation;
            message!.Errores.Should().Contain("ERROR - EfectosSecundarios en blanco o nulo invalidos.");
        }

        [Test]
        public void Validate_TiempoEfectoNegativo_RetornaFailure() {
            //Arrange
            var sustancia = new Medicina {
                Id = 0, Nombre = "Nombre", Descripcion = "Descripcion", Precio = 5,
                Disponibilidad = Disponibilidad.Comun, NivelPeligro = NivelDePeligro.Alto, Sintomas = "Sintomas",
                DosisRecomendada = 2, EfectosSecundarios = "efectos secundarios", TiempoDeEfecto = -300
            };

            //Act
            var res = _validator.Validate(sustancia);

            //Assert
            res.IsFailure.Should().BeTrue();
            res.Error.Should().BeOfType<SustanciaError.Validation>();

            var message = res.Error as SustanciaError.Validation;
            message!.Errores.Should().Contain("ERROR - Tiempo de efecto negativo invalido.");
        }

        [Test]
        public void Validate_TipoViaAdministracionInvalido_RetornaFailure() {
            //Arrange
            var sustancia = new Veneno {
                Id = 0, Nombre = "Sustancia", Descripcion = "Descripcion", Precio = 5,
                Disponibilidad = Disponibilidad.Rara, NivelPeligro = NivelDePeligro.Bajo,
                ViaDeAdministracion = (ViaDeAdministracion)99, TiempoAparicionSintomas = 3, Antidoto = null,
                GradoDeToxicidad = 5
            };

            //Act
            var res = _validator.Validate(sustancia);

            //Assert
            res.IsFailure.Should().BeTrue();
            res.Error.Should().BeOfType<SustanciaError.Validation>();

            var message = res.Error as SustanciaError.Validation;
            message!.Errores.Should().Contain("ERROR - Tipo de via de administracion invalido.");
        }

        [Test]
        public void Validate_TiempoAparicionSintomasNegativo_RetornaFailure() {
            //Arrange
            var sustancia = new Veneno {
                Id = 0, Nombre = "Sustancia", Descripcion = "Descripcion", Precio = 5,
                Disponibilidad = Disponibilidad.Rara, NivelPeligro = NivelDePeligro.Bajo,
                ViaDeAdministracion = ViaDeAdministracion.Contacto, TiempoAparicionSintomas = -3, Antidoto = null,
                GradoDeToxicidad = 5
            };

            //Act
            var res = _validator.Validate(sustancia);

            //Assert
            res.IsFailure.Should().BeTrue();
            res.Error.Should().BeOfType<SustanciaError.Validation>();

            var message = res.Error as SustanciaError.Validation;
            message!.Errores.Should().Contain("ERROR - Tiempo de aparicion de sintomas negativo invalido.");
        }

        [Test]
        public void Validate_GradoToxicidadNegativo_RetornaFailure() {
            //Arrange
            var sustancia = new Veneno {
                Id = 0, Nombre = "Sustancia", Descripcion = "Descripcion", Precio = 5,
                Disponibilidad = Disponibilidad.Rara, NivelPeligro = NivelDePeligro.Bajo,
                ViaDeAdministracion = ViaDeAdministracion.Contacto, TiempoAparicionSintomas = 3, Antidoto = null,
                GradoDeToxicidad = -5
            };

            //Act
            var res = _validator.Validate(sustancia);

            //Assert
            res.IsFailure.Should().BeTrue();
            res.Error.Should().BeOfType<SustanciaError.Validation>();

            var message = res.Error as SustanciaError.Validation;
            message!.Errores.Should().Contain("ERROR - Grado de toxicidad negativo invalido.");
        }

        [Test]
        public void Validate_IntensidadEfectoNegativo_RetornaFailure() {
            //Arrange
            var sustancia = new Afrodisiaco{Id = 0, Nombre = "Sustancia", Descripcion = "Descripcion", Precio = 5, Disponibilidad = Disponibilidad.Rara, NivelPeligro = NivelDePeligro.Bajo, IntensidadDelEfecto = -5, Duracion = 2, ContraIndicaciones = "contra indicaciones", RiegoUsoExcesivo = "riesgo de uso"};

            //Act
            var res = _validator.Validate(sustancia);

            //Assert
            res.IsFailure.Should().BeTrue();
            res.Error.Should().BeOfType<SustanciaError.Validation>();

            var message = res.Error as SustanciaError.Validation;
            message!.Errores.Should().Contain("ERROR - Intensidad del efecto negativa invalida.");
        }
        
        [Test]
        public void Validate_DuracionNegativo_RetornaFailure() {
            //Arrange
            var sustancia = new Afrodisiaco{Id = 0, Nombre = "Sustancia", Descripcion = "Descripcion", Precio = 5, Disponibilidad = Disponibilidad.Rara, NivelPeligro = NivelDePeligro.Bajo, IntensidadDelEfecto = 5, Duracion = -2, ContraIndicaciones = "contra indicaciones", RiegoUsoExcesivo = "riesgo de uso"};

            //Act
            var res = _validator.Validate(sustancia);

            //Assert
            res.IsFailure.Should().BeTrue();
            res.Error.Should().BeOfType<SustanciaError.Validation>();

            var message = res.Error as SustanciaError.Validation;
            message!.Errores.Should().Contain("ERROR - Duracion negativa invalida.");
        }
        
        [Test]
        public void Validate_ContraindicacionesBlancoONulo_RetornaFailure() {
            //Arrange
            var sustancia = new Afrodisiaco{Id = 0, Nombre = "Sustancia", Descripcion = "Descripcion", Precio = 5, Disponibilidad = Disponibilidad.Rara, NivelPeligro = NivelDePeligro.Bajo, IntensidadDelEfecto = 5, Duracion = 2, ContraIndicaciones = "", RiegoUsoExcesivo = "riesgo de uso"};

            //Act
            var res = _validator.Validate(sustancia);

            //Assert
            res.IsFailure.Should().BeTrue();
            res.Error.Should().BeOfType<SustanciaError.Validation>();

            var message = res.Error as SustanciaError.Validation;
            message!.Errores.Should().Contain("ERROR - Contra indicaciones en blanco o nulo invalidos.");
        }
        
        [Test]
        public void Validate_RiegoUsoExcesivoBlancoONulo_RetornaFailure() {
            //Arrange
            var sustancia = new Afrodisiaco{Id = 0, Nombre = "Sustancia", Descripcion = "Descripcion", Precio = 5, Disponibilidad = Disponibilidad.Rara, NivelPeligro = NivelDePeligro.Bajo, IntensidadDelEfecto = 5, Duracion = 2, ContraIndicaciones = "contra indicaciones", RiegoUsoExcesivo = ""};

            //Act
            var res = _validator.Validate(sustancia);

            //Assert
            res.IsFailure.Should().BeTrue();
            res.Error.Should().BeOfType<SustanciaError.Validation>();

            var message = res.Error as SustanciaError.Validation;
            message!.Errores.Should().Contain("ERROR - Riego de uso excesivo en blanco o nulo invalidos.");
        }
    }
}