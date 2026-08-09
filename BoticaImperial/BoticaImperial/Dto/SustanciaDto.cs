
using System.Xml.Serialization;

namespace BoticaImperial.Dto;

[XmlRoot("BoticaImperial")]
[XmlType("Sustancia")]
public record SustanciaDto(
    [property: XmlAttribute("id")] int Id,
    [property: XmlAttribute("nombre")] string Nombre,
    [property: XmlAttribute("descripcion")] string Descripcion,
    [property: XmlAttribute("precio")] string Precio,
    [property: XmlAttribute("disponibilidad")] string Disponibilidad,
    [property: XmlAttribute("nivelPeligro")] string NivelPeligro,
    [property: XmlAttribute("tipo")] string Tipo,
    
    [property: XmlAttribute("sintomas")] string? Sintomas,
    [property: XmlAttribute("dosisRecomendada")] string? DosisRecomendada,
    [property: XmlAttribute("efectosSecundarios")] string? EfectosSecundarios,
    [property: XmlAttribute("tiempoDeEfecto")] int? TiempoDeEfecto,
    
    [property: XmlAttribute("intensidadDelEfecto")] int? IntensidadDelEfecto,
    [property: XmlAttribute("duracion")] int? Duracion,
    [property: XmlAttribute("contraIndicaciones")] string? ContraIndicaciones,
    [property: XmlAttribute("riegoUsoExcesivo")] string? RiegoUsoExcesivo,
    
    [property: XmlAttribute("viaDeAdministracion")] string? ViaDeAdministracion,
    [property: XmlAttribute("tiempoAparicionSintomas")] int? TiempoAparicionSintomas,
    [property: XmlAttribute("antidoto")] SustanciaDto? Antidoto,
    [property: XmlAttribute("gradoDeToxicidad")] int? GradoDeToxicidad,
    [property: XmlAttribute("probabilidadSupervivencia")] int? ProbabilidadSupervivencia,
    
    [property: XmlAttribute("createAt")] string CreateAt,
    [property: XmlAttribute("updateAt")] string UpdateAt,
    [property: XmlAttribute("deleteAt")] string DeleteAt,
    [property: XmlAttribute("isDelete")] bool IsDelete
) {
    public SustanciaDto() : this(0, "", "", "", "", "", "", "", "", "", 0, 0, 0, "", "", "", 0, null, 0, 0, "", "", "", false) { }
}