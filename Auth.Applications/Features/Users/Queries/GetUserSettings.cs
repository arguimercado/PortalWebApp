using Auth.Applications.Commons.Interfaces;
using Auth.Applications.Features.Users.DTO;
using Auth.Core.Menu;
using Microsoft.EntityFrameworkCore;

namespace Auth.Applications.Features.Users.Queries
{
    public static class GetUserSettings
    {
        public record Query(string UserId) : IRequest<Result<UserSettingsDTO>>;

        public class QueryHandler : IRequestHandler<Query, Result<UserSettingsDTO>>
        {

            private readonly IAuthContext _authContext;

            public QueryHandler(IAuthContext authContext)
            {
                _authContext = authContext;
            }
            public async Task<Result<UserSettingsDTO>> Handle(Query request, CancellationToken cancellationToken)
            {
                try
                {

                    return Result.Ok(new UserSettingsDTO
                    {
                        UserId = request.UserId,
                        MenuSettings = await GetUserMenuAccess(request)
                    });
                }
                catch (Exception ex)
                {
                    return Result.Fail(ex.Message);
                }

            }

            private async Task<IEnumerable<UserMenuDTO>> GetUserMenuAccess(Query request)
            {

                //menu settings
                //get the menus



                var menus = await _authContext.AppMenus.ToListAsync();

                var parentMenus = menus.Where(m => m.ParentId == null)
                                        .OrderBy(m => m.Order)
                                        .Select(m => new UserMenuDTO
                                        {
                                            MenuId = m.Id.ToString(),
                                            MenuName = m.Name,
                                            MenuDescription = m.Description,
                                            MenuIcon = m.Icon,
                                            Module = m.Module!
                                        }).ToList();

                var userAppMenus = await _authContext.AppUserMenu
                                           .Where(au => au.UserId == Guid.Parse(request.UserId))
                                           .ToListAsync();


                foreach (var menu in parentMenus)
                {

                    var subMenus = menus.Where(m => m.ParentId == Guid.Parse(menu.MenuId))
                                        .OrderBy(m => m.Order)
                                        .Select(m => new UserMenuDTO
                                        {
                                            MenuId = m.Id.ToString(),
                                            MenuName = m.Name,
                                            MenuDescription = m.Description,
                                            MenuIcon = m.Icon,
                                            Module = m.Module!
                                        }).ToList();

                    foreach (var subMenu in subMenus)
                    {
                        List<UserMenuDTO> childrenOfSubMenus = menus.Where(m => m.ParentId == Guid.Parse(subMenu.MenuId))
                                                            .Select(m => new UserMenuDTO
                                                            {
                                                                MenuId = m.Id.ToString(),
                                                                MenuDescription = m.Description,
                                                                MenuName = m.Name,
                                                                MenuIcon = m.Icon,
                                                                Module = m.Module!
                                                            }).ToList() ?? new();
                        if (childrenOfSubMenus.Count() > 0)
                        {
                            foreach (var child in childrenOfSubMenus)
                            {
                                var userChildPref = userAppMenus.FirstOrDefault(ua => ua.AppMenuId == Guid.Parse(child.MenuId));
                                if (userChildPref != null)
                                {
                                    child.Create = userChildPref.Create;
                                    child.View = userChildPref.View;
                                    child.Modify = userChildPref.Modify;
                                }
                                subMenu.SubMenus.Add(child);
                            }
                        }

                        var userSubPref = userAppMenus.FirstOrDefault(ua => ua.AppMenuId == Guid.Parse(subMenu.MenuId));
                        if (userSubPref != null)
                        {
                            subMenu.Create = userSubPref.Create;
                            subMenu.View = userSubPref.View;
                            subMenu.Modify = userSubPref.Modify;
                        }

                        menu.SubMenus.Add(subMenu);
                    }

                    var userPref = userAppMenus.FirstOrDefault(ua => ua.AppMenuId == Guid.Parse(menu.MenuId));
                    if (userPref != null)
                    {
                        menu.Create = userPref.Create;
                        menu.View = userPref.View;
                        menu.Modify = userPref.Modify;
                    }
                }


                return parentMenus;
            }
        }
    }
}
