namespace Shared.Core.Abstractions;

public interface ITenantProvider
{
    Guid GetTenantId();
}
