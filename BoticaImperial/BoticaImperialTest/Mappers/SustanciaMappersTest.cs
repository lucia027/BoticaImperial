using BoticaImperial.Dto;
using BoticaImperial.Enum;
using BoticaImperial.Mappers;
using BoticaImperial.Models;
using FluentAssertions;

namespace BoticaImperialTest.Mappers;

[TestFixture]
public class SustanciaMappersTest {

    [TestFixture]
    public sealed class CasosValidos {

        [SetUp]
        public void SetUp() {
            _afrodisiaco = new Afrodisiaco {
                Id = 3,
                Nombre = "Polvo de Rosa Encantada",
                Descripcion = "Aumenta temporalmente el carisma y la atracción.",
                Precio = 150.75m,
                Disponibilidad = Disponibilidad.Comun,
                NivelPeligro = NivelDePeligro.Alto,
                CreateAt = DateTime.Now,
                UpdateAt = DateTime.Now,
                DeleteAt = DateTime.Now,
                IsDelete = false,
                IntensidadDelEfecto = 7,
                Duracion = 120,
                ContraIndicaciones = "No mezclar con pociones de maná",
                RiegoUsoExcesivo = "Puede causar euforia descontrolada y pérdida de memoria a corto plazo"
            };

            _medicina = new Medicina {
                Id = 1,
                Nombre = "Poción Curativa Menor",
                Descripcion = "Restaura la vitalidad básica y cura heridas superficiales.",
                Precio = 50.5m,
                Disponibilidad = Disponibilidad.Comun,
                NivelPeligro = NivelDePeligro.Alto,
                CreateAt = DateTime.UtcNow,
                UpdateAt = DateTime.UtcNow,
                DeleteAt = DateTime.MinValue,
                IsDelete = false,
                Sintomas = "Dolor de cabeza, cortes leves",
                DosisRecomendada = 15.0,
                EfectosSecundarios = "Ligera somnolencia",
                TiempoDeEfecto = 5
            };

            _veneno = new Veneno {
                Id = 2,
                Nombre = "Extracto de Sapo Sombrío",
                Descripcion = "Toxina de acción rápida que paraliza el sistema nervioso.",
                Precio = 300.0m,
                Disponibilidad = Disponibilidad.Comun,
                NivelPeligro = NivelDePeligro.Alto,
                CreateAt = DateTime.UtcNow,
                UpdateAt = DateTime.UtcNow,
                DeleteAt = DateTime.MinValue,
                IsDelete = false,
                ViaDeAdministracion = ViaDeAdministracion.Contacto,
                TiempoAparicionSintomas = 10,
                Antidoto = _medicina, 
                GradoDeToxicidad = 9
            };

            _afrodisiacoDto = new SustanciaDto {
                Id = 3,
                Nombre = "Polvo de Rosa Encantada",
                Descripcion = "Aumenta temporalmente el carisma y la atracción.",
                Precio = "150.75",
                Disponibilidad = "1",
                NivelPeligro = "2",
                Tipo = "Afrodisiaco",
                Sintomas = null,
                DosisRecomendada = null,
                EfectosSecundarios = null,
                TiempoDeEfecto = null,
                IntensidadDelEfecto = 7,
                Duracion = 120,
                ContraIndicaciones = "No mezclar con pociones de maná",
                RiegoUsoExcesivo = "Puede causar euforia descontrolada y pérdida de memoria a corto plazo",
                ViaDeAdministracion = null,
                TiempoAparicionSintomas = null,
                Antidoto = null,
                GradoDeToxicidad = null,
                ProbabilidadSupervivencia = null
            };

            _medicinaDto = new SustanciaDto {
                Id = 1,
                Nombre = "Poción Curativa Menor",
                Descripcion = "Restaura la vitalidad básica y cura heridas superficiales.",
                Precio = "50.50",
                Disponibilidad = "1", 
                NivelPeligro = "1",   
                Tipo = "Medicina",
                Sintomas = "Dolor de cabeza, cortes leves",
                DosisRecomendada = "15.0",
                EfectosSecundarios = "Ligera somnolencia",
                TiempoDeEfecto = 5,
                IntensidadDelEfecto = null,
                Duracion = null,
                ContraIndicaciones = null,
                RiegoUsoExcesivo = null,
                ViaDeAdministracion = null,
                TiempoAparicionSintomas = null,
                Antidoto = null,
                GradoDeToxicidad = null,
                ProbabilidadSupervivencia = null,
                CreateAt = "2026-08-18T10:00:00.000Z",
                UpdateAt = "2026-08-18T10:00:00.000Z",
                DeleteAt = "0001-01-01T00:00:00.000Z",
                IsDelete = false
            };

            _venenoDto = new SustanciaDto {
                Id = 2,
                Nombre = "Extracto de Sapo Sombrío",
                Descripcion = "Toxina de acción rápida que paraliza el sistema nervioso.",
                Precio = "300.00",
                Disponibilidad = "2",
                NivelPeligro = "3", 
                Tipo = "Veneno",
                Sintomas = null,
                DosisRecomendada = null,
                EfectosSecundarios = null,
                TiempoDeEfecto = null,
                IntensidadDelEfecto = null,
                Duracion = null,
                ContraIndicaciones = null,
                RiegoUsoExcesivo = null,
                ViaDeAdministracion = "1",
                TiempoAparicionSintomas = 10,
                Antidoto = _medicinaDto, 
                GradoDeToxicidad = 9,
                ProbabilidadSupervivencia = 20
            };

            _sustancias = new List<Sustancia> {_afrodisiaco, _medicina, _veneno};
            _sustanciasDto = new List<SustanciaDto> { _afrodisiacoDto, _medicinaDto, _venenoDto};
        }

        private Afrodisiaco _afrodisiaco = null!;
        private Medicina _medicina = null!;
        private Veneno _veneno = null!;
        private IEnumerable<Sustancia> _sustancias = null!;

        private SustanciaDto _afrodisiacoDto = null!;
        private SustanciaDto _medicinaDto = null!;
        private SustanciaDto _venenoDto = null!;
        private IEnumerable<SustanciaDto> _sustanciasDto = null!;


        [Test]
        public void ToModel_AfrodisiacoDtoValido_RetornaAfrodisiaco() {
            //Act
            var res = _afrodisiacoDto.ToModel();
            
            //Assert
            res.Should().BeOfType<Afrodisiaco>();
            res.Precio.Should().Be(150.75m);
            res.Disponibilidad.Should().Be(Disponibilidad.Rara);
            res.NivelPeligro.Should().Be(NivelDePeligro.Medio);
            
            var afrodisiaco = res as Afrodisiaco;
            afrodisiaco!.IntensidadDelEfecto.Should().Be(7);
            afrodisiaco.Duracion.Should().Be(120);
            afrodisiaco.ContraIndicaciones.Should().Be("No mezclar con pociones de maná");
            afrodisiaco.RiegoUsoExcesivo.Should().Be("Puede causar euforia descontrolada y pérdida de memoria a corto plazo");
        }

        [Test]
        public void ToModel_MedicinaDtoValida_RetornaMedicina() {
            //Act
            var res = _medicinaDto.ToModel();
            
            //Assert
            res.Should().BeOfType<Medicina>();
            res.Precio.Should().Be(50.50m);
            res.Disponibilidad.Should().Be(Disponibilidad.Rara);
            res.NivelPeligro.Should().Be(NivelDePeligro.Bajo);

            var medicina = res as Medicina;
            medicina!.Sintomas.Should().Be("Dolor de cabeza, cortes leves");
            medicina.DosisRecomendada.Should().Be(15.0);
            medicina.EfectosSecundarios.Should().Be("Ligera somnolencia");
            medicina.TiempoDeEfecto.Should().Be(5);
        }

        [Test]
        public void ToModel_VenenoDtoValido_RetornaVeneno() {
            //Act
            var res = _venenoDto.ToModel();
            
            //Assert
            res.Should().BeOfType<Veneno>();
            res.Precio.Should().Be(300.00m);
            res.Disponibilidad.Should().Be(Disponibilidad.MuyRara);
            res.NivelPeligro.Should().Be(NivelDePeligro.Alto);

            var veneno = res as Veneno;
            veneno!.ViaDeAdministracion.Should().Be(ViaDeAdministracion.Contacto);
            veneno.TiempoAparicionSintomas.Should().Be(10);
            veneno.Antidoto.Should().Be(_medicinaDto.ToModel());
            veneno.GradoDeToxicidad.Should().Be(9);
            veneno.ProbabilidadSupervivencia.Should().Be(20);
        }

        [Test]
        public void ToModel_ColeccionValida_RetornaColeccionConvertida() {
            //Act
            var res = _sustanciasDto.ToModel();
            
            //Assert
            res.Should().AllBeAssignableTo<Sustancia>();
        }

        [Test]
        public void ToDto_AfrodisiacoValido_RetornaSustanciaDtoTipoAfrodisiaco() {
            //Act
            var res = _afrodisiaco.ToDto();
            
            //Assert
            res.Should().BeOfType<SustanciaDto>();
            res.Tipo.Should().Be("Afrodisiaco");
            res.Precio.Should().Be("150.75");
            res.Disponibilidad.Should().Be("Comun");
            res.NivelPeligro.Should().Be("Alto");
        }

        [Test]
        public void ToDto_MedicinaValida_RetornaSustanciaDtoTipoMedicina() {
            //Act
            var res = _medicina.ToDto();
            
            //Assert
            res.Should().BeOfType<SustanciaDto>();
            res.Tipo.Should().Be("Medicina");
            res.Precio.Should().Be("50.5");
            res.Disponibilidad.Should().Be("Comun");
            res.NivelPeligro.Should().Be("Alto");
            res.DosisRecomendada.Should().Be("15");
        }

        [Test]
        public void ToDto_VenenoValido_RetornaSustanciaDtoTipoVeneno() {
            //Act
            var res = _veneno.ToDto();
            
            //Assert
            res.Should().BeOfType<SustanciaDto>();
            res.Tipo.Should().Be("Veneno");
            res.Precio.Should().Be("300.0");
            res.Disponibilidad.Should().Be("Comun");
            res.NivelPeligro.Should().Be("Alto");
            res.ViaDeAdministracion.Should().Be("Contacto");
            res.Antidoto.Should().Be(_medicina.ToDto());
        }
        
        [Test]
        public void ToDto_ColeccionValida_RetornaColeccionConvertida() {
            //Act
            var res = _sustancias.ToDto();
            
            //Assert
            res.Should().AllBeOfType<SustanciaDto>();
        }
    }

    [TestFixture]
    public sealed class CasosInvalidos {
        
        [SetUp]
        public void SetUp() {
            _afrodisiacoDto = new SustanciaDto {
                Id = 3,
                Nombre = "Polvo de Rosa Encantada",
                Descripcion = "Aumenta temporalmente el carisma y la atracción.",
                Precio = "150.75",
                Disponibilidad = "1",
                NivelPeligro = "2",
                Tipo = "",
                Sintomas = null,
                DosisRecomendada = null,
                EfectosSecundarios = null,
                TiempoDeEfecto = null,
                IntensidadDelEfecto = 7,
                Duracion = 120,
                ContraIndicaciones = "No mezclar con pociones de maná",
                RiegoUsoExcesivo = "Puede causar euforia descontrolada y pérdida de memoria a corto plazo",
                ViaDeAdministracion = null,
                TiempoAparicionSintomas = null,
                Antidoto = null,
                GradoDeToxicidad = null,
                ProbabilidadSupervivencia = null
            };
        }

        private readonly Afrodisiaco _afrodisiaco = null!;
        private SustanciaDto _afrodisiacoDto = null!;


        [Test]
        public void ToModel_TipoSustanciaInvalido_RetornaExcepcion() {
            //Act
            Action res = () => _afrodisiacoDto.ToModel();
            
            //Assert
            var mes = res.Should().Throw<ArgumentException>().Which;
            mes.Message.Should().Be("El tipo de sustancia es desconocido.");
        }

        [Test]
        public void ToDto_TipoSustanciaInvalido_RetornaExcepcion() {
            //Act
            Action res = () => _afrodisiaco.ToDto();
            
            //Assert
            var mes = res.Should().Throw<ArgumentException>().Which;
            mes.Message.Should().Be("El tipo de sustancia es desconocido.");
        }
    }
}