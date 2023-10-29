using WebApp.Client.Pages.PMV.Fuels.Models;
using WebApp.UILibrary.Commons;


namespace WebApp.Client.Pages.PMV.Fuels.FuelTracking.Models;


public class StationLogContainerModel
{
    public IEnumerable<StationLogModel> Stations { get; set; } = Enumerable.Empty<StationLogModel>();
    public IEnumerable<SelectItem> AssetCodes { get; set; } = Enumerable.Empty<SelectItem>();
    public IEnumerable<SelectItem> LogTypes { get; set; } = Enumerable.Empty<SelectItem>();
    public IEnumerable<SelectItem> ProjectCodes { get; set; } = Enumerable.Empty<SelectItem>();

    public IEnumerable<SelectItem> GetAssetList(string logType)
    {
        if (logType == EnumLogType.Distribute.ToString() || logType == EnumLogType.Refill.ToString())
        {
            return AssetCodes.Where(a => a.Type == "tanker");
        }

        return AssetCodes;
    }
}
