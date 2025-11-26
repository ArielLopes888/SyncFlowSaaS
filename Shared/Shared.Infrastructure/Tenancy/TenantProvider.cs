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

    public string GetTenantId()
    {
        var tenant =
            _httpContextAccessor.HttpContext?.Items["TenantId"]?.ToString();

        if (string.IsNullOrWhiteSpace(tenant))
            throw new InvalidOperationException("TenantId não encontrado no contexto da requisição.");

        return tenant;
    }
}
