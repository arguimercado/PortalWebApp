using Asset.Core.Models.Assets.Entities;

namespace Asset.Core.Contracts.Assets;

public interface IServiceAlertDataService
{
    Task CreateServiceAlert(ServiceAlert due);
    Task DeleteAlert(string dueId);
    Task UpdateAlert(ServiceAlert due, bool kmOnly = false);

    Task<IEnumerable<ServiceAlert>> GetServiceAlert(int assetId);
}
