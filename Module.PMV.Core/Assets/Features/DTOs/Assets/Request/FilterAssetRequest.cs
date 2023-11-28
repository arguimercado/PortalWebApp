namespace Module.PMV.Core.Assets.Features.DTOs.Assets.Request;

public class FilterAssetRequest
{
    public string AssetType { get; set; } = "";
    public string? AssetCode { get; set; }
    public string? Category { get; set; }
    public string? SubCategory { get; set; }
    public string? Brand { get; set; }
    public string? CompanyCode { get; set; }
    public string? VendorCode { get; set; }
    public string? Status { get; set; } = null;
    public string? PlateType { get; set; }
    public string? PlateNum { get; set; }
    public string? HireOrSubContract { get; set; }
    public bool IsPostBack { get; set; } = false;
    public bool IsRefresh { get; set; } = false;
    public string? Fields { get; set; }
}
