namespace WebApp.Client.Pages.PMV.PMS.ServiceLogs.Models;

public class ServiceDueModel
{
    public string Id { get; set; }
    public string ServiceDueId { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public DateTime? LastServiceDate { get; set; }
    public int LastSMUReading { get; set; }
    public int CurrentSMUReading { get; set; }
    public int KmAlert { get; set; }
    public int AlertDue { get; set; }
    public int KmInterval { get; set; }
    public int IntervalDue { get; set; }
    public string Status { get; set; } = string.Empty;
}

public class ServiceDueEntry
{

}