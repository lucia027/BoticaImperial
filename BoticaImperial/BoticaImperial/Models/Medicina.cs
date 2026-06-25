namespace BoticaImperial.Models;

public sealed record Medicina : Sustancia {
    public string Sintomas { get; set; } = string.Empty;
    public double DosisRecomendada { get; set; }
    public string EfectosSecundarios { get; set; } = string.Empty;
    public int TiempoDeEfecto { get; set; }
}