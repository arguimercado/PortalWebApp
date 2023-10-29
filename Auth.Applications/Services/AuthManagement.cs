using Auth.Applications.Commons;
using Auth.Applications.Commons.Interfaces;
using Auth.Applications.Features.Users.Interfaces;
using Auth.Applications.Menus.Interfaces;
using Auth.Core.Supports;
using Auth.Core.Users;
using Microsoft.EntityFrameworkCore;

namespace Auth.Applications.Services;

public class AuthManagement : IAuthManagement
{
    private readonly IUserManagement _management;
    private readonly IAuthContext _context;
    private readonly IJwtTokenGenerator _jwt;
    private readonly IMenuManagement _menuManagement;

    public AuthManagement(
        IUserManagement management,
        IAuthContext context,
        IJwtTokenGenerator jwt,
        IMenuManagement menuManagement)
    {
        _management = management;
        _context = context;
        _jwt = jwt;
        _menuManagement = menuManagement;
    }


    public async Task<AuthResult> ValidateThruPortal(string username)
    {   
        User user = await _management.GetUserInPortal(username);
        
        return new AuthResult {
            EmpCode = user.UserName,
            EmpName = user.FullName,
            Menus =  await GetFreeUserMenus(),
            Token = _jwt.GenerateToken(user),
        };
        
    }

    public async Task<AuthResult> ExportLogin(string username, int valid)
    {
        var user = await _management.GetUserInPortal(username);
        var authResult = new AuthResult
        {
            EmpCode = user.UserName,
            EmpName = user.FullName,
            Token = _jwt.GenerateToken(user),
        };

        return authResult;
    }

    public async Task<AuthResult> Login(string username, string password)
    {

        var user = await _management.GetUserInPortal(username);
        if (user.Password != SimpleEncode.EncodePassword(password))
            throw new Exception("Password does not match");

        var authResult = new AuthResult
        {
            EmpCode = user.UserName,
            EmpName = user.FullName,
            Token = _jwt.GenerateToken(user),
            PermissionLevel = user.PermissionLevel
        };
        return authResult;


    }

    private async Task<List<MenuResult>> GetFreeUserMenus()
    {
        var menusEnumerable = await  _menuManagement.GetAllMenus();

        List<MenuResult> parentMenus = new List<MenuResult>();

        foreach (var menuEnumerable in menusEnumerable)
        {

            var parentMenu = new MenuResult
            {
                Id = menuEnumerable.Id.ToString(),
                Name = menuEnumerable.Name,
                Description = menuEnumerable.Description,
                Icon = menuEnumerable.Icon,
                Order = menuEnumerable.Order,
                ShowInSidebar = menuEnumerable.ShowInSidebar,
                Url = menuEnumerable.Url,
                Create = true,
                View = true,
                Modify = true,
            };

            var userMenuPref = true;
            if (userMenuPref)
            {
                //child
                var subMenus = menusEnumerable
                    .Where(m => m.ParentId == menuEnumerable.Id)
                    .OrderBy(m => m.Order).ToList();


                foreach (var subMenu in subMenus)
                {
                    var subMenuChildDTO = new MenuResult
                    {
                        Id = subMenu.Id.ToString(),
                        Name = subMenu.Name,
                        Description = subMenu.Description,
                        Icon = subMenu.Icon,
                        Order = subMenu.Order,
                        ShowInSidebar = subMenu.ShowInSidebar,
                        Url = subMenu.Url,
                        Create = true,
                        View = true,
                        Modify = true,
                    };

                    var userSubMenuPref = true;
                    if (userSubMenuPref)
                    {
                        //child
                        var childMenus = menusEnumerable
                            .Where(m => m.ParentId == subMenu.Id)
                            .OrderBy(m => m.Order).ToList();

                        foreach (var childMenu in childMenus)
                        {


                            subMenuChildDTO.MenuChildren.Add(new MenuResult
                            {
                                Id = childMenu.Id.ToString(),
                                Name = childMenu.Name,
                                Description = childMenu.Description,
                                Icon = childMenu.Icon,
                                Order = childMenu.Order,
                                ShowInSidebar = childMenu.ShowInSidebar,
                                Url = childMenu.Url,
                                Create = true,
                                View = true,
                                Modify = true,
                            });

                        }
                    }

                    parentMenu.MenuChildren.Add(subMenuChildDTO);

                }
                parentMenus.Add(parentMenu);
            }

        }





        return parentMenus;
    }



    private async Task<List<MenuResult>> GetUserMenus(User? user, string module)
    {
        var menusEnumerable = user!.Menus
                                .Where(m => m.ParentId == null);

        if (module == "*")
        {
            menusEnumerable = menusEnumerable.Where(m => m.Module != "pmv")
                                .OrderBy(m => m.Order)
                                .ToList();
        }
        else
        {
            menusEnumerable = menusEnumerable.Where(m => m.Module == module).OrderBy(m => m.Order).ToList();
        }

        List<MenuResult> parentMenus = new List<MenuResult>();

        foreach (var menuEnumerable in menusEnumerable)
        {
            var userMenuPref = await _context.AppUserMenu
                                .FirstOrDefaultAsync(p => p.UserId == user.Id && p.AppMenuId == menuEnumerable.Id) ?? new();
            var parentMenu = new MenuResult
            {
                Id = menuEnumerable.Id.ToString(),
                Name = menuEnumerable.Name,
                Description = menuEnumerable.Description,
                Icon = menuEnumerable.Icon,
                Order = menuEnumerable.Order,
                ShowInSidebar = menuEnumerable.ShowInSidebar,
                Url = menuEnumerable.Url,
                Create = userMenuPref.Create,
                View = userMenuPref.View,
                Modify = userMenuPref.Modify,
            };

            if (userMenuPref.View)
            {
                //child
                var subMenus = user.Menus
                    .Where(m => m.ParentId == menuEnumerable.Id)
                    .OrderBy(m => m.Order).ToList();


                foreach (var subMenu in subMenus)
                {
                    var userSubMenuPref = await _context.AppUserMenu
                                        .FirstOrDefaultAsync(p => p.UserId == user.Id && p.AppMenuId == subMenu.Id) ?? new();

                    var subMenuChildDTO = new MenuResult
                    {
                        Id = subMenu.Id.ToString(),
                        Name = subMenu.Name,
                        Description = subMenu.Description,
                        Icon = subMenu.Icon,
                        Order = subMenu.Order,
                        ShowInSidebar = subMenu.ShowInSidebar,
                        Url = subMenu.Url,
                        Create = userSubMenuPref.Create,
                        View = userSubMenuPref.View,
                        Modify = userSubMenuPref.Modify,
                    };

                    if (userSubMenuPref.View)
                    {
                        //child
                        var childMenus = user.Menus
                            .Where(m => m.ParentId == subMenu.Id)
                            .OrderBy(m => m.Order).ToList();

                        foreach (var childMenu in childMenus)
                        {
                            var userChildMenuPref = await _context.AppUserMenu
                                        .FirstOrDefaultAsync(p => p.UserId == user.Id && p.AppMenuId == childMenu.Id) ?? new();

                            subMenuChildDTO.MenuChildren.Add(new MenuResult
                            {
                                Id = childMenu.Id.ToString(),
                                Name = childMenu.Name,
                                Description = childMenu.Description,
                                Icon = childMenu.Icon,
                                Order = childMenu.Order,
                                ShowInSidebar = childMenu.ShowInSidebar,
                                Url = childMenu.Url,
                                Create = userChildMenuPref.Create,
                                View = userChildMenuPref.View,
                                Modify = userChildMenuPref.Modify,
                            });

                        }
                    }

                    parentMenu.MenuChildren.Add(subMenuChildDTO);

                }
                parentMenus.Add(parentMenu);
            }

        }





        return parentMenus;
    }
}
