
using BaseEntityPack.Core;
using System.ComponentModel.DataAnnotations.Schema;
using WebApp.SharedServer.Attributes;

namespace Core.PMV.Assets.Entities;

[Auditable]
public class ServiceLog : AggregateRoot<Guid>
{

    public ServiceLog() : base(Guid.NewGuid())
    {

    }
    public ServiceLog(Guid Id) : base(Id)
    {
    }

    public int AssetId { get; set; }
    public string ServiceCode { get; set; } = null!;
    public string? TransactionId { get; set; }
    public string? Source { get; set; }
    public DateTime? LastServiceDate { get; set; }
    public int LastReading { get; set; }
    public int KmAlert { get; set; }
    public int KmInterval { get; set; }

    [NotMapped]
    public virtual int AlertAtKm => LastReading + KmAlert;

    [NotMapped]
    public virtual int IntervalAtKm => LastReading + KmInterval;

    [NotMapped]
    public string ServiceStatus
    {
        get
        {
            if (LastReading == AlertAtKm)
            {
                return "Warning";
            }
            else if (LastReading > AlertAtKm && LastReading <= IntervalAtKm)
            {
                return "Danger";
            }
            else if (LastReading > IntervalAtKm)
            {
                return "Critical";
            }
            else
            {
                return "Good";
            }
        }
    }
}