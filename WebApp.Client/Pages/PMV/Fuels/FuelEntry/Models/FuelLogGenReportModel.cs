using WebApp.UILibrary.Commons;

namespace WebApp.Client.Pages.PMV.Fuels.FuelEntry.Models;

public static class FuelLogGenReportModel
{
    public class Parameter
    {

        public string Station { get; set; }

        public string[] StationCollection => string.IsNullOrEmpty(Station) ? Array.Empty<string>() : Station.Split(",");

        public string ReportType { get; set; } = "";
        public DateTime DateFrom { get; set; } = DateTime.Now;
        public DateTime DateTo { get; set; } = DateTime.Now;
        public bool IsPostBack { get; set; } = false;
    }

    public class Container
    {
        public IEnumerable<DailyConsumption> DailyConsumptionReport { get; set; } = new List<DailyConsumption>();
        public IEnumerable<SelectItem> ReportTypes { get; set; } = new List<SelectItem>();

        public IEnumerable<SelectItem> TankStations { get; set; } = new List<SelectItem>();
    }

    public class DailyConsumption
    {
        public string AssetCode { get; set; } = "";
        public string Location { get; set; } = "";
        public string Station { get; set; } = "";
        public DateTime FuelDateTime { get; set; }
        public int SMU { get; set; }
        public float Quantity { get; set; }
        public string DCSReferenceNo { get; set; } = "";
        public string DocumentNo { get; set; } = "";
    }
}
