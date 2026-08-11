using System.Globalization;
using System.Runtime.InteropServices.JavaScript;
using BoticaImperial.Dto;
using BoticaImperial.Enum;
using BoticaImperial.Models;

namespace BoticaImperial.Mappers;

public static class CasoMedicoMapper {
    
    private const string DateTimeFormat = "s";
    private static readonly CultureInfo InvariantCulture = CultureInfo.InvariantCulture;

    public static CasoMedico ToModel(this CasoMedicoDto dto) {
        return new CasoMedico {
            Id = dto.Id,
            TituloCaso = dto.TituloCaso,
            Sintomas = dto.Sintomas,
            FechaInicio = DateTime.TryParse(dto.FechaInicio, InvariantCulture, out var fecha) ? fecha : DateTime.Now,
            Gravedad = System.Enum.TryParse(dto.Gravedad, out Gravedad gravedad) ? gravedad : Gravedad.Nula,
            CausaSospecha = System.Enum.TryParse(dto.CausaSospecha, out CausaSospecha causaSospecha) ? causaSospecha : CausaSospecha.Desconocida,
            SustanciasSospechosas = dto.SustanciasSospechosas?.ToModel() as HashSet<Veneno>,
            TratamientosAplicados = dto.TratamientosAplicados?.ToModel() as HashSet<Medicina>,
            CreateAt = DateTime.TryParse(dto.CreateAt, InvariantCulture, out var create) ? create : DateTime.Now,
            UpdateAt = DateTime.TryParse(dto.UpdateAt, InvariantCulture, out var update) ? update : DateTime.Now,
            DeleteAt = DateTime.TryParse(dto.DeleteAt, InvariantCulture, out var delete) ? delete : DateTime.Now,
            IsDelete = dto.IsDelete
        };
    }

    public static CasoMedicoDto ToDto(this CasoMedico casoMedico) {
        return new CasoMedicoDto(
            casoMedico.Id,
            casoMedico.TituloCaso,
            casoMedico.Sintomas,
            casoMedico.FechaInicio.ToString(DateTimeFormat),
            casoMedico.Gravedad.ToString(),
            casoMedico.CausaSospecha.ToString(),
            casoMedico.SustanciasSospechosas?.ToDto() as HashSet<SustanciaDto>,
            casoMedico.TratamientosAplicados?.ToDto() as HashSet<SustanciaDto>,
            casoMedico.Estado.ToString(),
            casoMedico.CreateAt.ToString(DateTimeFormat),
            casoMedico.UpdateAt.ToString(DateTimeFormat),
            casoMedico.DeleteAt.ToString(DateTimeFormat),
            casoMedico.IsDelete
        );
    }
}