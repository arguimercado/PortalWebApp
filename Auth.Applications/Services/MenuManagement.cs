using Auth.Applications.Db;
using Auth.Applications.Menus.DTO;
using Auth.Applications.Menus.Interfaces;
using Auth.Core.Menu;
using Microsoft.EntityFrameworkCore;

namespace Auth.Applications.Services;

public class MenuManagement : IMenuManagement
{
    private readonly AuthContext _authContext;

    public MenuManagement(AuthContext authContext)
    {
        _authContext = authContext;
    }

    public async Task<IEnumerable<AppMenu>> GetAllMenus()
    {
        return await _authContext.AppMenus
            .OrderBy(a => a.Order)
            .ToListAsync();
    }

    public async Task Create(MenuRegister register)
    {
        
        var menu = new AppMenu(register.Name, register.Description,register.Module, 
            register.Url,register.Icon,register.Order,register.ShowInSidebar, 
            register.ParentId != null ? Guid.Parse(register.ParentId) : null);

        _authContext.AppMenus.Add(menu);
       
        await _authContext.SaveChangesAsync();
    
    }


    public async Task Update(MenuRegister registerDTO)
    {
        var menu = await _authContext.AppMenus.FirstOrDefaultAsync(m => m.Id == Guid.Parse(registerDTO.Id));
        
        if (menu is null)
            throw new NullReferenceException();
        
        menu.Name = registerDTO.Name;
        menu.Description = registerDTO.Description;
        menu.Url = registerDTO.Url;
        menu.Icon = registerDTO.Icon;
        menu.Order = registerDTO.Order;
        menu.ShowInSidebar = registerDTO.ShowInSidebar;
        menu.ParentId = registerDTO.ParentId != null ? Guid.Parse(registerDTO.ParentId) : null;
        
        await _authContext.SaveChangesAsync();
    }

}
