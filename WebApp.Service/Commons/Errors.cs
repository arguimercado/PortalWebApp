namespace WebApp.Service.Commons;

public enum EnumStatusCode
{
    Success,
    BadResponse
}

public class ErrorModel
{
    public ErrorModel()
    {
        
    }
    public int Status { get; set; }
    public string Title { get; set; } = "";

    public string Detail { get; set; } = "";

}

public class ErrorResultException : Exception
{
    public ErrorResultException(ErrorModel error)
    {
        Error = error;
    }
    public ErrorModel Error { get; set; }
}
