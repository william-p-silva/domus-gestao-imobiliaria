

namespace Domus.Domain.Exceptions.Domain;

public class ValidationException : DomainException
{
    public ValidationException(string message) : base(message)
    {
    }
}
