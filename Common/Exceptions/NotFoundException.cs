namespace Common.Exceptions
{
    public class NotFoundException(string message, object? data = null) : BaseException(message, data)
    {
    }
}