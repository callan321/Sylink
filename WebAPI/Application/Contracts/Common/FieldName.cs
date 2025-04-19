using System.Text.Json.Serialization;

namespace WebAPI.Application.Contracts.Common;

/// <summary>
/// Field names used in validation errors.
/// 
/// Values use camelCase to match frontend form control names.
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum FieldName
{
    email,
    password,
    displayName,
    general
}
