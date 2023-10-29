using Auth.Core.Users;

namespace Auth.Applications.Commons.Interfaces;

public interface IJwtTokenGenerator
{
    string GenerateToken(User user);
}
