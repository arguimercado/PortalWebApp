using Microsoft.AspNetCore.Components.Authorization;
using Newtonsoft.Json;
using Portal.Support.Sessions;
using System.Security.Claims;
using WebApp.Service.Sessions;

namespace WebApp.Client.Providers;

public class CustomAuthStateProvider : AuthenticationStateProvider
{
    private readonly IStorage _sessionStorage;
    private ClaimsPrincipal _anonymous = new ClaimsPrincipal(new ClaimsIdentity());

    public CustomAuthStateProvider(IStorage sessionStorage)
    {
        _sessionStorage = sessionStorage;
    }



    public async Task RefreshAuthenticate()
    {

        ClaimsPrincipal claimsPrincipal;
        var userSession = await _sessionStorage.ReadAsync<UserSession>(nameof(UserSession));

        if (userSession != null)
        {
            claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim> {
                    new Claim(ClaimTypes.Name,userSession.EmpName),
                    new Claim("Code", userSession.EmpCode)
            }, "JwtAuth"));
        }
        else
        {
            claimsPrincipal = _anonymous;
        }

        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));

    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        try
        {

            var userSession = await _sessionStorage.ReadAsync<UserSession>(nameof(UserSession));

            if (userSession == null)
                return await Task.FromResult(new AuthenticationState(_anonymous));


            var serializeSession = JsonConvert.SerializeObject(userSession);

            var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim> {
                new Claim(ClaimTypes.Name,userSession.EmpName),
                new Claim("Code", userSession.EmpCode),
                new Claim("Session", serializeSession)
            }, "JwtAuth"));

            return await Task.FromResult(new AuthenticationState(claimsPrincipal));
        }
        catch
        {
            return await Task.FromResult(new AuthenticationState(_anonymous));
        }
    }

    public async Task UpdateAuthenticationState(UserSession? userSession)
    {

        if (userSession != null)
        {
            await _sessionStorage.SaveAsync(nameof(UserSession), userSession);
        }
        else
        {

            await _sessionStorage.RemoveAsync(nameof(UserSession));
        }

        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }



    public async Task<string> GetToken()
    {
        var result = string.Empty;

        try
        {
            var userSession = await _sessionStorage.ReadAsync<UserSession>(nameof(UserSession));
            if (userSession != null)
            {
                result = userSession.Token;
            }
        }
        catch (Exception ex)
        {

        }

        return result;
    }
}
