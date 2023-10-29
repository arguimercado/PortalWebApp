using Auth.Core.Users;
using BaseEntityPack.Core;

namespace Auth.Core.Menu
{
    public class AppMenu : AggregateRoot<Guid>
    {
        public AppMenu() : base(Guid.NewGuid())
        {
        }

        public AppMenu(string name,string description,string module, string url, string icon, int order, bool showInSidebar, Guid? parentId) : base(Guid.NewGuid())
        {
            Name = name;
            Url = url;
            Icon = icon;
            Order = order;
            ParentId = parentId;
            Description= description;
            ShowInSidebar= showInSidebar;
            Module= module;
        }

       

        public string Name { get; set; }
        public string Description { get; set; }
        public bool ShowInSidebar { get; set; }
        public string Url { get; set; }
        public string Icon { get; set; }
        public int Order { get; set; }

        public string? Module { get; set; }
        public Guid? ParentId { get; set; }

        public IEnumerable<User> Users { get; set; }

    }
}
