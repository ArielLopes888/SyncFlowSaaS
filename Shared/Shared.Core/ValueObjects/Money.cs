using Shared.Core.Domain;

namespace Shared.Core.ValueObjects;

public sealed class Money : ValueObject
{
    public decimal Amount { get; }
    public string Currency { get; }

    private Money(decimal amount, string currency)
    {
        Amount = amount;
        Currency = currency;
    }

    public static Money From(decimal amount, string currency = "BRL")
    {
        amount = Math.Round(amount, 2, MidpointRounding.AwayFromZero);
        return new Money(amount, currency);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Amount;
        yield return Currency;
    }

    public override string ToString() => $"{Amount} {Currency}";
}
