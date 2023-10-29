using BaseEntityPack.Core;

namespace Auth.Core.Users.Entities;

public class UserNotification : Entity<Guid>
{

    public static UserNotification Instance(string type, string link, DateTime? readDate, DateTime? expiryDate, string title, string body)
        => new(Guid.NewGuid(), type, link, readDate, expiryDate, title, body);

    public UserNotification() : base(Guid.NewGuid())
    {

    }

    private UserNotification(Guid id, string type, string link, DateTime? readDate, DateTime? expiryDate, string title, string body) : base(id)
    {
        Type = type;
        Link = link;
        ReadDate = readDate;
        ExpiryDate = expiryDate;
        Title = title;
        Body = body;
    }

    public string Type { get; set; } = string.Empty;
    public string Link { get; set; } = string.Empty;
    public DateTime? ReadDate { get; set; }
    public DateTime? ExpiryDate { get; set; }

    public string Title { get; set; } = string.Empty;
    public string Body { get; set; } = string.Empty;

    public Guid UserId { get; set; }
    public User User { get; set; }



}
