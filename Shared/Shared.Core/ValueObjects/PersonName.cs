using Shared.Core.Domain;
using Shared.Core.Exceptions;

namespace Shared.Core.ValueObjects;

public sealed class PersonName : ValueObject
{
    public string FirstName { get; }
    public string LastName { get; }

    private PersonName(string first, string last)
    {
        FirstName = first;
        LastName = last;
    }

    public static PersonName Create(string firstName, string lastName)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            throw new ValidationException(new[] { "First name is required." });

        return new PersonName(firstName.Trim(), lastName?.Trim() ?? "");
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return FirstName;
        yield return LastName;
    }

    public override string ToString() => $"{FirstName} {LastName}".Trim();
}
