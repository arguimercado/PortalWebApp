using WebApp.Client.Pages.PMV.Fuels.FuelTracking.Models;
using WebApp.Service.Http;

namespace Portal.WebClient.Pages.Fuels.FuelTracking.Data;

public interface IFuelTrackingService
{
    Task UpdateStatus(string fuelLogId, string status, bool IsPostback = false);
    Task<FuelLogModel> UpdateMeter(string fuelId, float openMeter, string type);
    Task<StationLogContainerModel> GetPendingLogs(bool isPostBack = false);
    Task<FuelLogModel> Void(FuelTransactionModel transactionModel);
    Task DirectUpdate(FuelTransactionModel transactionModel);
    Task MultiplePost(Dictionary<string, string> ids);
    Task<EmployeeModel> GetEmployeeInfo(string employeeId);

    Task<AssignedAssetModel> GetAssetDetailExpress(string assetCode);


}
public class FuelTrackingService : IFuelTrackingService
{
    private readonly HttpService _httpService;

    public FuelTrackingService(HttpService httpService)
    {
        _httpService = httpService;
    }

    const string CONST_URI = "pmv/fuelLog";

    public async Task DirectUpdate(FuelTransactionModel transactionModel) => 
        await _httpService.PostAsync<FuelTransactionModel>($"{CONST_URI}/direct-update", transactionModel);

    public async Task<StationLogContainerModel> GetPendingLogs(bool isPostBack = false) {
        return await _httpService.GetAsync<StationLogContainerModel>($"pmv/FuelLog/getPendingLogs?isPostBack={isPostBack}")
        ?? new();
    }

    public async Task MultiplePost(Dictionary<string, string> ids) {
        var url = $"pmv/fuellog/post";
        var response = await _httpService.PostScalarAsync(url, ids);

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("Posting failed");
        }
    }
    public async Task<FuelLogModel> UpdateMeter(string fuelId, float openMeter, string type)
    {
        var responseMessage = await _httpService.PostAsync<object, FuelLogModel>("pmv/FuelLog/updateMeter", new
        {
            FuelLogId = fuelId,
            Value = openMeter,
            Type = type
        });

        return responseMessage;

    }

    public async Task UpdateStatus(string fuelLogId, string status, bool IsPostback = false)
    {
        var responseMessage = await _httpService.PostAsync<object>("pmv/FuelLog/action",
           new { FuelLogId = fuelLogId, Action = status, IsPostback = IsPostback });
    }

    public async Task<FuelLogModel> Void(FuelTransactionModel transactionModel) => 
        await _httpService.PostAsync<FuelTransactionModel, FuelLogModel>("pmv/FuelLog/void", transactionModel);

    public async Task<EmployeeModel> GetEmployeeInfo(string employeeId)
    {
        return await _httpService.GetAsync<EmployeeModel>($"employee/{employeeId}") ?? new();
    }

    public async Task<AssignedAssetModel> GetAssetDetailExpress(string assetCode)
    {
        return await _httpService.GetAsync<AssignedAssetModel>($"pmv/asset/detail/{assetCode}") ?? new();
    }
}
