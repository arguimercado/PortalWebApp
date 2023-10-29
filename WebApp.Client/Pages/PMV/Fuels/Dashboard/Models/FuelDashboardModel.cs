namespace WebApp.Client.Pages.PMV.Fuels.Dashboard.Models;


public class FuelDashboardDataModel {
    public IEnumerable<StationSummaryModel> TankSummary { get; set; } = new List<StationSummaryModel>();
}

public record StationSummaryModel
{
    public string Station { get; set; } = string.Empty;
    public string DocumentNo { get; set; } = string.Empty;
    public DateTime? CurrentDate { get; set; }
    public string Track { get; set; } = string.Empty;
    public float OpeningMeter { get; set; }
    public float ClosingMeter { get; set; }
    public float CurrentMeter => Track == "p" ? ClosingMeter : 0;
    public int BegBalance { get; set; }
    public float TotalRefill { get; set; }
    public float TotalDispense { get; set; }
    public float TotalAdjustment { get; set; }
    public float TotalDistribute { get; set; }
    public int TotalDraft { get; set; }
    public int TotalSubmitted { get; set; }
}
