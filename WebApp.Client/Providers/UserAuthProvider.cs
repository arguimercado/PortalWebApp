using Microsoft.AspNetCore.Components.Authorization;
using Newtonsoft.Json;
using Portal.Support.Sessions;

namespace WebApp.Client.Providers
{
    public class UserAuthProvider
    {
        private readonly CustomAuthStateProvider _authProvider;

        public UserAuthProvider(AuthenticationStateProvider authProvider)
        {
            _authProvider = (CustomAuthStateProvider)authProvider;
        }

        public async Task ExtractUser() {
            
            var authState = await _authProvider.GetAuthenticationStateAsync();
            var claim = authState.User.Claims.Where(c => c.Type == "Session").FirstOrDefault();
            if (claim != null) {
                var deserializeSessionObject = JsonConvert.DeserializeObject<UserSession>(claim.Value) ?? new UserSession();
                UserName = deserializeSessionObject.EmpCode;
            }
        }

        public string UserName { get; set; } = "";

    }
}
