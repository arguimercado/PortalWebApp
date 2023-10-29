using Auth.Applications.Db;
using Auth.Applications.Features.Users.DTO;
using Auth.Applications.Features.Users.Interfaces;
using Auth.Core.Menu;
using Auth.Core.Users;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Auth.Applications.Services
{
    internal class UserManagement : IUserManagement
    {
        private readonly AuthContext _authContext;
        private readonly SqlDataConnection _sqlConnection;

        public UserManagement(AuthContext authContext,SqlDataConnection sqlConnection)
        {
            _authContext = authContext;
            _sqlConnection = sqlConnection;
        }

        public Task<IEnumerable<User>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<AppUserMenu> GetAppMenusByUserAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetByUserId(string userId)
        {
            var user = await _authContext.Users
                        .Include(u => u.Menus)
                        .FirstOrDefaultAsync(u => u.Id == Guid.Parse(userId));
            
            if(user is null)
                throw new NullReferenceException();

            return user;
        }

        public async Task<User> GetUserInPortal(string empCode) {

            var sql = @"SELECT 
                            e.EmpCode UserName,
                            l.EmpPasswd Password,
                            e.EmpName FullName,
                            e.Designation Designation,
                            e.VisaDesignation Company,
                            e.PresentEmail Email, 
                            e.LocationOfWork Location, 
                            e.EmpRejoinDt DateJoined,
                            e.Active IsActive,
                            a.permissionLevel PermissionLevel 
                        FROM EmployeeMaster e 
                        INNER JOIN EmployeeLogin l ON l.EmpCode = e.EmpCode  
                        LEFT JOIN ADM_UserSettings a ON a.EmpCode = e.EmpCode 
                        WHERE e.Active = 1 AND e.EmpCode = @empCode ";
            try
            {
                _sqlConnection.Open();
                var results = await _sqlConnection.GetConnection().QueryAsync<User>(sql, new { empCode = empCode });
                _sqlConnection.Close();
                
                if (results.Count() == 0)
                    throw new Exception("User is registered in the portal");
                
                return results.First(); 
            }
            catch(SqlException ex)
            {
                if(con.State == System.Data.ConnectionState.Open)
                {
                    _sqlConnection.Close();
                }

                throw new Exception(ex.InnerException!.Message,ex);
            }
            

        }

        public async Task SaveAndCreateUser(User user)
        {
            _authContext.Users.Add(user);
            await _authContext.SaveChangesAsync();
        }

        public async Task<User?> GetByUserNameAsync(string userName)
        {
            var user = await _authContext.Users
                                .Include(u => u.Menus)
                                .FirstOrDefaultAsync(u => u.UserName == userName);

            return user;
        }

       
        private async Task UpdateMenuRecur(List<UserMenuDTO> menus, string userId)
        {
            if(menus.Count > 0)
            {
                foreach(var menu in menus)
                {
                    if(menu.SubMenus.Count() > 0)
                    {
                        var childMenu = menu.SubMenus.ToList() ?? new();
                        await UpdateMenuRecur(childMenu,userId);
                    }

                    //check if exist
                    var userAppMenu = await _authContext.AppUserMenu
                                .FirstOrDefaultAsync(a => a.UserId == Guid.Parse(userId) && a.AppMenuId == Guid.Parse(menu.MenuId));

                    if(userAppMenu is null)
                    {
                        userAppMenu = new AppUserMenu
                        {
                            UserId = Guid.Parse(userId),
                            AppMenuId = Guid.Parse(menu.MenuId),
                            Create = menu.Create,
                            View = menu.View,
                            Modify = menu.Modify,
                            Action = ""
                        };
                        _authContext.AppUserMenu.Add(userAppMenu);
                    }
                    else
                    {
                        userAppMenu.Create = menu.Create;
                        userAppMenu.View = menu.View;
                        userAppMenu.Modify = menu.Modify;
                        
                        _authContext.AppUserMenu.Update(userAppMenu);
                    }
                }
            }
            //base case
            
        }

        public async Task UpdateUserSettings(UserSettingsDTO userSettings)
        {
            //update the menu
            await UpdateMenuRecur(userSettings.MenuSettings.ToList(),userSettings.UserId);

            await _authContext.SaveChangesAsync();

        }
    }
}
