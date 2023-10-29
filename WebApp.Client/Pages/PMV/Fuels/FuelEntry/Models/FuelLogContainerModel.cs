using WebApp.UILibrary.Commons;

namespace WebApp.Client.Pages.PMV.Fuels.FuelEntry.Models;


public class FuelLogContainerModel
{
    public FuelLogModel FuelLog { get; set; } = new();
    public IEnumerable<SelectItem> LocationSelections { get; set; } = new List<SelectItem>();
    public IEnumerable<FuelStationModel> StationSelections { get; set; } = new List<FuelStationModel>();
    public IEnumerable<SelectItem> AssetCodeSelections { get; set; } = new List<SelectItem>();
    public IEnumerable<FuelerSelection> FuelerSelections { get; set; } = new List<FuelerSelection>();

    public IEnumerable<SelectItem> LogTypeSelections { get; set; } = new List<SelectItem>();
}
