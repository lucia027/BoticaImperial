using System.Globalization;
using BoticaImperial.Dto;
using BoticaImperial.Enum;
using BoticaImperial.Models;

namespace BoticaImperial.Mappers;

public static class SustanciaMapper {
    
    private const string DateTimeFormat = "s";
    private static readonly CultureInfo InvariantCulture = CultureInfo.InvariantCulture;

    public static Sustancia ToModel(this SustanciaDto dto) {
        return dto.Tipo switch {
            "Medicina" => new Medicina {
                Id = dto.Id,
                Nombre = dto.Nombre,
                Descripcion = dto.Descripcion,
                Precio = decimal.TryParse(dto.Precio, InvariantCulture, out var precio) ? precio : 0,
                Disponibilidad = System.Enum.TryParse(dto.Disponibilidad, out Disponibilidad disponibilidad) ? disponibilidad : Disponibilidad.Comun,
                NivelPeligro = System.Enum.TryParse(dto.NivelPeligro, out NivelDePeligro nivel) ? nivel : NivelDePeligro.Nulo,
                Sintomas = dto.Sintomas ?? string.Empty,
                DosisRecomendada = double.TryParse(dto.DosisRecomendada, InvariantCulture, out var dosis ) ? dosis : 0.0,
                EfectosSecundarios = dto.EfectosSecundarios ?? string.Empty,
                TiempoDeEfecto = dto.TiempoDeEfecto ?? 0,
                CreateAt = DateTime.TryParse(dto.CreateAt, InvariantCulture, out var create) ? create : DateTime.Now,
                UpdateAt = DateTime.TryParse(dto.UpdateAt, InvariantCulture, out var update) ? update : DateTime.Now,
                DeleteAt = DateTime.TryParse(dto.DeleteAt, InvariantCulture, out var delete) ? delete : DateTime.Now,
                IsDelete = dto.IsDelete
            },
            "Afrodisiaco" => new Afrodisiaco {
                Id = dto.Id,
                Nombre = dto.Nombre,
                Descripcion = dto.Descripcion,
                Precio = decimal.TryParse(dto.Precio, InvariantCulture, out var precio) ? precio : 0,
                Disponibilidad = System.Enum.TryParse(dto.Disponibilidad, out Disponibilidad disponibilidad) ? disponibilidad : Disponibilidad.Comun,
                NivelPeligro = System.Enum.TryParse(dto.NivelPeligro, out NivelDePeligro nivel) ? nivel : NivelDePeligro.Nulo,
                IntensidadDelEfecto = dto.IntensidadDelEfecto ?? 0,
                Duracion = dto.Duracion ?? 0,
                ContraIndicaciones = dto.ContraIndicaciones ?? string.Empty,
                RiegoUsoExcesivo = dto.RiegoUsoExcesivo ?? string.Empty,
                CreateAt = DateTime.TryParse(dto.CreateAt, InvariantCulture, out var create) ? create : DateTime.Now,
                UpdateAt = DateTime.TryParse(dto.UpdateAt, InvariantCulture, out var update) ? update : DateTime.Now,
                DeleteAt = DateTime.TryParse(dto.DeleteAt, InvariantCulture, out var delete) ? delete : DateTime.Now,
                IsDelete = dto.IsDelete
            },
            "Veneno" => new Veneno {
                Id = dto.Id,
                Nombre = dto.Nombre,
                Descripcion = dto.Descripcion,
                Precio = decimal.TryParse(dto.Precio, InvariantCulture, out var precio) ? precio : 0,
                Disponibilidad = System.Enum.TryParse(dto.Disponibilidad, out Disponibilidad disponibilidad) ? disponibilidad : Disponibilidad.Comun,
                NivelPeligro = System.Enum.TryParse(dto.NivelPeligro, out NivelDePeligro nivel) ? nivel : NivelDePeligro.Nulo, 
                ViaDeAdministracion = System.Enum.TryParse(dto.ViaDeAdministracion, out ViaDeAdministracion viaDeAdministracion) ? viaDeAdministracion : ViaDeAdministracion.Desconocida,
                TiempoAparicionSintomas = dto.TiempoAparicionSintomas ?? 0,
                Antidoto = dto.Antidoto?.ToModel() as Medicina,
                GradoDeToxicidad = dto.GradoDeToxicidad ?? 0,
                CreateAt = DateTime.TryParse(dto.CreateAt, InvariantCulture, out var create) ? create : DateTime.Now,
                UpdateAt = DateTime.TryParse(dto.UpdateAt, InvariantCulture, out var update) ? update : DateTime.Now,
                DeleteAt = DateTime.TryParse(dto.DeleteAt, InvariantCulture, out var delete) ? delete : DateTime.Now,
                IsDelete = dto.IsDelete
            },
            _ => throw new ArgumentException("El tipo de sustancia es desconocido.")
        };
    }

    public static SustanciaDto ToDto(this Sustancia sustancia) {
        return sustancia switch {
            Medicina medicina => new SustanciaDto(
                medicina.Id,
                medicina.Nombre,
                medicina.Descripcion,
                medicina.Precio.ToString(InvariantCulture),
                medicina.Disponibilidad.ToString(),
                medicina.NivelPeligro.ToString(),
                "Medicina",
                medicina.Sintomas,
                medicina.DosisRecomendada.ToString(InvariantCulture),
                medicina.EfectosSecundarios,
                medicina.TiempoDeEfecto,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                medicina.CreateAt.ToString(DateTimeFormat),
                medicina.UpdateAt.ToString(DateTimeFormat),
                medicina.DeleteAt.ToString(DateTimeFormat),
                medicina.IsDelete
            ),
            Afrodisiaco afrodisiaco => new SustanciaDto(
                afrodisiaco.Id,
                afrodisiaco.Nombre,
                afrodisiaco.Descripcion,
                afrodisiaco.Precio.ToString(InvariantCulture),
                afrodisiaco.Disponibilidad.ToString(),
                afrodisiaco.NivelPeligro.ToString(),
                "Afrodisiaco",
                null,
                null,
                null,
                null,
                afrodisiaco.IntensidadDelEfecto,
                afrodisiaco.Duracion,
                afrodisiaco.ContraIndicaciones,
                afrodisiaco.RiegoUsoExcesivo,
                null,
                null,
                null,
                null,
                null,
                afrodisiaco.CreateAt.ToString(DateTimeFormat),
                afrodisiaco.UpdateAt.ToString(DateTimeFormat),
                afrodisiaco.DeleteAt.ToString(DateTimeFormat),
                afrodisiaco.IsDelete
            ),
            Veneno veneno => new SustanciaDto(
                veneno.Id,
                veneno.Nombre,
                veneno.Descripcion,
                veneno.Precio.ToString(InvariantCulture),
                veneno.Disponibilidad.ToString(),
                veneno.NivelPeligro.ToString(),
                "Veneno",
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                veneno.ViaDeAdministracion.ToString(),
                veneno.TiempoAparicionSintomas,
                veneno.Antidoto?.ToDto(),
                veneno.GradoDeToxicidad,
                veneno.ProbabilidadSupervivencia,
                veneno.CreateAt.ToString(DateTimeFormat),
                veneno.UpdateAt.ToString(DateTimeFormat),
                veneno.DeleteAt.ToString(DateTimeFormat),
                veneno.IsDelete
            ),
            _ => throw new ArgumentException("El tipo de sustancia es desconocido.")
        };
    }

    public static IEnumerable<Sustancia> ToModel(this IEnumerable<SustanciaDto> dtos) {
        return dtos.Select(s => s.ToModel());
    }

    public static IEnumerable<SustanciaDto> ToDto(this IEnumerable<Sustancia> entidades) {
        return entidades.Select(d => d.ToDto());
    }
}