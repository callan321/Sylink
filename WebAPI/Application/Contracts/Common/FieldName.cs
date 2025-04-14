using System.Text.Json.Serialization;

namespace WebAPI.Application.Contracts.Common;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum FieldName
{
    Email,
    Password,
    DisplayName,
    General
}
