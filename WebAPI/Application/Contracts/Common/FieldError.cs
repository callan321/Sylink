namespace WebAPI.Application.Contracts.Common;

public class FieldError
{
    public string? Field { get; set; }
    public required string Message { get; set; }
}
