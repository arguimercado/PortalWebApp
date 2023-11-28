using Module.PMV.Core.Assets.Features.DTOs.Assets.Response;
using WebApp.Client.Pages.PMV.Assets.Models;

namespace WebApp.Client.Pages.PMV.Assets.Data;

public interface IAssetService
{
    Task<AssetContainerModel> CreateNew(string assetType, bool isPostBack = false);
    Task<AssetContainerResponse> GetAssets(FilterAssetModel filterAssetParam);
    Task<AssetContainerModel?> GetAsset(string searchValue, string searchType, string assetType, bool IsPostBack);
    Task<string> GetAssetNo(string subCategory);
    Task<AssetReadModel?> ViewAsset(int id);
    Task SaveInternal(InternalAssetModel model, string userId);
    Task SaveExternal(ExternalAssetModel model,string userId);

    Task<AssetDashboardContainer> GetCategoryCount();
    Task<CostReportModel> GetMaintenanceCost(MCRequest request);
    Task UpdateAssignedDriver(OperatorDriverModel assignedDriver, string assetType);
    Task DeleteAssignedDriver(int assignedId);
    Task<IEnumerable<OperatorDriverModel>> GetAllAssignedDrivers(string assetCode);

    Task<AssignedAssetModel> GetAssetDetailExpress(string assetCode);

    #region document
    Task<AssetDocumentModel?> UploadDocument(AssetDocumentModel document);
    Task DeleteDocument(string documentId);
    #endregion

    #region service
    Task UpdateServiceDue(int assetId, ServiceDueEntryModel serviceEntry);
    Task DeleteServiceDue(string serviceId);
    Task<IEnumerable<ServiceDueEntryModel>> CreateServiceDue(int assetId, string groupAlertId);
    #endregion

}
