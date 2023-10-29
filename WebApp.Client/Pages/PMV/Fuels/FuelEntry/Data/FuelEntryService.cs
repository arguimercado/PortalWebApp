using WebApp.Client.Pages.PMV.Fuels.FuelEntry.Models;
using WebApp.Service.Http;

namespace WebApp.Client.Pages.PMV.Fuels.FuelEntry.Data;

public interface IFuelEntryService
{
    Task<FuelLogContainerModel?> CreateNew();
    Task<FuelLogContainerModel?> EditFuelLog(string? id = null, string? type = null, bool isPostBack = false);
    Task DirectUpdate(FuelTransactionModel transactionModel);
    Task<FuelLogKeyResponse?> CreateLog(FuelLogModel request);
    Task UpdateLog(FuelLogModel request);
    Task TogglePost(string id);
    Task<FuelLogGenReportModel.Container> GetReportInstance(FuelLogGenReportModel.Parameter reportParam);
    Task<FuelEfficiencyContainerModel?> GetFuelLogEfficiency(string assetCode, DateTime? dateFrom, DateTime? dateTo, bool isPostBack = false);

    Task<FuelLogModel> UpdateMeter(string fuelId, float openMeter, string type);


}
public class FuelEntryService : IFuelEntryService
{
    private HttpService _httpService;

    public FuelEntryService(HttpService httpService)
    {
        _httpService = httpService;
    }

    public async Task<FuelLogKeyResponse?> CreateLog(FuelLogModel request) =>
        await _httpService.PostAsync<FuelLogModel, FuelLogKeyResponse?>($"pmv/fuelLog", request);

    public async Task<FuelLogContainerModel?> CreateNew() =>
        await _httpService.GetAsync<FuelLogContainerModel>("pmv/FuelLog/new");

    public async Task DirectUpdate(FuelTransactionModel transactionModel)
        => await _httpService.PostAsync($"pmv/fuelLog/direct-update", transactionModel);


    public async Task<FuelLogContainerModel?> EditFuelLog(string? id = null, string? type = null, bool isPostBack = false)
        => await _httpService.GetAsync<FuelLogContainerModel>($"pmv/fuelLog/single?documentNo={id}&type={type}&isPostBack={isPostBack}");

    public async Task<FuelEfficiencyContainerModel?> GetFuelLogEfficiency(string assetCode, DateTime? dateFrom, DateTime? dateTo, bool isPostBack = false)
    {
        string dateString = "";
        if (dateFrom.HasValue && dateTo.HasValue)
        {
            dateString = $"dateFrom={dateFrom}&dateTo={dateTo}&";
        }
        var url = $"pmv/FuelLog/efficiency?assetCode={assetCode}&{dateString}isPostBack={isPostBack}";
        var response = await _httpService.GetAsync<FuelEfficiencyContainerModel>(url);

        return response;
    }
    public async Task<FuelLogGenReportModel.Container> GetReportInstance(FuelLogGenReportModel.Parameter reportParam)
    {
        var url = $"pmv/fuellog/report-instance?reportType={reportParam.ReportType}&isPostBack=false";

        var response = await _httpService.GetAsync<FuelLogGenReportModel.Container>(url);

        return response;
    }

  
    public async Task TogglePost(string id) => await _httpService.PostAsync("pmv/FuelLog/unpost", id);

    public async Task UpdateLog(FuelLogModel request)
    {
        var result = await _httpService.PostAsync<object>($"pmv/fuelLog/update", new
        {
            request.Id,
            request.Station,
            request.FueledDate,
            request.ShiftStartTime,
            request.ShiftEndTime,
            request.Remarks,
            request.Fueler,
            request.IsPosted,
            request.IsSubmitted,
            request.LogType,
            request.OpeningBalance,
            request.OpeningMeter
        });
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

}
