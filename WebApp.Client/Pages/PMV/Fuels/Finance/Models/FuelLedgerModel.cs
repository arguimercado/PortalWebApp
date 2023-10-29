namespace WebApp.Client.Pages.PMV.Fuels.Finance.Models;

public class TankerStockContainer
{
    public IEnumerable<TankerStockModel> FuelEntries { get; set; } = new List<TankerStockModel>();
    public int Month { get; set; }
    public int Year { get; set; }
    public int TankerCount { get; set; }
    public float TotalDispense { get; set; }

    public float TotalDistribute { get; set; }
    public float TotalFlowVariance { get; set; }
}

public class TankerStockModel
{

    public string Id { get; set; }

    public int Month { get; set; }
    public string MonthName { get; set; } = "";
    public int Year { get; set; }
    public string TankerCode { get; set; } = "";
    public float OpeningBalance { get; set; }
    public float TotalRefill { get; set; }
    public float TotalDistribute { get; set; }
    public float TotalDispense { get; set; }
    public float TotalAdjustment { get; set; }
    public float TotalFlowVariance { get; set; }
    public int TotalPosted { get; set; }
    public int TotalUnposted { get; set; }
    public bool GLPosted { get; set; }
}
