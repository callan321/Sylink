using WebAPI.Application.Contracts.Common;

public class OperationResult<T>
{
    public bool Success { get; set; }
    public T? Data { get; set; }
    public string Message { get; set; } = string.Empty;
    public List<FieldError>? Errors { get; set; }
    public string? ErrorCode { get; set; }

    public static OperationResult<T> Ok(T? data, string message = "Success") => new()
    {
        Success = true,
        Data = data,
        Message = message
    };

    public static OperationResult<T> Fail(string message, List<FieldError>? errors = null, string? errorCode = null) => new()
    {
        Success = false,
        Data = default,
        Message = message,
        Errors = errors,
        ErrorCode = errorCode
    };
}
