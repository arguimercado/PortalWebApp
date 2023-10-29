namespace WebApp.Client.Pages.PMV.Fuels.FuelEntry.Models;

public class FuelStationModel
{
    public string? Id { get; set; }
    public string? Code { get; set; }
    public string? Description { get; set; }
    public float OpeningMeter { get; set; }
    public float OpeningBalance { get; set; }
    public string StationType { get; set; } = "";
}
