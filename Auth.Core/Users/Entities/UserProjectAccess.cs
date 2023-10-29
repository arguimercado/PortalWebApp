using BaseEntityPack.Core;

namespace Auth.Core.Users.Entities
{
    public class UserProjectAccess : Entity<Guid> {
        public UserProjectAccess() : base(Guid.NewGuid())
        {

        }

        public UserProjectAccess(Guid id,string companyCode, string projectCode, Guid userId, bool hasAccess = true) : base(id) {
            ProjectCode = projectCode;
            UserId = userId;
            HasAccess = hasAccess;
        }

        public string CompanyCode { get; set; } = string.Empty;
        public string ProjectCode { get; set; } = string.Empty;
        public Guid UserId { get; set; }
        public bool HasAccess { get; set; } = true;

        public User User { get; set; }

    }
}
