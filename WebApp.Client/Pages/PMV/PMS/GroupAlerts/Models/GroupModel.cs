using WebApp.UILibrary.Commons;

namespace WebApp.Client.Pages.PMV.PMS.GroupAlerts.Models;

public class GroupAlertContainerModel
{
    public GroupAlertModel GroupAlert { get; set; } = new();
    public IEnumerable<GroupAlertModel> GroupAlerts { get; set; } = new List<GroupAlertModel>();
    public List<SelectItem> AssetServices { get; set; } = new();
    public IEnumerable<SelectItem> Categories { get; set; } = new List<SelectItem>();
    public IEnumerable<SelectItem> SubCategories { get; set; } = new List<SelectItem>();

}
public class GroupAlertModel
{

    public string Id { get; set; } = "";
    public string GroupName { get; set; } = "";
    public string Description { get; set; } = "";

    public IList<GroupAlertDetailModel> Details { get; set; } = new List<GroupAlertDetailModel>();

    public int TotalAlerts => Details.Count();


    public void AddUpdateDetail(GroupAlertDetailModel model)
    {
        if (!Details.Any(d => d.Id == model.Id))
        {
            model.Id = Guid.NewGuid().ToString();
            model.Index = TotalAlerts + 1;
            Details.Add(GroupAlertDetailModel.Clone(model));
        }
        else
        {
            var detail = Details.SingleOrDefault(d => d.Id == model.Id);

            detail.ServiceAlertId = model.ServiceAlertId;
            detail.Name = model.Name;
            detail.KmAlert = model.KmAlert;
            detail.SMU = model.SMU;
            detail.KmInterval = model.KmInterval;
        }
    }

    public void RemoveDetail(GroupAlertDetailModel model)
    {
        if (Details.Any(d => d.Id == model.Id))
        {
            Details.Remove(model);
        }
    }
}

public class GroupAlertDetailModel
{
    public static GroupAlertDetailModel Clone(GroupAlertDetailModel model)
    {
        return new GroupAlertDetailModel
        {
            Id = model.Id,
            Name = model.Name,
            ServiceAlertId = model.ServiceAlertId,
            KmAlert = model.KmAlert,
            SMU = model.SMU,
            KmInterval = model.KmInterval,
            Index = model.Index,
            NotifyManagementAfterInterval = model.NotifyManagementAfterInterval,
            NotifyManagementWhenAlert = model.NotifyManagementWhenAlert
        };
    }


    public string Id { get; set; } = string.Empty;
    public int Index { get; set; }
    public string ServiceAlertId { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public int KmAlert { get; set; }
    public int KmInterval { get; set; }

    public string SMU { get; set; }



    public bool NotifyManagementWhenAlert { get; set; } = true;
    public bool NotifyManagementAfterInterval { get; set; } = true;
}
