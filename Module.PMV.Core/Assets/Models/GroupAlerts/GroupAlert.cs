namespace Module.PMV.Core.Assets.Models.GroupAlerts;


[Auditable]
public class GroupAlert : AggregateRoot<Guid>
{


    private List<GroupAlertDetail> _newDetails = new();

    public GroupAlert() : base(Guid.NewGuid())
    {

    }

    public GroupAlert(string groupName, string description)
        : base(Guid.NewGuid())
    {
        GroupName = groupName;
        Description = description;
    }


    public string GroupName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public bool InActive { get; private set; } = false;

    private List<GroupAlertDetail> _details = new List<GroupAlertDetail>();

    public IEnumerable<GroupAlertDetail> Details => new List<GroupAlertDetail>(_details);

    [NotMapped]
    public IEnumerable<GroupAlertDetail> NewDetails => _newDetails;

    public void AddDetail(Guid guid, string name, string serviceId, int kmAlert, int kmInterval, string smu)
    {

        _details.Add(new GroupAlertDetail(guid, serviceId, name, kmAlert, kmInterval, smu, this));
    }

    public void AddNewDetail(Guid guid, string name, string serviceId, int kmAlert, int kmInterval, string smu)
    {
        _newDetails.Add(new GroupAlertDetail(guid, serviceId, name, kmAlert, kmInterval, smu, this));
    }

    public void RemoveDetail(string[] ids)
    {
        foreach (var id in ids)
        {
            var detail = _details.Where(d => d.Id == Guid.Parse(id)).FirstOrDefault();
            if (detail is not null)
                detail.Tracker = "D";
        }
    }

    public void UpdateDetail(Guid id, int kmAlert, int kmInterval, string smu, string employeeCode, bool notifyAlert, bool notifyAfterInterval)
    {
        var detail = _details.Where(d => d.Id == id).FirstOrDefault();

        if (detail is not null)
        {
            detail.KmAlert = kmAlert;
            detail.KmInterval = kmInterval;
            detail.SMU = smu;
            detail.NotifyManagementAfterInterval = notifyAfterInterval;
            detail.NotifyManagementWhenAlert = notifyAlert;
        }
    }


}