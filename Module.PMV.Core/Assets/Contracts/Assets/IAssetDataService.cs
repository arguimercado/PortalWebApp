using Module.PMV.Core.Assets.Features.DTOs.Assets.Response;
using Module.PMV.Core.Assets.Models.Assets;


namespace Module.PMV.Core.Assets.Contracts.Assets;

public interface IAssetDataService
{
    Task CreateUpdateInternal(InternalAsset asset, bool onlyDetail = false);
    Task UpdateInternalFields(Dictionary<string, object> values, string assetCode, string assetType);
    Task<InternalAsset> GetInternal(object value);
    Task<InternalAsset> GetInternal(object value, string searchType = "");
    Task<InternalAsset> GetInternalByAssetCode(string assetCode);
    Task<int> GetInternalAssetNo(string subCatCode);
    Task<IEnumerable<object>> GetInternalDynamics(string assetCode = "",
            string categoryIds = "",
            string subCategory = "",
            string brand = "",
            string companyCode = "",
            string status = "",
            string fields = "");
    Task<InternalAssetResponse> GetInternalForViewing(int assetId);
    Task<IEnumerable<InternalAsset>> GetInternals(string assetCode = "",
            string categoryIds = "",
            string subCategory = "",
            string brand = "",
            string companyCode = "",
            string status = "");

    Task CreateUpdateExternalAssets(ExternalAsset asset);
    Task<ExternalAsset> GetExternalAsset(string assetCode);

    Task<IEnumerable<ExternalAsset>> GetExternalAssets(
        string plateType = "",
        string plateNum = "",
        string hiredUnder = "",
        string vendor = "",
        string hireSubContract = ""
    );



}