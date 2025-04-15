namespace WebAPI.Domain.Attributes;

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
public sealed class ProducesAuthCookiesAttribute : Attribute { }
