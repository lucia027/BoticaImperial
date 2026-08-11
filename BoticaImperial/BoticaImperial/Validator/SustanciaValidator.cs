using BoticaImperial.Enum;
using BoticaImperial.Errors;
using BoticaImperial.Errors.Common;
using BoticaImperial.Models;
using BoticaImperial.Validator.Common;
using CSharpFunctionalExtensions;
using Serilog;


namespace BoticaImperial.Validator;

public class SustanciaValidator : IValidator<Sustancia> {

    private readonly ILogger _logger = Log.ForContext<SustanciaValidator>();
    
    public Result<Sustancia, DomainError> Validate(Sustancia entity) {
        
        _logger.Debug($"Intentando validar la entidad [Id={entity.Id}, Nombre={entity.Nombre}]");
        var errores = new List<string>();
        
        if (!entity.Nombre.IsValidNombre()) errores.Add("ERROR - Nombre en blanco o nulo invalido.");
        if (!entity.Descripcion.IsValidDescripcion()) errores.Add("ERROR - Descripcion en blanco o nulo invalido.");
        if (!entity.Precio.IsValidPrecio()) errores.Add("ERROR - Precio negativo invalido.");
        if (!entity.Disponibilidad.IsValidDisponibilidad()) errores.Add("ERROR - Tipo de disponibilidad invalido.");
        if (!entity.NivelPeligro.IsValidNivelPeligro()) errores.Add("ERROR - Tipo de nivel de peligro invalido.");
 
        if (entity is Medicina medicina1 && !medicina1.Sintomas.IsValidSintomasSustancia()) errores.Add("ERROR - Sintomas en blanco o nulo invalidos.");
        if (entity is Medicina medicina2 && !medicina2.DosisRecomendada.IsValidDosis()) errores.Add("ERROR - Dosis negativa invalida.");
        if (entity is Medicina medicina3 && !medicina3.EfectosSecundarios.IsValidEfectosSecundarios()) errores.Add("ERROR - EfectosSecundarios en blanco o nulo invalidos.");
        if (entity is Medicina medicina4 && !medicina4.TiempoDeEfecto.IsValidTiempoEfecto()) errores.Add("ERROR - Tiempo de efecto negativo invalido.");
        
        if (entity is Veneno veneno1 && !veneno1.ViaDeAdministracion.IsValidViaDeAdministracion()) errores.Add("ERROR - Tipo de via de administracion invalido.");
        if (entity is Veneno veneno2 && !veneno2.TiempoAparicionSintomas.IsValidTiempoAparicionSintomas()) errores.Add("ERROR - Tiempo de aparicion de sintomas negativo invalido.");
        if (entity is Veneno veneno3 && !veneno3.Antidoto.IsValidAntidoto()) errores.Add("ERROR - Antidoto no es medicina o nulo invalido.");
        if (entity is Veneno veneno4 && !veneno4.GradoDeToxicidad.IsValidGradoToxicidad()) errores.Add("ERROR - Grado de toxicidad negativo invalido.");
         
        if (entity is Afrodisiaco afrodisiaco1 && !afrodisiaco1.IntensidadDelEfecto.IsValidIntensidadEfecto()) errores.Add("ERROR - Intensidad del efecto negativa invalida.");
        if (entity is Afrodisiaco afrodisiaco2 && !afrodisiaco2.Duracion.IsValidDuracion()) errores.Add("ERROR - Duracion negativa invalida.");
        if (entity is Afrodisiaco afrodisiaco3 && !afrodisiaco3.ContraIndicaciones.IsValidContraIndicaciones()) errores.Add("ERROR - Contra indicaciones en blanco o nulo invalidos.");
        if (entity is Afrodisiaco afrodisiaco4 && !afrodisiaco4.RiegoUsoExcesivo.IsValidRiegoUsoExcesivo()) errores.Add("ERROR - Riego de uso excesivo en blanco o nulo invalidos.");

        if (errores.Any()) {
            _logger.Debug("Validacion no superada de la entidad.");
            return Result.Failure<Sustancia, DomainError>(SustanciaErrors.Validation(errores));
        }
        _logger.Debug("Validacion superada de la entidad.");
        return Result.Success<Sustancia, DomainError>(entity);
    }
}

public static class ValidadorSustancias {
    public static bool IsValidPrecio(this decimal precio) {
        return precio > 0;
    }

    public static bool IsValidDosis(this double dosis) {
        return dosis > 0;
    }

    public static bool IsValidTiempoEfecto(this int tiempo) {
        return tiempo > 0;
    }

    public static bool IsValidTiempoAparicionSintomas(this int tiempo) {
        return tiempo > 0;
    }

    public static bool IsValidGradoToxicidad(this int grado) {
        return grado > 0;
    }

    public static bool IsValidIntensidadEfecto(this int intensidad) {
        return intensidad > 0;
    }

    public static bool IsValidDuracion(this int duracion) {
        return duracion > 0;
    }

    public static bool IsValidNombre(this string nombre) {
        return !string.IsNullOrEmpty(nombre);
    }
    
    public static bool IsValidDescripcion(this string descripcion) {
        return !string.IsNullOrEmpty(descripcion);
    }

    public static bool IsValidDisponibilidad(this Disponibilidad disponibilidad) {
        return System.Enum.IsDefined(typeof(Disponibilidad), disponibilidad);
    }

    public static bool IsValidNivelPeligro(this NivelDePeligro nivelDePeligro) {
        return System.Enum.IsDefined(typeof(NivelDePeligro), nivelDePeligro);
    }

    public static bool IsValidSintomasSustancia(this string sintomas) {
        return !string.IsNullOrEmpty(sintomas);
    }

    public static bool IsValidEfectosSecundarios(this string efectos) {
        return !string.IsNullOrEmpty(efectos);
    }

    public static bool IsValidViaDeAdministracion(this ViaDeAdministracion viaDeAdministracion) {
        return System.Enum.IsDefined(typeof(ViaDeAdministracion), viaDeAdministracion);
    }

    public static bool IsValidAntidoto(this Medicina? antidoto) {
        return antidoto != null || antidoto is not Medicina;
    }

    public static bool IsValidContraIndicaciones(this string contraindicaciones) {
        return !string.IsNullOrEmpty(contraindicaciones);
    }

    public static bool IsValidRiegoUsoExcesivo(this string riesgo) {
        return !string.IsNullOrEmpty(riesgo);
    }
}