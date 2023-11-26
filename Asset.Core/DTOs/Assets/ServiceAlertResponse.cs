namespace Asset.Core.DTOs.Assets;

public class ServiceAlertResponse
{
    public string Id { get; set; } = string.Empty;
    public string GroupId { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public DateTime? LastServiceDate { get; set; }
    public int LastSMUReading { get; set; }
    public int CurrentSMUReading { get; set; }
    public int KmAlert { get; set; }
    public int AlertDue { get; set; }
    public int KmInterval { get; set; }
    public string SMU { get; set; } = string.Empty;
    public int IntervalDue { get; set; }
    public string Source { get; set; } = string.Empty;
    public bool MarkDeleted { get; set; }
    public string Status { get; set; } = string.Empty;
    public string? Remarks { get; set; } = string.Empty;
}
