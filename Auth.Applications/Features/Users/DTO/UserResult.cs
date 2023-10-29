namespace Auth.Applications.Features.Users.DTO;

public class UserResult
{
    public string Id { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string? Location { get; set; }
    public string? Company { get; set; }
    public string? Designation { get; set; }
    public DateTime? DateJoined { get; set; } = DateTime.Now;


}
