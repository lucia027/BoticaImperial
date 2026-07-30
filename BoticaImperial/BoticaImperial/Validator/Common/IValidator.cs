using BoticaImperial.Errors.Common;
using CSharpFunctionalExtensions;

namespace BoticaImperial.Validator.Common;

public interface IValidator<T> {
    Result<T, DomainError> Validate(T entity);
}