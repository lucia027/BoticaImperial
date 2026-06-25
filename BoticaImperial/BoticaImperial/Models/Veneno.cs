using BoticaImperial.Enum;

namespace BoticaImperial.Models;

public sealed record Veneno : Sustancia{
    public ViaDeAdministracion ViaDeAdministracion { get; set; }
    public int TiempoAparicionSintomas { get; set; }
    public Medicina? Antidoto { get; set; }
    public int GradoDeToxicidad { get; set; }
    public int ProbabilidadSupervivencia => (GradoDeToxicidad >= 8) ? 20 : 60;
}