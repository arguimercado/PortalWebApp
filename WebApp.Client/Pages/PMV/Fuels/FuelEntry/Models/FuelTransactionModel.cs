using WebApp.Client.Pages.PMV.Fuels.FuelTracking.Models;
using WebApp.Client.Pages.PMV.Fuels.Models;

namespace WebApp.Client.Pages.PMV.Fuels.FuelEntry.Models;

public class FuelTransactionModel
{
    public static FuelTransactionModel Clone(FuelTransactionModel model)
    {
        return (FuelTransactionModel)model.MemberwiseClone();
    }
    public string Id { get; set; } = "";
    public string FuelLogId { get; set; } = "";
    public string AssetCode { get; set; } = "";
    public DateTime FuelDateTime { get; set; } = DateTime.Now;
    public string? OperatorDriver { get; set; } = "";
    public string LogType { get; set; } = EnumLogType.Dispense.ToString();
    public string LogTypeAcro
    {
        get
        {
            if (LogType == EnumLogType.Dispense.ToString())
            {
                return "D";
            }
            else if (LogType == EnumLogType.Distribute.ToString())
            {
                return "T";
            }
            else if (LogType == EnumLogType.Void.ToString())
            {
                return "V";
            }
            else if (LogType == EnumLogType.Adjustment.ToString())
            {
                return "A";
            }
            else if (LogType == EnumLogType.Refill.ToString())
            {
                return "R";
            }
            else
            {
                return "";
            }

        }
    }
    public string Remarks { get; set; } = "";
    public int Reading { get; set; } = 0;
    public float Quantity { get; set; } = 0f;
    public float DispenseQuantity => LogType == EnumLogType.Dispense.ToString() ? Quantity : 0;
    public float RefillQuantity => LogType == EnumLogType.Refill.ToString() ? Quantity : 0;
    public float DistributeQuantity => LogType == EnumLogType.Distribute.ToString() ? Quantity : 0;

    public float ConsumedQuantity => DispenseQuantity + DistributeQuantity;
    public float AdjustmentQuantity => LogType == EnumLogType.Adjustment.ToString() ? Quantity : 0;
    public float RealQuantity => LogType.ToLower() == "dispense" || LogType.ToLower() == "distribute" ? Quantity * -1 : Quantity;
    public string? DriverQatarIdUrl { get; set; }
    public string ProjectCode { get; set; } = "";
    public bool MarkDelete { get; set; }
    public bool MarkVoid { get; set; }
    public DateTime? CreatedAt { get; set; } = DateTime.Now;
    public string? CreatedBy { get; set; } = "";
    public string? Photo { get; set; }
    public string[]? Photos => !string.IsNullOrEmpty(Photo) ? Photo.Split(";") : Array.Empty<string>();
    public string? VerifierId { get; set; } = "";
    public bool IsMatch { get; set; } = false;
}
