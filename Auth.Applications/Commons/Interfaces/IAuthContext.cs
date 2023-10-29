using Auth.Core.Menu;
using Auth.Core.Users;
using Microsoft.EntityFrameworkCore;

namespace Auth.Applications.Commons.Interfaces;

public interface IAuthContext
{
    public DbSet<User> Users { get; set; }

    public DbSet<AppMenu> AppMenus { get; set; }

    DbSet<AppUserMenu> AppUserMenu { get; set; }
}
