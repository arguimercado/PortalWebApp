namespace Auth.Applications.Menus.DTO;

public class MenuRegister
{
    public string Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public bool ShowInSidebar { get; set; }

    public string Module { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public string Icon { get; set; } = string.Empty;
    public int Order { get; set; }
    public string? ParentId { get; set; } = null;
}
