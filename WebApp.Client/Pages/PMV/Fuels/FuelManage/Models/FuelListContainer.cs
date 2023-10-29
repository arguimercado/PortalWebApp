namespace WebApp.Client.Pages.PMV.Fuels.FuelManage.Models;

public class FuelListContainer
{
    public IEnumerable<FuelStationSelectionModel> StationSelections { get; set; } = new List<FuelStationSelectionModel>();
    public IEnumerable<FuelListModel> Masters { get; set; } = new List<FuelListModel>();
    public bool IsPostBack { get; set; }

}


public class FuelListModel
{
    public string Id { get; set; } = string.Empty;
    public string Station { get; set; } = "";
    public string StationType { get; set; } = "";
    public int ReferenceNo { get; set; }
    public int RefLink => ReferenceNo * 12345;
    public int DocumentNo { get; set; }
    public DateTime FueledDate { get; set; }
    public float OpeningMeter { get; set; }
    public float ClosingMeter { get; set; }
    public float TotalDispense { get; set; }
    public float TotalRefill { get; set; }
    public float TotalAdjustment { get; set; }
    public float TotalDistribute { get; set; }
    public float InclusiveClosing => ClosingMeter;
    public float OpeningBalance { get; set; }
    public float RemainingBalance { get; set; }
    public string Location { get; set; } = "";
    public string? Remarks { get; set; } = null;
    public string Fueler { get; set; } = null!;
    public bool IsPosted { get; set; }
    public DateTime? PostedAt { get; set; }
    public string Status { get; set; } = "";
    public string CreatedBy { get; set; } = "";
    public DateTime? CreatedAt { get; set; } = null;

    public bool IsSelected { get; set; } = false;
    public string Track { get; set; }
}
