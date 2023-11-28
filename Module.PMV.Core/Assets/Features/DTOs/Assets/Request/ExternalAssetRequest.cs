namespace Module.PMV.Core.Assets.Features.DTOs.Assets.Request;

public class ExternalAssetRequest
{
    public string? AssetCode { get; set; }
    public string AssetDesc { get; set; } = string.Empty;
    public string PlateType { get; set; } = string.Empty;
    public string PlateNum { get; set; } = string.Empty;
    public string VendorCode { get; set; } = string.Empty;
    public string CompanyCode { get; set; } = string.Empty;
    public string HireSub { get; set; } = string.Empty;
    public float FuelTankCapacity { get; set; } = 0;
}
