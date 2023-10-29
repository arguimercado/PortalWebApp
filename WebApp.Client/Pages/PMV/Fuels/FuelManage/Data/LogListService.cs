using WebApp.Client.Pages.PMV.Fuels.FuelManage.Models;
using WebApp.Service.Http;


namespace WebApp.Client.Pages.PMV.Fuels.FuelManage.Data;


public interface ILogListService
{
    Task<FuelListContainer> GetFuelLogList(string station, DateTime dateFrom, DateTime dateTo, bool isPostBack = false);

    Task MultiplePost(Dictionary<string, string> ids);
}
public class LogListService : ILogListService
{
    private HttpService _httpService;

    public LogListService(HttpService httpService)
    {
        _httpService = httpService;
    }

    public async Task<FuelListContainer> GetFuelLogList(string station, DateTime dateFrom, DateTime dateTo, bool isPostBack = false)
    {
        var url = $"pmv/FuelLog/list?stations={station}&dateFrom={dateFrom}&dateTo={dateTo}&isPostBack={isPostBack}";
        var response = await _httpService.GetAsync<FuelListContainer>(url);
        return response;
    }

    public async Task MultiplePost(Dictionary<string, string> ids)
    {
        var url = $"pmv/fuellog/post";
        var response = await _httpService.PostScalarAsync(url, ids);

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("Posting failed");
        }
    }
}
