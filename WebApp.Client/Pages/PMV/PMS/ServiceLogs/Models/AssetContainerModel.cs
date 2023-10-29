
using WebApp.UILibrary.Commons;

namespace WebApp.Client.Pages.PMV.PMS.ServiceLogs.Models;

public class AssetContainerModel
{
    public IEnumerable<AssetModel> Assets { get; set; } = new List<AssetModel>();

    public IEnumerable<SelectItem> DueTypes { get; set; } = new List<SelectItem>();
    public IEnumerable<SelectItem> Categories { get; set; } = new List<SelectItem>();
    public IEnumerable<SelectItem> SubCategories { get; set; } = new List<SelectItem>();

    public string GetCategoryName(string categoryId)
    {
        var category = Categories.FirstOrDefault(x => x.Value == categoryId);
        return category?.Text ?? string.Empty;
    }

    public IEnumerable<SelectItem> GetSubCategoriesByCategory(string categoryId)
    {
        return SubCategories.Where(s => s.Type == categoryId);
    }

    public string GetSubCategoryName(string subCategory)
    {
        var category = SubCategories.FirstOrDefault(x => x.Value == subCategory);
        return category?.Text ?? string.Empty;
    }
}

public class AssetModel
{
    public int SlNo { get; set; }
    public string AssetCode { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public string SubCatCode { get; set; } = string.Empty;
    public string BrandName { get; set; } = string.Empty;
    public int KmPerHr { get; set; }
    public string? CompanyCode { get; set; }
    public string Model { get; set; } = string.Empty;
    public int Year { get; set; }
    public string? PlateNo { get; set; }
    public string? EngineNo { get; set; }
    public string? ChasisNo { get; set; }

    public IEnumerable<ServiceDueModel> Items { get; set; } = new List<ServiceDueModel>();

    public IEnumerable<ServiceDueModel> DueItems => Items.Where(x => x.Status.ToLower() != "good").ToList();
}

public class AssetSMUEntry
{

}