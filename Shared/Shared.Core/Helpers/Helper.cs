namespace Shared.Core.Helpers;

public static class StringHelper
{
    public static bool HasValue(this string? input)
        => !string.IsNullOrWhiteSpace(input);
}
