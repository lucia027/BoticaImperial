using BoticaImperial.Models;
using FluentAssertions;

namespace BoticaImperialTest.Models;

[TestFixture]
public class VenenoTest {

    [Test]
    public void ProbabilidadSupervivencia_GradoToxicidadAlto_DevuelveVeinte() {
        //Arrange
        Veneno v = new Veneno { GradoDeToxicidad = 8};
        
        //Act
        var res = v.ProbabilidadSupervivencia;
        
        //Assert
        res.Should().Be(20);
    }
    
    [Test]
    public void ProbabilidadSupervivencia_GradoToxicidadBajo_DevuelveSesenta() {
        //Arrange
        Veneno v = new Veneno { GradoDeToxicidad = 7};
        
        //Act
        var res = v.ProbabilidadSupervivencia;
        
        //Assert
        res.Should().Be(60);
    }
}