namespace WebAPI.Domain.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public sealed class JsonOnlyAttribute : Attribute
{
}
