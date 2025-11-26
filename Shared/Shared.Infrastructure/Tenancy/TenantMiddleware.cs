using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Shared.Infrastructure.Tenancy
{
    public class TenantMiddleware
    {
        private readonly RequestDelegate _next;
        private const string HEADER_TENANT = "X-Tenant"; 

        public TenantMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Headers.TryGetValue(HEADER_TENANT, out var tenantHeader))
            {
                context.Items["TenantId"] = tenantHeader.ToString();
            }
            else if (context.User?.Identity?.IsAuthenticated == true)
            {
                var claim = context.User.FindFirst("tenant_id") ?? context.User.FindFirst("tenant");
                if (claim != null)
                    context.Items["TenantId"] = claim.Value;
            }
          await _next(context);
        }
    }
}
