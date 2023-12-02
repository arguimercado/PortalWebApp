using Module.PMV.Core.Assets.Models.Assets.Entities;

namespace Module.PMV.Core.Assets.Contracts.Assets;

public interface IServiceAlertDataService
{
    Task CreateServiceAlert(ServiceAlert due);
    Task DeleteAlert(string dueId);
    Task UpdateAlert(ServiceAlert due, bool kmOnly = false);
    Task<IEnumerable<ServiceAlert>> GetServiceAlerts(int assetId);
}
