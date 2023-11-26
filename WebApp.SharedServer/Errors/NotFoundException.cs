namespace WebApp.SharedServer.Errors;

public class NotFoundException : Exception
{
    public NotFoundException(string message) : base($"{message} not found")
    {
        
    }
}
