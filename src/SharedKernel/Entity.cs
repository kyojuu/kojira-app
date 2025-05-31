namespace SharedKernel;

public abstract class Entity
{
    private readonly List<IDomainEvents> _domainEvents = [];

    public List<IDomainEvents> DomainEvents => [.. _domainEvents];

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }

    public void Raise(IDomainEvents domainEvents)
    {
        _domainEvents.Add(domainEvents);
    }
}
