namespace WebApp.SharedServer.Commons;


public class SelectItem
{
    public string Key { get; set; } = "";
    public string Text { get; set; } = "";
    public string Value { get; set; } = "";
    public string? Type { get; set; } 
    public bool IsSelected { get; set; }
}
