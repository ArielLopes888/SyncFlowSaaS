using Shared.Core.Domain;
using Shared.Core.Exceptions;

namespace Shared.Core.ValueObjects;

public sealed class Email : ValueObject
{
    public string Value { get; }

    private Email(string value) => Value = value;

    public static Email Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || !value.Contains('@'))
            throw new ValidationException(new[] { "Invalid email" });

        return new Email(value.Trim().ToLower());
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString() => Value;
}
