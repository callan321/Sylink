namespace WebAPI.Application.Contracts.Common;

public class OperationResult<T>
{
    public bool Success { get; set; }
    public T? Data { get; set; }
    public required string Message { get; set; }
    public List<FieldError>? Errors { get; set; }
    public string? ErrorCode { get; set; }
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    public bool IsSuccess { get; internal set; }

    public static OperationResult<T> Ok(T data, string message = "Success") =>
        new() { Success = true, Data = data, Message = message };

    public static OperationResult<T> Fail(string message, List<FieldError>? errors = null, string? code = null) =>
        new() { Success = false, Message = message, Errors = errors, ErrorCode = code };
}
