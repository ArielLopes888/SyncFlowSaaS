namespace Shared.Core.Abstractions;

public interface ITenantProvider
{
    string GetTenantId();
}
