namespace WebApp.Client.Pages.PMV.Fuels.Models;


public enum EnumLogType
{
    Refill,
    Distribute,
    Dispense,
    Adjustment,
    Void
}

public enum EnumFuelLogStatus
{
    Pending,
    Posted,
    Approved,
    Submitted,
    Locked
}
