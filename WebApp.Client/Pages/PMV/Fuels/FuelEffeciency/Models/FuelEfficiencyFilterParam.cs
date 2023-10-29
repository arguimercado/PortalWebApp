namespace WebApp.Client.Pages.PMV.Fuels.FuelEffeciency.Models;

public class FuelEfficiencyFilterParam
{
    
    public FuelEfficiencyFilterParam()
    {

    }


    public FuelEfficiencyFilterParam(DateTime dateFrom, DateTime dateTo)
    {
        DateFrom = dateFrom;
        DateTo = dateTo;
    }

    public DateTime DateFrom { get; set; } = DateTime.Today;
    public DateTime DateTo { get; set; } = DateTime.Today;
    public bool IsPostBack { get; set; } = true;

    public string[] Assets => string.IsNullOrEmpty(StringAssets) ? Array.Empty<string>() : StringAssets.Split(",");
    public string StringAssets { get; set; } = "";
}
