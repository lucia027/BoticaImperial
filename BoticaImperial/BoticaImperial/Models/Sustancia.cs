using BoticaImperial.Enum;

namespace BoticaImperial.Models;

public abstract record Sustancia {
    public int Id { get; init; }
    public string Nombre { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public decimal Precio { get; set; }
    public Disponibilidad Disponibilidad { get; set; }
    public NivelDePeligro NivelPeligro { get; set; }
    public DateTime CreateAt { get; set; }
    public DateTime UpdateAt { get; set; }
    public DateTime DeleteAt { get; set; }
    public bool IsDelete { get; set; }
}