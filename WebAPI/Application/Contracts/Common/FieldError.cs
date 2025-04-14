namespace WebAPI.Application.Contracts.Common;

public class FieldError
{
    public required string Field { get; set; }
    public required string Message { get; set; }
}
