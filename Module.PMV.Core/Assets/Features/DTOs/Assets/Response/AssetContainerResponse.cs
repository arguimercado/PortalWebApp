using WebApp.SharedServer.Commons;

namespace Module.PMV.Core.Assets.Features.DTOs.Assets.Response;


public class AssetContainerResponse
{
    public class DriversOperator
    {
        public string EmpCode { get; set; } = string.Empty;
        public string EmpName { get; set; } = string.Empty;
        public string EmpPasswd { get; set; } = string.Empty;
        public string VisaDesignation { get; set; } = string.Empty;
        public string Designation { get; set; } = string.Empty;
        public string LocationOfWork { get; set; } = string.Empty;
        public string RPNo { get; set; }
        public string MobilePhone { get; set; } = string.Empty;
        public string? PresentEmail { get; set; }
    }
    public IEnumerable<InternalAssetResponse> InternalAssets { get; set; } = new List<InternalAssetResponse>();
    public InternalAssetResponse InternalAsset { get; set; } = new();
    public IEnumerable<ExternalAssetResponse> ExternalAssets { get; set; } = new List<ExternalAssetResponse>();

    public ExternalAssetResponse ExternalAsset { get; set; } = new();

    public List<SelectItem> Categories { get; set; } = new();
    public List<SelectItem> SubCategories { get; set; } = new();
    public List<SelectItem> Brands { get; set; } = new();
    public List<SelectItem> Companies { get; set; } = new();
    public List<SelectItem> Statuses { get; set; } = new();
    public List<SelectItem> PlateTypes { get; set; } = new();
    public List<SelectItem> HireSub { get; set; } = new();
    public List<SelectItem> Vendors { get; set; } = new();
    public IEnumerable<SelectItem> Accounts { get; set; } = new List<SelectItem>();
    public IEnumerable<SelectItem> RentOwnes { get; set; } = new List<SelectItem>();
    public IEnumerable<SelectItem> HireMethods { get; set; } = new List<SelectItem>();
    public IEnumerable<SelectItem> AssetTypes { get; set; } = new List<SelectItem>();
    public IEnumerable<SelectItem> ServiceGroups { get; set; } = new List<SelectItem>();
    public IEnumerable<DriversOperator> Drivers { get; set; } = new List<DriversOperator>();
    public List<SelectItem> GetSubCategoryByCat(List<string?>? catId) => SubCategories.Where(s => catId!.Contains(s.Type!)).ToList();
    public List<SelectItem> GetAll() => SubCategories.ToList();
}

