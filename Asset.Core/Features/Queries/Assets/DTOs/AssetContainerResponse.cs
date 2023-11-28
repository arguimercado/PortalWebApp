using WebApp.SharedServer.Commons;

namespace Asset.Core.Features.Queries.Assets.DTOs;


public class AssetContainerResponse
{
    public IEnumerable<InternalAssetsResponse> InternalAssets { get; set; } = new List<InternalAssetsResponse>();
    public IEnumerable<ExternalAssetsResponse> ExternalAssets { get; set; } = new List<ExternalAssetsResponse>();
    public List<SelectItem> Categories { get; set; } = new();
    public List<SelectItem> SubCategories { get; set; } = new();
    public List<SelectItem> Brands { get; set; } = new();
    public List<SelectItem> Companies { get; set; } = new();
    public List<SelectItem> Statuses { get; set; } = new();
    public List<SelectItem> PlateTypes { get; set; } = new();
    public List<SelectItem> HireSub { get; set; } = new();
    public List<SelectItem> Vendors { get; set; } = new();
    public List<SelectItem> GetSubCategoryByCat(List<string?>? catId) => SubCategories.Where(s => catId!.Contains(s.Type!)).ToList();
    public List<SelectItem> GetAll() => SubCategories.ToList();
}

public class InternalAssetsResponse
{
    public class DriversOperator
    {
        public string EmpCode { get; set; } = "";
        public string? Name { get; set; }
        public DateTime? AssignedAt { get; set; }
    }

    public int? Id { get; set; }
    public string AssetCode { get; set; } = "";
    public string AssetDesc { get; set; } = "";
    public string BrandCode { get; set; } = "";
    public string Model { get; set; } = "";
    public int Year { get; set; }
    public string Color { get; set; } = "";
    public string PlateNo { get; set; } = "";
    public string MakeModelYear => $"{BrandCode}/{Model}/{Year}";
    public DateTime? PurchaseDate { get; set; }
    public string VendorCode { get; set; } = "";
    public string? CompanyCode { get; set; }
    public string Category { get; set; } = "";
    public string SubCategory { get; set; } = "";
    public string? LPONo { get; set; }
    public string? DeliveryNote { get; set; }
    public int KmPerHr { get; set; }
    public string Status { get; set; } = "";
    public int TankCapacity { get; set; }
    public IEnumerable<DriversOperator> Drivers { get; set; } = new List<DriversOperator>();


}

public class ExternalAssetsResponse
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