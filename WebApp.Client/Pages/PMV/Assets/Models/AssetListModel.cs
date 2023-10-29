

using WebApp.UILibrary.Commons;

namespace WebApp.Client.Pages.PMV.Assets.Models;

public class AssetListContainerModel
{
    public IEnumerable<AssetListModel> AssetData { get; set; } = new List<AssetListModel>();
    public IEnumerable<ExternalListModel> ExternalData { get; set; } = new List<ExternalListModel>();
    public List<SelectItem> Categories { get; set; } = new();
    public List<SelectItem> SubCategories { get; set; } = new();
    public List<SelectItem> Brands { get; set; } = new();
    public List<SelectItem> Companies { get; set; } = new();
    public List<SelectItem> Statuses { get; set; } = new();
    public List<SelectItem> PlateTypes { get; set; } = new();
    public List<SelectItem> HireSub { get; set; } = new();
    public List<SelectItem> Vendors { get; set; } = new();

    public List<SelectItem> GetSubCategoryByCat(List<string> catId) => SubCategories.Where(s => catId.Contains(s.Type)).ToList();
    public List<SelectItem> GetAll() => SubCategories.ToList();
}

public class AssetListModel
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
    public string MakeModelYear => $"{BrandCode}/{Model}/{Year}";
    public string Color { get; set; } = "";
    public string PlateNo { get; set; } = "";

    public DateTime? PurchaseDate { get; set; }
    public string? LPONo { get; set; }
    public string? DeliveryNote { get; set; }
    public string VendorCode { get; set; } = "";

    public string? CompanyCode { get; set; }
    public string Category { get; set; } = "";
    public string SubCategory { get; set; } = "";

    public int KmPerHr { get; set; }

    public string Status { get; set; } = "";

    public int TankCapacity { get; set; }

    public IEnumerable<DriversOperator> Drivers { get; set; } = new List<DriversOperator>();
}


public class ExternalListModel
{
    public int? Id { get; set; }
    public string AssetCode { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string PlateType { get; set; } = string.Empty;
    public string PlateNum { get; set; } = string.Empty;
    public string HireUnder { get; set; } = string.Empty;
    public string Vendor { get; set; } = string.Empty;
    public string HireOrSubContract { get; set; } = string.Empty;
    public DateTime? CreatedAt { get; set; }

    public float FuelTankCapacity { get; set; }
}





