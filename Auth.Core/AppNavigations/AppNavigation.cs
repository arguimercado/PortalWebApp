using Auth.Core.Permissions;
using BaseEntityPack.Core;

namespace Auth.Core.AppNavigations;

public sealed class AppNavigation : AggregateRoot<Guid> {
    
    public AppNavigation() : base(Guid.NewGuid()) {

    }



    public string Name { get; set; }
    public string Title { get; set; }
    public string Icon { get; set; }
    public string Url { get; set; }
    public int ParentId { get; set; }
    public Guid ApplicationId { get; set; }

    private IList<AppNavigation> _apps = new List<AppNavigation>();
    public IEnumerable<AppNavigation> Navigations => _apps.ToList();
    public IEnumerable<Permission> Permissions { get; set; }

}
