namespace Asset.Core.Models.GroupAlerts
{
    [Auditable]
    public class GroupAlertDetail : Entity<Guid>
    {
        public GroupAlertDetail() : base(Guid.NewGuid())
        {

        }

        public GroupAlertDetail(
            Guid id,
            string serviceAlertId,
            string name, int kmAlert, int kmInterval, string smu, GroupAlert group) : base(id)
        {
            ServiceAlertId = serviceAlertId;
            Name = name;
            KmAlert = kmAlert;
            KmInterval = kmInterval;
            Group = group;
            SMU = smu;
        }

        public string ServiceAlertId { get; set; } = "";
        public string Name { get; set; } = string.Empty;
        public int KmAlert { get; set; }
        public int KmInterval { get; set; }
        public string SMU { get; set; } = "KM";

        public GroupAlert Group { get; set; } = new();
        public bool NotifyManagementWhenAlert { get; set; } = false;
        public bool NotifyManagementAfterInterval { get; set; } = false;

        [NotMapped]
        public virtual string Tracker { get; set; } = "";


    }
}
