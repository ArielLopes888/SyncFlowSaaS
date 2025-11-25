using Shared.Core.Providers;

namespace Shared.Infrastructure.Providers;

public class GuidProvider : IGuidProvider
{
    public Guid NewGuid() => Guid.NewGuid();
}
