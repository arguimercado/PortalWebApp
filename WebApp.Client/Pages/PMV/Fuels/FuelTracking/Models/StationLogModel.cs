using WebApp.UILibrary.Commons;

namespace WebApp.Client.Pages.PMV.Fuels.FuelTracking.Models;

public class StationLogModel
{
    public string StationCode { get; set; } = "";
    public float CurrentMeterReading { get; set; } = 0f;
    public float TankCapacity { get; set; }
    public float TotalDispense { get; set; }
    public float TotalRefill { get; set; }
    public IEnumerable<FuelLogModel> Logs { get; set; } = new List<FuelLogModel>();

    public IEnumerable<SelectItem> AssetCodes { get; set; } = Enumerable.Empty<SelectItem>();
    public IEnumerable<SelectItem> LogTypes { get; set; } = Enumerable.Empty<SelectItem>();
    public IEnumerable<SelectItem> ProjectCodes { get; set; } = Enumerable.Empty<SelectItem>();

}
