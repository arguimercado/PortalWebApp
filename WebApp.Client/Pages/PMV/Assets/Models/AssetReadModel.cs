namespace WebApp.Client.Pages.PMV.Assets.Models;

public sealed class AssetReadModel
{
    public int SlNo { get; set; }
    public string AssetCode { get; set; } = string.Empty;
    public string AssetDesc { get; set; } = string.Empty;
    public string CompanyCode { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public string SubCatName { get; set; } = string.Empty;
    public string Color { get; set; } = string.Empty;
    public string ChasisNo { get; set; } = string.Empty;
    public int KmHr { get; set; }
    public int TankCapacity { get; set; }
    public int CurrentSMUReading { get; set; }
    public string VendorCode { get; set; } = string.Empty;
    public string LpoNo { get; set; } = string.Empty;
    public string DeliveryNote { get; set; } = string.Empty;
    public string RateType { get; set; } = string.Empty;
    public string AccountCategory { get; set; } = string.Empty;
    public int ConditionRank { get; set; }
    public DateTime? FirstRegDate { get; set; }
    public string EngineNo { get; set; } = string.Empty;
    public string PlateNo { get; set; } = string.Empty;
    public float NetValue { get; set; }
    public string RentOwned { get; set; } = string.Empty;
    public int ModifiedAsset { get; set; }
    public string ModifiedAssetString => ModifiedAsset == 1 ? "True" : "False";
    public int Rate { get; set; }
    public string YearMakeModel { get; set; } = string.Empty;
    public string HireMethod { get; set; } = string.Empty;
    public DateTime? RegistrationExpiry { get; set; }
    public float RateOfDepreciation { get; set; }
    public string Owner { get; set; } = string.Empty;
    public string OperatedBy { get; set; } = string.Empty;
    public DateTime? Warranty { get; set; }
    public DateTime? RegistrationExpiryDate { get; set; }
    public DateTime? Insurance { get; set; }
    public string Dimension { get; set; } = string.Empty;
    public string StatusDescription { get; set; } = string.Empty;
    public bool Completed { get; set; }
    public string EmpName { get; set; } = string.Empty;
    public DateTime? CreatedAt { get; set; }
    public string CreatedBy { get; set; } = "";
    public IList<AssetFuelEfficiencyModel> EfficiencyList { get; set; } = new List<AssetFuelEfficiencyModel>();
    public IEnumerable<AssetServiceOrder> ServiceOrders { get; set; } = new List<AssetServiceOrder>();
    public IEnumerable<ServiceDueReadModel> ServiceDues { get; set; } = new List<ServiceDueReadModel>();
    public IEnumerable<AssignedDriver> Drivers { get; set; } = new List<AssignedDriver>();
    public IEnumerable<AssetDocument> Documents { get; set; } = new List<AssetDocument>();

}

public class AssetServiceOrder
{
    public string AssetCode { get; set; } = string.Empty;
    public string DocumentNo { get; set; } = string.Empty;
    public DateTime OrderDate { get; set; } = DateTime.UtcNow;
    public string? Remarks { get; set; } = string.Empty;
    public string? InspectedBy { get; set; }
    public DateTime? InspectedDate { get; set; }
    public DateTime? ScheduleDate { get; set; }
    public DateTime? ExpectedCompletion { get; set; }
    public string? CorrectiveAction { get; set; } = string.Empty;
    public string Stage { get; set; } = string.Empty;
    public DateTime? ClosedDate { get; set; }

    public string Status => ClosedDate.HasValue ? "Closed" : "Open";
}

public class AssetFuelEfficiencyModel
{
    public string DocumentNo { get; set; } = string.Empty;
    public string AssetCode { get; set; } = "";
    public string Location { get; set; } = "";
    public string OnHireTo { get; set; } = "";
    public string Station { get; set; } = "";
    public DateTime FuelDateTime { get; set; }
    public float Quantity { get; set; }
    public int Reading { get; set; } = 0;
    public int PreviousReading { get; set; } = 0;
    public float Diff { get; set; }
    public string LH { get; set; } = string.Empty;
    public string HL { get; set; } = string.Empty;
    public string Fueler { get; set; } = string.Empty;
}

public class ServiceDueReadModel
{
    public string GroupId { get; set; } = string.Empty;
    public string Id { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public DateTime? LastServiceDate { get; set; }
    public int LastSMUReading { get; set; }
    public int CurrentSMUReading { get; set; }
    public int KmAlert { get; set; }
    public int AlertDue { get; set; }
    public int KmInterval { get; set; }
    public int IntervalDue { get; set; }

    public int GetBalanceDue(int kmHr) => AlertDue - kmHr;

    public string SMU { get; set; } = string.Empty;

    public string Status { get; set; } = string.Empty;


}

public class AssetSMUResponse
{
    public int CurrentSMU { get; set; }
    public int DocumentNo { get; set; }
    public DateTime Date { get; set; }
}
