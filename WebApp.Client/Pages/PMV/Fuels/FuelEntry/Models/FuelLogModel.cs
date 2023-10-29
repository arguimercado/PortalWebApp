using WebApp.Client.Pages.PMV.Fuels.Models;

namespace WebApp.Client.Pages.PMV.Fuels.FuelEntry.Models;

public class FuelLogModel
{

    public string? Id { get; set; } = null;
    public int ReferenceNo { get; set; }
    public int DocumentNo { get; set; }
    public string Station { get; set; } = "";
    public DateTime FueledDate { get; set; } = DateTime.Now;
    public string? ShiftStartTime { get; set; }
    public DateTime? GetShiftStartTimeValue() => !string.IsNullOrEmpty(ShiftStartTime) ? DateTime.Parse($"{ShiftStartTime}") : null;

    public void SetShiftStartTimeValue(DateTime? value)
    {
        if (value.HasValue) ShiftStartTime = value.Value.ToString("yyyy-MM-dd hh:mm tt");
    }

    public string? ShiftEndTime { get; set; }

    public DateTime? GetShiftEndTimeValue()
    {
        return !string.IsNullOrEmpty(ShiftEndTime) ? DateTime.Parse($"{ShiftEndTime}") : null;
    }

    public void SetShiftEndTimeValue(DateTime? value)
    {
        if (value.HasValue) ShiftEndTime = value.Value.ToString("yyyy-MM-dd hh:mm tt");
    }

    public int StartShiftTankerKm { get; set; }
    public int? EndShiftTankerKm { get; set; }
    public float OpeningMeter { get; set; }
    public float OpeningVariance { get; set; }
    public string? OpeningMeterPhoto { get; set; } = "";
    public float ClosingMeter { get; set; }
    public string? ClosingMeterPhoto { get; set; } = "";
    public float OpeningBalance { get; set; }
    public float ClosingBalance { get; set; }
    public float PreviousClosingMeter { get; set; }
    public float RemainingBalance => OpeningBalance + TotalRefill - TotalDispDist + TotalAdjustment;
    public float ClosingVariance { get; set; }
    public string? Remarks { get; set; } = null;
    public string Fueler { get; set; } = null!;
    public bool IsPosted { get; set; } = false;
    public bool IsSubmitted { get; set; } = false;
    public string EmployeeCode { get; set; } = "";
    public string LogType { get; set; } = "";

    public string Status { get; set; } = "";
    public string? ApprovedBy { get; set; }
    public DateTime? ApprovedDate { get; set; } = null;
    public float TotalDispense { get; set; }
    public float TotalDistribute { get; set; }
    public float TotalRefill { get; set; }
    public float TotalAdjustment { get; set; }
    public float TotalDispDist => TotalDispense + TotalDistribute;

    public IList<FuelTransactionModel> FuelTransactions { get; set; } = new List<FuelTransactionModel>();



    public void AddDetail(FuelTransactionModel item)
    {

        var record = FuelTransactions.FirstOrDefault(d => d.Id == item.Id);
        if (record == null)
        {
            item.Id = Guid.NewGuid().ToString();
            item.FuelLogId = Id ?? "";
            item.CreatedAt = DateTime.Now;
            FuelTransactions.Add(item);
        }
        else
        {
            item.Id = record.Id;
            record = item;
        }
    }

    public void RemoveDetail(FuelTransactionModel item)
    {

        if (FuelTransactions.Any(d => d.Id == item.Id))
        {
            FuelTransactions.Remove(item);
        }
    }

    public bool IsVoidExcluded { get; set; } = false;

    public IEnumerable<FuelTransactionModel> TransactionList => IsVoidExcluded ? FuelTransactions.Where(f => f.LogType != EnumLogType.Void.ToString()).ToList() : FuelTransactions.ToList();

}
