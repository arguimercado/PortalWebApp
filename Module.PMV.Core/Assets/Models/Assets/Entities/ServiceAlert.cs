namespace Module.PMV.Core.Assets.Models.Assets.Entities;

public enum ServiceDueStatus
{
    Good,
    Due,
    OverDue,
    Critical,
    Danger
}


public class ServiceAlert : Entity<Guid>
{
    public ServiceAlert() : base(Guid.NewGuid())
    {

    }

    public ServiceAlert(
       int assetId,
       string groupId,
       string code,
       string name,
       int lastSMUReading,
       int currentSMUReading,
       int kmAlert,
       int kmInterval,
       string createdBy) : base(Guid.NewGuid())
    {

        GroupId = groupId;
        Code = code;
        Name = name;
        KmAlert = kmAlert;
        KmInterval = kmInterval;
        AssetId = assetId;
        LastSMUReading = lastSMUReading;
        CurrentSMUReading = currentSMUReading;
        Source = "Initial";
        CreatedBy = createdBy;
        CreatedAt = DateTime.Now;
    }
    public ServiceAlert(
        Guid id,
        int assetId,
        string groupId,
        string code,
        string name,
        int lastSMUReading,
        int currentSMUReading,
        int kmAlert,
        int kmInterval,
        string createdBy) : base(id)
    {
        GroupId = groupId;
        Code = code;
        Name = name;
        KmAlert = kmAlert;
        KmInterval = kmInterval;
        AssetId = assetId;
        LastSMUReading = lastSMUReading;
        CurrentSMUReading = currentSMUReading;
        Source = "Initial";
        CreatedBy = createdBy;
        CreatedAt = DateTime.Now;
    }

    public int AssetId { get; set; }
    public string GroupId { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public DateTime? LastServiceDate { get; set; }
    public int LastSMUReading { get; set; }
    public int CurrentSMUReading { get; set; }
    public int KmAlert { get; set; }
    public string SMU { get; set; } = "";
    public string Remarks { get; set; } = string.Empty;
    public int AlertDue => KmAlert + LastSMUReading;
    public int KmInterval { get; set; }
    public int IntervalDue => KmInterval + KmAlert + LastSMUReading;
    public string Status => GetStatus();

    private string GetStatus()
    {
        //check if more than interval
        var diffInterval = IntervalDue - CurrentSMUReading;
        var diffAlertDue = AlertDue - CurrentSMUReading;

        if (AlertDue <= CurrentSMUReading && IntervalDue >= CurrentSMUReading)
        {
            return ServiceDueStatus.Due.ToString();
        }
        else if (IntervalDue <= CurrentSMUReading)
        {
            return ServiceDueStatus.OverDue.ToString();
        }
        else
        {
            return ServiceDueStatus.Good.ToString();
        }

    }

    public string Source { get; set; } = string.Empty;
    public string? SourceId { get; set; } = null;

    public string Tracker { get; set; } = string.Empty;



}
