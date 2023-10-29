using BaseEntityPack.Core;

namespace Auth.Core.Users.Entities
{
    public class UserStoreAccess : Entity<Guid> {

        public UserStoreAccess() : base(Guid.NewGuid())
        {

        }

        public UserStoreAccess(Guid id, string storeCode, Guid userId, bool hasAccess = true) : base(id)
        {
            StoreCode = storeCode;
            UserId = userId;
            HasAccess = hasAccess;
        }


        public string StoreCode { get; set; }
        public Guid UserId { get; set; }
        public bool HasAccess { get; set; } = true;

        public User User { get; set; }
    }
}
