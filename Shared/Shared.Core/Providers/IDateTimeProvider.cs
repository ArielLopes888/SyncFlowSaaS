namespace Shared.Core.Providers;

public interface IDateTimeProvider
{
    DateTime UtcNow { get; }
}
