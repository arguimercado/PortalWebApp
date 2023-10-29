using BaseEntityPack.Core;

namespace Auth.Core.Users.Entities;

public class UserDocTypeAccess : Entity<Guid>
{
    public UserDocTypeAccess() : base(Guid.NewGuid())
    {
    }

    public UserDocTypeAccess(Guid id, string docTypeCode, Guid userId, bool hasAccess = true) : base(id) {   
        DocTypeCode = docTypeCode;
        UserId = userId;
        HasAccess = hasAccess;
    }
    
    public string DocTypeCode { get; set; } = string.Empty;
    public Guid UserId { get; set; }
    public bool HasAccess { get; set; } = true;


    public User User { get; set; }

}
