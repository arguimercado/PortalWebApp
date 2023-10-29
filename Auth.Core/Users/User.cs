using Auth.Core.Menu;
using Auth.Core.Supports;
using Auth.Core.Users.Entities;
using BaseEntityPack.Core;

namespace Auth.Core.Users;

public class User : AggregateRoot<Guid>
{
    public User() : base(Guid.NewGuid())
    {

    }

    public User(Guid id,string userName) : base(id)
    {
        UserName= userName;
    }

    public User(Guid Id,
                string userName,
                string fullName,
                string location,
                string company,
                string designation,
                DateTime dateJoined,
                bool isActive,
                string email, string password, List<AppMenu> menus) : base(Id)
    {
        UserName = userName;
        FullName = fullName;
        Location = location;
        Company = company;
        Designation = designation;
        DateJoined = dateJoined;
        IsActive = isActive;
        Email = email;
        SetPassword(password);

        _menus = menus;
    }

    public User(Guid Id,
                string userName,
                string fullName,
                string location,
                string company,
                string designation,
                DateTime dateJoined,
                bool isActive,
                string email, string password) : base(Id)
    {
        UserName = userName;
        FullName = fullName;
        Location = location;
        Company = company;
        Designation = designation;
        DateJoined = dateJoined;
        IsActive = isActive;
        Email = email;
        SetPassword(password);
    }


    public string UserName { get; set; }
    public string Password { get; set; }
    public string FullName { get; set; }
    public string? Location { get; set; }
    public string? Company { get; set; }
    public string? Designation { get; set; }
    public DateTime? DateJoined { get; set; }

    public bool IsActive { get; set; }
    public string? Email { get; set; }
    public int PermissionLevel { get; set; }

    public IEnumerable<UserDocTypeAccess> DocTypeAccess { get; set; }
    public IEnumerable<UserProjectAccess> ProjectAccess { get; set; }
    public IEnumerable<UserStoreAccess> StoreAccess { get; set; }
    public IEnumerable<UserAuditLog> AuditLogs { get; set; }

    private List<AppMenu> _menus = new List<AppMenu>();
    public IEnumerable<AppMenu> Menus  => _menus.ToList();

    public void SetPassword(string password) {
        Password = SimpleEncode.EncodePassword(password);
    }

}
