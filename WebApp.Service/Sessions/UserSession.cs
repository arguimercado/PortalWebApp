namespace Portal.Support.Sessions;

public class UserSession
{

    public string EmpCode { get; set; } = "";
    public string EmpName { get; set; } = "";
    public string Token { get; set; } = "";
    public string ImagePath => string.IsNullOrEmpty(EmpCode) ? "" : EmpCode.Substring(1, EmpCode.Length - 1);
    public bool IsLoggedIn { get; set; }

    public IEnumerable<MenuSession> Menus { get; set; } = new List<MenuSession>();
}

public class MenuSession
{
    public string Id { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public bool ShowInSidebar { get; set; }
    public string Url { get; set; } = string.Empty;
    public string Icon { get; set; } = string.Empty;

    public int Order { get; set; }

    public bool Create { get; set; }
    public bool View { get; set; }
    public bool Modify { get; set; }

    public List<MenuSession> MenuChildren { get; set; } = new();
}
