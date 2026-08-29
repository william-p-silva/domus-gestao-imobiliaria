
namespace Domus.Domain.Exceptions.Domain;

public class NotFoundException : DomainException
{
    public NotFoundException(string message) : base(message)
    {
    }
}
