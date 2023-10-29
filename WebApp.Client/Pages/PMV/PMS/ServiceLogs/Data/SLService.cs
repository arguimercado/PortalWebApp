
using WebApp.Client.Pages.PMV.PMS.ServiceLogs.Models;

namespace WebApp.Client.Pages.PMV.PMS.ServiceLogs.Data;

public interface ISLService
{
    Task<AssetContainerModel> GetAssetDueList(string? category, string? subcategory, string? assetCode, string? status, bool isPostback = false);

    Task UpdateAssetKm(int assetId,int smu);

    Task UpdateServiceDue(int assetId, ServiceDueEntry serviceEntry);
}
