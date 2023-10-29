namespace WebApp.Client.Pages.Authentication.Models;

public class LoginModel
{
    public LoginModel()
    {
        
    }
    public string EmpCode { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;

    public string Module => "pmv";
}

public class UserModel
{
    public string EmpCode { get; set; } = string.Empty;
    public string Token { get; set; } = string.Empty;


}