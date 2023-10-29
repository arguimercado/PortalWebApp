using Portal.Support.Sessions;
using WebApp.Client.Pages.Authentication.Models;
using WebApp.Service.Http;

namespace WebApp.Client.Pages.Authentication.Data;

public interface IAuthenticationService
{
    Task<UserSession?> AuthenticateUser(LoginModel login);
    Task<UserSession?> ValidateUser(LoginModel login);
}


public class AuthenticationService : IAuthenticationService
{
    private readonly HttpService _httpService;

    public AuthenticationService(HttpService httpService)
    {
        _httpService = httpService;
    }

    public async Task<UserSession?> AuthenticateUser(LoginModel login)
    {
        var url = "account/auth";
        var response = await _httpService.PostAsync<object,UserSession>(url, new { login.EmpCode });

        return response;
    }

    public async Task<UserSession?> ValidateUser(LoginModel login)
    {
        var url = $"account/verify?EmpCode={login.EmpCode}&Password={login.Password}";
        var response = await _httpService.GetAsync<UserSession>(url);
        return response;

    }



}
