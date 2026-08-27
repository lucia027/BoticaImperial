using System.Data;
using BoticaImperial.Dto;
using BoticaImperial.Enum;
using BoticaImperial.Mappers;
using BoticaImperial.Models;
using FluentAssertions;

namespace BoticaImperialTest.Mappers;

[TestFixture]
public class CasoMedicoMapperTest {

    [TestFixture]
    public sealed class CasosPositivos {

        [SetUp]
        public void SetUp() {
            _casoMedico = new CasoMedico{
                Id = 2,
                TituloCaso = "Paciente con fiebre misteriosa", 
                Sintomas = "Fiebre alta y escalofríos prolongados", 
                FechaInicio = DateTime.Today.AddDays(1),
                Gravedad = Gravedad.Leve,
                CausaSospecha = CausaSospecha.Desconocida,
                SustanciasSospechosas = null, 
                Estado = EstadoCasoMedico.Abierto,
                TratamientosAplicados = new HashSet<Medicina>(), 
            };
            
            _casoMedicoDto = new CasoMedicoDto{
                Id = 2,
                TituloCaso = "Paciente con fiebre misteriosa",
                Sintomas = "Fiebre alta y escalofríos prolongados",
                FechaInicio = DateTime.Today.AddDays(1).ToString("s"),
                Gravedad = "Leve",
                CausaSospecha = "Desconocida",
                SustanciasSospechosas = null,
                TratamientosAplicados = new HashSet<SustanciaDto>(),
                Estado = "Abierto",
            };
        }

        private CasoMedico _casoMedico = null!;
        private CasoMedicoDto _casoMedicoDto = null!;

        [Test]
        public void ToModel_CasoMedicoDtoValido_RetornaCasoMedico() {
            //Act
            var res = _casoMedicoDto.ToModel();
            
            //Assert
            res.FechaInicio.Should().Be(DateTime.Today.AddDays(1));
            res.Gravedad.Should().Be(Gravedad.Leve);
            res.CausaSospecha.Should().Be(CausaSospecha.Desconocida);
            res.SustanciasSospechosas.Should().BeNull();
            res.TratamientosAplicados.Should().BeNull();
            res.Estado.Should().Be(EstadoCasoMedico.Abierto);
        }

        [Test]
        public void ToDto_CasoMedicoValido_RetornaCasoMedicoDto() {
            //Act
            var res = _casoMedico.ToDto();
            
            //Assert
            res.FechaInicio.Should().Be(DateTime.Today.AddDays(1).ToString("s"));
            res.Gravedad.Should().Be("Leve");
            res.CausaSospecha.Should().Be("Desconocida");
            res.SustanciasSospechosas.Should().BeNull();
            res.TratamientosAplicados.Should().BeNull();
            res.Estado.Should().Be("Abierto");
        }
    }
}