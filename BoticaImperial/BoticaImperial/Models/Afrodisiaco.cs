namespace BoticaImperial.Models;

public sealed record Afrodisiaco : Sustancia{
    public int IntensidadDelEfecto { get; set; }
    public int Duracion { get; set; }
    public string ContraIndicaciones { get; set; } = string.Empty;
    public string RiegoUsoExcesivo { get; set; } = string.Empty;
}