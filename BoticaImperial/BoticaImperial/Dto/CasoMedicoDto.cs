using System.Xml.Serialization;

namespace BoticaImperial.Dto;

[XmlRoot("BoticaImperial")]
[XmlType("Caso Medico")]
public record CasoMedicoDto(
    [property: XmlAttribute("")] int Id,
    [property: XmlAttribute("tituloCaso")] string TituloCaso,
    [property: XmlAttribute("sintomas")] string Sintomas,
    [property: XmlAttribute("fechaInicio")] string FechaInicio,
    [property: XmlAttribute("gravedad")] string Gravedad,
    [property: XmlAttribute("causaSospecha")] string CausaSospecha,
    [property: XmlAttribute("sustanciasSospechosas")] HashSet<SustanciaDto>? SustanciasSospechosas,
    [property: XmlAttribute("tratamientosAplicados")] HashSet<SustanciaDto>? TratamientosAplicados,
    [property: XmlAttribute("estado")] string Estado,
    [property: XmlAttribute("createAt")] string CreateAt,
    [property: XmlAttribute("updateAt")] string UpdateAt,
    [property: XmlAttribute("deleteAt")] string DeleteAt,
    [property: XmlAttribute("isDelete")] bool IsDelete
    
) {
    public CasoMedicoDto() : this(0, "", "", "", "", "", null, null, "", "", "", "", false) { }
}