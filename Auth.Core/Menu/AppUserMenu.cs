using Auth.Core.Users;

namespace Auth.Core.Menu
{
    public class AppUserMenu 
    {

        public Guid UserId { get; set; }
        public Guid AppMenuId { get; set; }

        public bool Create { get; set; }
        public bool View { get; set; }
        public bool Modify { get; set; }

        public string Action { get; set; }


        public User User { get; set; }

     
        public AppMenu Menu { get; set; }

    }
}
