using Auth.Applications.Menus.DTO;
using Auth.Core.Menu;

namespace Auth.Applications.Menus.Interfaces
{
    public interface IMenuManagement
    {

        Task Create(MenuRegister register);

        Task Update(MenuRegister register);

        Task<IEnumerable<AppMenu>> GetAllMenus();


    }
}
