namespace Asset.Core.Models.Assets.Entities;

public class OperatorDriver
{

    public int Id { get; set; } = 0;
    public string AssetCode { get; set; } = "";
    public string? AssetTypeCode { get; set; }
    public string? Division { get; set; }
    public string? BrandCode { get; set; }
    public string EmpCode { get; set; } = string.Empty;
    public string EmpType { get; set; } = string.Empty;
    public string EmpName { get; set; } = string.Empty;
    public string? RPNo { get; set; }
    public string? Name { get; set; }
    public string? Company { get; set; }
    public string? MobileNo { get; set; }
    public string? Department { get; set; }
    public string? AssetLocation { get; set; } = string.Empty;
    public string? VendorCode { get; set; }
    public int InternalExternal { get; set; }
    public DateTime? AssignedAt { get; set; }
    public DateTime? ReturnedAt { get; set; }
    public int? DcsSlNo { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? CreatedAt { get; set; }
}
