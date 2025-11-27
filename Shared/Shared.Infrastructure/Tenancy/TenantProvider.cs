using Microsoft.AspNetCore.Http;
using Shared.Core.Abstractions;

namespace Shared.Infrastructure.Tenancy;

public class TenantProvider : ITenantProvider
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public TenantProvider(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid GetTenantId()
    {
        var tenant =
            _httpContextAccessor.HttpContext?.Items["TenantId"]?.ToString();

        if (string.IsNullOrWhiteSpace(tenant))
            throw new InvalidOperationException("TenantId não encontrado no contexto da requisição.");

        if (!Guid.TryParse(tenant, out var tenantId))
            throw new InvalidOperationException("TenantId no contexto da requisição não é um GUID válido.");

        return tenantId;
    }
}
