using WebApp.UILibrary.Commons;

namespace WebApp.Client.Pages.PMV.Assets.Models;

public class AssetDashboardContainer
{

    public IEnumerable<CategoryCountModel> CategoryCounts { get; set; } = new List<CategoryCountModel>();
    public IEnumerable<CategoryCountModel> ExternalCounts { get; set; } = new List<CategoryCountModel>();
}

public class CategoryCountModel
{
    public string Category { get; set; } = "";
    public string Code { get; set; } = "";
    public int Status { get; set; }
    public int Active { get; set; }
    public int Inactive { get; set; }
    public int Sold { get; set; }
    public int TradedIn { get; set; }
    public int Scrapped { get; set; }

    public int Summary { get; set; }

    public int Retired { get; set; }
}

public class CostReportModel
{
    public IEnumerable<AssetSIVTransaction> Transactions { get; set; } = new List<AssetSIVTransaction>();
    public IList<AssetSIVSummary> Summary { get; set; } = new List<AssetSIVSummary>();
    public List<SelectItem> Assets { get; set; } = new();
    public List<SelectItem> Categories { get; set; } = new();
    public List<SelectItem> SubCategories { get; set; } = new();

    public List<SelectItem> FilterSubCategory(string? category) =>
         !string.IsNullOrEmpty(category) ? SubCategories.Where(s => s.Type == category).ToList() : SubCategories.ToList();

    public List<SelectItem> FilterAsset(string? category) =>
         !string.IsNullOrEmpty(category) ? Assets.Where(s => s.Type == category).ToList() : Assets.ToList();

}

public class AssetSIVTransaction
{
    public string AssetCode { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string ItemCode { get; set; } = string.Empty;
    public string ItemDescription { get; set; } = string.Empty;
    public string DocType { get; set; } = string.Empty;
    public string SIVNo { get; set; } = string.Empty;
    public DateTime? SIVDate { get; set; }
    public decimal Rate { get; set; }
    public int Qty { get; set; }
    public decimal Amount { get; set; }
}


public class AssetSIVSummary
{
    public string AssetCode { get; set; } = string.Empty;
    public IList<AssetSIVTransaction> Transactions { get; set; } = new List<AssetSIVTransaction>();
    public decimal TotalAmount => Transactions.Sum(t => t.Amount);
}

public class MCRequest
{

    public DateTime? DateFrom { get; set; } = null;
    public DateTime? DateTo { get; set; } = null;
    public string[]? AssetCodes { get; set; }
    public string? CategoryId { get; set; }
    public string? SubCategory { get; set; }
    public bool IsPostback { get; set; } = false;

}
