

namespace Domus.Domain.Exceptions.Domain;

public abstract class DomainException : Exception
{
    public DomainException(string message) : base(message) { }
}
