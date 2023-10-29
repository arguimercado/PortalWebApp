using WebApp.UILibrary.Commons;

namespace WebApp.Client.Pages.PMV.Fuels.Stations.Models;

public class FuelStationModel
{
    public string? Id { get; set; }
    public string? Code { get; set; }
    public string? Description { get; set; }
    public float OpeningMeter { get; set; }
    public float OpeningBalance { get; set; }
    public string StationType { get; set; } = "";
    public float TankCapacity { get; set; } = 0;
}

public class FuelStationContainerModel
{
    public FuelStationModel FuelStation { get; set; } = new();
    public IEnumerable<FuelStationModel> FuelStationList { get; set; } = new List<FuelStationModel>();
    public List<SelectItem> Stations { get; set; } = new();
    public List<SelectItem> StationTypes { get; set; } = new();
}
