namespace WebApp.Client.Pages.PMV.Fuels.FuelManage.Models;



public class FuelSearchFilterParam
{

    private readonly IEnumerable<string> _fuelStations;
    public FuelSearchFilterParam()
    {

    }

    public FuelSearchFilterParam(DateTime dateFrom, DateTime dateTo)
    {
        DateFrom = dateFrom;
        DateTo = dateTo;
    }

    public DateTime DateFrom { get; set; } = DateTime.Today;
    public DateTime DateTo { get; set; } = DateTime.Today;
    public bool IsPostBack { get; set; } = true;

    public string[] FuelStations => string.IsNullOrEmpty(StringFuelStation) ? Array.Empty<string>() : StringFuelStation.Split(",");
    public string StringFuelStation { get; set; }
}
