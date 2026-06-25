using BoticaImperial.Enum;

namespace BoticaImperial.Models;

public record CasoMedico {
    public int Id { get; init; }
    public string TituloCaso { get; set; } = string.Empty;
    public string Sintomas { get; set; } = string.Empty;
    public DateTime FechaInicio { get; set; }
    public Gravedad Gravedad { get; set; }
    public CausaSospecha CausaSospecha { get; set; }
    public HashSet<Veneno>? SustanciasSospechosas { get; set; }
    public HashSet<Medicina>? TratamientosAplicados { get; set; }
    public EstadoCasoMedico Estado { get; set; }
}