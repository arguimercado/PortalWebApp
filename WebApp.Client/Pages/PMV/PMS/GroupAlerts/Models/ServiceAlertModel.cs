namespace WebApp.Client.Pages.PMV.PMS.GroupAlerts.Models;

public class ServiceAlertModel
{
    public string ServiceCode { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string ServiceCategory { get; set; } = string.Empty;
    public int KmAlert { get; set; }
    public int KmInterval { get; set; }
}
