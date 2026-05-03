using MediatR;

namespace Ordering.Domain.Abstractions;
public interface IDomainEvent : INotification
{
    Guid EventId => Guid.NewGuid();
    public DateTime OccurredOn => DateTime.Now;
    
    /// <summary>
    /// /Represents which class throwing this domain event understanding from the assembly
    /// </summary>
    public string EventType => GetType().AssemblyQualifiedName;
}
