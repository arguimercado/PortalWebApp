using System.Net.Http.Json;
using WebApp.Client.Pages.PMV.Assets.Models;
using WebApp.Service.Http;

namespace WebApp.Client.Pages.PMV.Assets.Data;
public interface IAssetService
{
    Task<AssetContainerModel?> CreateNew(string assetType, bool isPostBack = false);
    Task<AssetListContainerModel?> GetAssets(FilterAssetParam filterAssetParam);
    Task<AssetContainerModel?> GetAsset(string searchValue, string searchType, string assetType, bool IsPostBack);
    Task<string> GetAssetNo(string subCategory);
    Task<AssetReadModel?> ViewAsset(int id);
    Task Save(AssetContainerModel assetContainerModel, string assetType);

    //deprecated
    Task<AssetSMUResponse> UpdateAssetRecord(string assetCode);
    Task<AssetDashboardContainer> GetCategoryCount();
    Task<CostReportModel> GetMaintenanceCost(MCRequest request);
    Task UpdateAssignedDriver(AssignedDriver assignedDriver, string assetType);
    Task DeleteAssignedDriver(int assignedId);
    Task<IEnumerable<AssignedDriver>> GetAllAssignedDrivers(string assetCode);

    Task<AssignedAssetModel> GetAssetDetailExpress(string assetCode);

    #region document
    Task<AssetDocument?> UploadDocument(AssetDocument document);
    Task DeleteDocument(string documentId);
    #endregion

    #region service
    Task UpdateServiceDue(int assetId, ServiceDueEntryModel serviceEntry);
    Task DeleteServiceDue(string serviceId);
    Task<IEnumerable<ServiceDueEntryModel>> CreateServiceDue(int assetId, string groupAlertId);
    #endregion

}
public class AssetService : IAssetService
{
    private readonly HttpService _httpService;

    public AssetService(HttpService httpService)
    {
        _httpService = httpService;
    }

    public async Task<AssetContainerModel?> CreateNew(string assetType, bool isPostBack = false)
    {
        var url = $"pmv/asset/new?assetType={assetType}&isPostBack={isPostBack}";
        var result = await _httpService.GetAsync<AssetContainerModel?>(url);

        return result;
    }

    public async Task<AssetListContainerModel?> GetAssets(FilterAssetParam filterAssetParam)
    {
        var prms = filterAssetParam
                            .GetType()
                            .GetProperties();

        string urlParam = "";
        foreach (var prop in prms)
        {
            string name = prop.Name;
            object? value = prop.GetValue(filterAssetParam);
            if (value is not null)
            {
                urlParam += $"{prop.Name}={value}&";
            }
        }

        var url = $"pmv/asset?{urlParam.Substring(0, urlParam.Length - 1)}";

        var result = await _httpService.GetAsync<AssetListContainerModel?>(url);
        return result;
    }

    public async Task<AssetContainerModel?> GetAsset(string searchValue, string searchType, string assetType, bool IsPostBack)
    {

        string url = $"pmv/asset/edit?searchValue={searchValue}&searchType={searchType}&assetType={assetType}&isPostBack={IsPostBack}";
        var result = await _httpService.GetAsync<AssetContainerModel?>(url);
        return result;
    }

    public async Task Save(AssetContainerModel assetContainerModel, string assetType)
    {
        var url = $"pmv/asset/save/{assetType}";
        await _httpService.PostAsync(url, assetContainerModel);
    }

    #region ServiceDue
    public async Task<IEnumerable<ServiceDueEntryModel>> CreateServiceDue(int assetId, string groupAlertId)
    {
        var result = await _httpService.PostAsync<object, IEnumerable<ServiceDueEntryModel>>($"pmv/AssetService",
            new { AssetId = assetId, GroupAlertId = groupAlertId });

        return result;
    }

    public async Task UpdateServiceDue(int assetId, ServiceDueEntryModel serviceEntry)
    {
        await _httpService.PostAsync($"pmv/AssetService/update/{assetId}", serviceEntry);
    }

    public async Task DeleteServiceDue(string serviceId)
    {
        await _httpService.DeleteAsync($"pmv/AssetService/{serviceId}");
    }
    #endregion

    public async Task<AssetReadModel?> ViewAsset(int id)
    {

        string url = $"pmv/asset/view/{id}";

        var result = await _httpService.GetAsync<AssetReadModel?>(url);

        return result;
    }

    public async Task<AssetDocument?> UploadDocument(AssetDocument document)
    {


        string url = "pmv/assetDocument/upload";

        var response = await _httpService.PostFormData(url, document.GetFormData());
        if (response!.IsSuccessStatusCode)
        {
            var returnValue = await response.Content.ReadFromJsonAsync<AssetDocument>();

            return returnValue;
        }

        return new();
    }

    public async Task DeleteDocument(string documentId)
    {
        string url = $"pmv/assetDocument/{documentId}";
        await _httpService.DeleteAsync(url);
    }

    public async Task<AssetSMUResponse> UpdateAssetRecord(string assetCode)
    {
        string url = "pmv/asset/record";
        var response = await _httpService.PostAsync<object, AssetSMUResponse>(url, new { AssetCode = assetCode });

        return response;
    }

    public async Task<AssetDashboardContainer> GetCategoryCount()
    {
        string url = "pmv/assetdashboard/asset-count";
        var response = await _httpService.GetAsync<AssetDashboardContainer>(url);

        return response;

    }

    public async Task<CostReportModel> GetMaintenanceCost(MCRequest request)
    {

        string url = "pmv/AssetDashboard/maintenance-cost";

        var response = await _httpService.PostAsync<MCRequest, CostReportModel>(url, request);

        return response;
    }

    public async Task<string> GetAssetNo(string subCategory)
    {
        var response = await _httpService
                                .GetScalarAsync($"pmv/asset/new-assetcode/{subCategory}");

        return response;

    }

    public async Task DeleteAssignedDriver(int assignedId)
    {
        await _httpService
                .PostAsync<object>("pmv/asset/driver/delete", new { AssignedId = assignedId });
    }

    public async Task<IEnumerable<AssignedDriver>> GetAllAssignedDrivers(string assetCode)
    {
        return await _httpService.GetAsync<IEnumerable<AssignedDriver>>($"pmv/asset/driver?AssetCode={assetCode}")
            ?? new List<AssignedDriver>();
    }

    public async Task UpdateAssignedDriver(AssignedDriver assignedDriver, string assetType)
    {
        await _httpService
                .PostAsync<AssignedDriver>($"pmv/asset/driver/assign/{assetType}", assignedDriver);
    }

    

    public async Task<AssignedAssetModel> GetAssetDetailExpress(string assetCode)
    {
        return await _httpService.GetAsync<AssignedAssetModel>($"pmv/asset/detail/{assetCode}") ?? new();
    }
}
