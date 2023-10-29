using Auth.Applications.Features.Users.DTO;
using Auth.Core.Menu;
using Auth.Core.Users;


namespace Auth.Applications.Features.Users.Interfaces;

public interface IUserManagement
{

    Task<User?> GetByUserNameAsync(string userName);

    Task<User> GetUserInPortal(string empCode);

    Task SaveAndCreateUser(User user);

    Task<AppUserMenu> GetAppMenusByUserAsync(string userId);
    Task<User> GetByUserId(string userId);
    Task<IEnumerable<User>> GetAllAsync();

    Task UpdateUserSettings(UserSettingsDTO userSettings);

}
