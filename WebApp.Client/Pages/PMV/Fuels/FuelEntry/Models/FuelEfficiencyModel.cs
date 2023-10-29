
namespace WebApp.Client.Pages.PMV.Fuels.FuelEntry.Models;

public class FuelAssetSelection
{

    public int Id { get; set; }
    public string AssetCode { get; set; } = string.Empty;
    public string FullDesc { get; set; } = string.Empty;
    public string CompanyCode { get; set; } = string.Empty;

}

public class FuelEfficiencyContainerModel
{
    public IEnumerable<FuelLogEffeciencyModel> EffeciencyLists { get; set; } = new List<FuelLogEffeciencyModel>();
    public IEnumerable<FuelAssetSelection> AssetSelections { get; set; } = new List<FuelAssetSelection>();

    public bool IsPostBack { get; set; }
}
public class FuelLogEffeciencyModel
{
    public string DocumentNo { get; set; } = string.Empty;
    public string AssetCode { get; set; } = "";
    public string PlateNo { get; set; } = "";
    public string Location { get; set; } = "";
    public string OnHireTo { get; set; } = "";
    public string Station { get; set; } = "";
    public DateTime FuelDateTime { get; set; }

    public float Quantity { get; set; }
    public int Reading { get; set; } = 0;
    public int PreviousReading { get; set; } = 0;
    public float Diff { get; set; }
    public string LH { get; set; } = string.Empty;
    public string HL { get; set; } = string.Empty;
    public string Fueler { get; set; } = string.Empty;
    public string OperatorDriver { get; set; } = string.Empty;
}
