namespace Ordering.Domain.ValueObjects;
public record OrderId
{
    public Guid Value { get; }
    
    public OrderId(Guid value)
    {
        Value = value;
    }
    public static CustomerId Of(Guid value)
    {
        ArgumentNullException.ThrowIfNull(value);
        if (value == Guid.Empty)
        {
            throw new DomainException("CustomerId cannot be empty.");
        }

        return new CustomerId(value);
    }
}