using BoticaImperial.Errors.Common;

namespace BoticaImperial.Errors;

public abstract record SustanciaError(string Message) : DomainError(Message) {
    public sealed record Validation(IEnumerable<string> errores)
        : DomainError("Se han encontrado errores en el proceso de validacion de la entidad, entidad no apta.");
}

public static class SustanciaErrors {
    public static DomainError Validation(IEnumerable<string> errores) {
        return new SustanciaError.Validation(errores);
    }
}