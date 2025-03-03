namespace Restaurants.Domain.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string message) : base(message)
    {

    }

    public NotFoundException(string message,string id) : base(message)
    {

    }
}
