using Microsoft.AspNetCore.Components.Authorization;
using Portal.Support.Sessions;
using Radzen;
using WebApp.Client.Pages.Authentication.Data;
using WebApp.Client.Pages.Authentication.Models;
using WebApp.Client.Providers;
using WebApp.UILibrary.Commons;

namespace Portal.WebClient.Pages.Accounts.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private readonly IAuthenticationService _service;
        private readonly AuthenticationStateProvider _authStateProvider;
        private readonly NotificationService _notification;

        public LoginViewModel(
            IAuthenticationService service,
            AuthenticationStateProvider authenticationStateProvider, NotificationService notification)
        {

            _service = service;
            _authStateProvider = authenticationStateProvider;
            _notification = notification;
        }

        public async Task<Result<UserSession>> Validate(LoginModel login)
        {
            try
            {
                var userSession = await _service.ValidateUser(login);
                var customAuthStateProvider = (CustomAuthStateProvider)_authStateProvider;
                await customAuthStateProvider.UpdateAuthenticationState(userSession);

                Notify("updatelogin");

                return Result<UserSession>.Success(userSession!);
            }
            catch (Exception ex) {
                _notification.Notify(NotificationSeverity.Error, detail: $"Error: {ex.Message}");
                return Result<UserSession>.Failure(ex.Message);
            }
        }

        public async Task<Result<UserSession>> Login(LoginModel login)
        {
            try
            {

                var userSession = await _service.AuthenticateUser(login);
                var customAuthStateProvider = (CustomAuthStateProvider)_authStateProvider;
                await customAuthStateProvider.UpdateAuthenticationState(userSession);

                Notify("updatelogin");

                return Result<UserSession>.Success(userSession!);

            }
            catch (Exception ex)
            {
                _notification.Notify(NotificationSeverity.Error, detail: $"Error: {ex.Message}");
                return Result<UserSession>.Failure(ex.Message);
            }
        }

        public async Task Logout()
        {
            var customAuthStateProvider = (CustomAuthStateProvider)_authStateProvider;

            await customAuthStateProvider.UpdateAuthenticationState(null);


        }






    }
}
