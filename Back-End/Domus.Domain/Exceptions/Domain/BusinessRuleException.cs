
namespace Domus.Domain.Exceptions.Domain;

public class BusinessRuleException : DomainException
{
    public BusinessRuleException(string message) : base(message)
    {
    }
}
