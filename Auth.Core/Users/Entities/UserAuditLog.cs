namespace Auth.Core.Users.Entities;

public class UserAuditLog
{
    public int Id { get; set; }
    public string IpAddress { get; set; }
    public string Description { get; set; }
    public string Features { get; set; }
    public string Application { get; set; }

    public Guid UserId { get; set; }
    public User User { get; set; }
}
