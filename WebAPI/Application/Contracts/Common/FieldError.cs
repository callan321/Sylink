namespace WebAPI.Application.Contracts.Common;

public class FieldError
{
    public required FieldName Field { get; set; }
    public required string Message { get; set; }
}
