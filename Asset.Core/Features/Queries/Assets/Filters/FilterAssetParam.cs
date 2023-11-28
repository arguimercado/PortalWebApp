namespace Asset.Core.Features.Queries.Assets.Filters;

public class FilterAssetParam {
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

    public IEnumerable<string> SelectedCategories => !string.IsNullOrEmpty(Category) ? Category.Split(',').AsEnumerable() :
                                                       Enumerable.Empty<string>();
}
