using Auth.Applications.Commons;

namespace Auth.Applications.Features.Users.Interfaces
{
    public interface IAuthManagement
    {
        Task<AuthResult> Login(string username, string password);
        Task<AuthResult> ValidateThruPortal(string username);
        Task<AuthResult> ExportLogin(string username, int valid);
    }
}
