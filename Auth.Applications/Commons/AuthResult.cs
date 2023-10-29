namespace Auth.Applications.Commons;

public class AuthResult
{
    public string EmpCode { get; set; }
    public string Token { get; set; }
    public string EmpName { get; set; }

    public int PermissionLevel { get; set; }

    public List<MenuResult> Menus { get; set; } = new();



}

public class MenuResult
{
    public string Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public bool ShowInSidebar { get; set; }
    public string Url { get; set; } = string.Empty;
    public string Icon { get; set; } = string.Empty;
    public int Order { get; set; }
    public bool Create { get; set; }
    public bool View { get; set; }
    public bool Modify { get; set; }

    public List<MenuResult> MenuChildren { get; set; } = new();

}