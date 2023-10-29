using Auth.Applications.Commons;

namespace Auth.Applications.Features.Users.Interfaces
{
    public interface IAuthManagement
    {
        Task<Result<AuthResult>> Login(string username, string password, string module = "*");
        Task<AuthResult?> ValidateThruPortal(string username);
        Task<AuthResult?> ExportLogin(string username, int valid);
    }
}
