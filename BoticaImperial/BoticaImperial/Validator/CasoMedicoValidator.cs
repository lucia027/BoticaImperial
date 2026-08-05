using BoticaImperial.Enum;
using BoticaImperial.Errors;
using BoticaImperial.Errors.Common;
using BoticaImperial.Models;
using BoticaImperial.Validator.Common;
using CSharpFunctionalExtensions;
using Serilog;

namespace BoticaImperial.Validator;

public class CasoMedicoValidator : IValidator<CasoMedico> {

    private readonly ILogger _logger = Log.ForContext<CasoMedicoValidator>();
    
    public Result<CasoMedico, DomainError> Validate(CasoMedico entity) {
        
        _logger.Debug($"Intentando validar la entidad [Id={entity.Id}, Titulo={entity.TituloCaso}]");
        var errores = new List<string>();

        if(!entity.TituloCaso.IsValidTitulo()) errores.Add("ERROR - Titulo del caso en blanco o nulo invalido.");
        if(!entity.Sintomas.IsValidSintomasCasoMedico()) errores.Add("ERROR - Sintomas en blanco o nulo invalido.");
        if(!entity.FechaInicio.IsValidFechaInicio()) errores.Add("ERROR - Fecha de inicio anterior a la de hoy invalida.");
        if(!entity.Gravedad.IsValidGravedad()) errores.Add("ERROR - Tipo de gravedad invalido.");
        if(!entity.CausaSospecha.IsValidCausaSospecha()) errores.Add("ERROR - Tipo de causa de sospecha invalido.");
        if(entity.CausaSospecha == CausaSospecha.Veneno && !entity.SustanciasSospechosas.IsValidSustanciasSospechosas()) errores.Add("ERROR - No hay venenos asociados al caso medico invalido.");
        if(!entity.Estado.IsValidEstado()) errores.Add("ERROR - Tipo de estado invalido.");


        if (errores.Any()) {
            _logger.Debug("Validacion no superada de la entidad.");
            return Result.Failure<CasoMedico, DomainError>(CasoMedicoErrors.Validation(errores));
        }
        _logger.Debug("Validacion superada de la entidad.");
        return Result.Success<CasoMedico, DomainError>(entity);
    }
}

public static class ValidatorCasosMedicos {
    public static bool IsValidTitulo(this string titulo) {
        return !string.IsNullOrEmpty(titulo);
    }

    public static bool IsValidSintomasCasoMedico(this string sintomas) {
        return !string.IsNullOrEmpty(sintomas);
    }

    public static bool IsValidFechaInicio(this DateTime fecha) {
        return fecha > DateTime.Today;
    }

    public static bool IsValidGravedad(this Gravedad gravedad) {
        return System.Enum.IsDefined(typeof(Gravedad), gravedad);
    }
    
    public static bool IsValidCausaSospecha(this CausaSospecha causaSospecha) {
        return System.Enum.IsDefined(typeof(CausaSospecha), causaSospecha);
    }

    public static bool IsValidSustanciasSospechosas(this IEnumerable<Veneno>? sustanciasSospechosas) {
        return sustanciasSospechosas != null && sustanciasSospechosas.Any();
    }

    public static bool IsValidEstado(this EstadoCasoMedico estado) {
        return System.Enum.IsDefined(typeof(EstadoCasoMedico), estado);
    }
}