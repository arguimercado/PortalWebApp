namespace Auth.Applications.Features.Users.DTO;


public class UserSettingsDTO
{
    public string UserId { get; set; } = string.Empty;
    public IEnumerable<UserMenuDTO> MenuSettings { get; set; } = new List<UserMenuDTO>();
}

public class UserMenuDTO
{
    public string MenuId { get; set; } = string.Empty;

    public string MenuName { get; set; } = string.Empty;
    public string MenuDescription { get; set; } = string.Empty;
    public string MenuIcon { get; set; } = string.Empty;
    public string Module { get; set; }

    public bool Create { get; set; }
    public bool View { get; set; }
    public bool Modify { get; set; }

    public List<UserMenuDTO> SubMenus { get; set; } = new();


}
