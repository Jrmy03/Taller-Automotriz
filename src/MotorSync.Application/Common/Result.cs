namespace MotorSync.Application.Common;

public enum ErrorType
{
    None,
    Validation,
    NotFound,
    Conflict
}

/// <summary>
/// Resultado de un caso de uso. Evita usar excepciones para el flujo de negocio
/// y permite que la API traduzca el error al código HTTP adecuado.
/// </summary>
public sealed class Result<T>
{
    public bool IsSuccess { get; }
    public T? Value { get; }
    public string? Error { get; }
    public ErrorType ErrorType { get; }

    private Result(bool isSuccess, T? value, string? error, ErrorType errorType)
    {
        IsSuccess = isSuccess;
        Value = value;
        Error = error;
        ErrorType = errorType;
    }

    public static Result<T> Success(T value) => new(true, value, null, ErrorType.None);

    public static Result<T> Failure(string error, ErrorType errorType) => new(false, default, error, errorType);
}
