using Auth.Core.AppNavigations;
using Auth.Core.Users;
using BaseEntityPack.Core;

namespace Auth.Core.Permissions;

public sealed class Permission : AggregateRoot<Guid>
{

    public static Permission Instance(bool hasAccess, string actions, bool appDefault = false)
        => new(Guid.NewGuid(), hasAccess, actions, appDefault);
    public Permission() : base(Guid.NewGuid())
    {

    }
    private Permission(Guid Id, bool hasAccess, string actions, bool appDefault = false) : base(Id)
    {
       
        HasAccess = hasAccess;
        Actions = actions;
        Default = appDefault;
    }


    public IEnumerable<User> Users { get; set; }
    public IEnumerable<AppNavigation> AppNavigations { get; private set; }

    public bool HasAccess { get; set; }
    public string Actions { get; set; }
    public bool Default { get; set; }

}
