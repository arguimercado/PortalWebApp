namespace Module.PMV.Core.Assets.Features.DTOs.Assets.Response;


public class ExternalAssetResponse
{
    public string AssetCode { get; set; } = "";
    public string Description { get; set; } = "";
    public string PlateType { get; set; } = "";
    public string PlateNum { get; set; } = "";
    public string HireUnder { get; set; } = "";
    public string Vendor { get; set; } = "";
    public string HireOrSubContract { get; set; } = "";
    public float FuelTankCapacity { get; set; }
    public DateTime? CreatedAt { get; set; }
}