using WebApp.Client.Pages.PMV.Fuels.FuelEffeciency.Models;
using WebApp.Service.Http;

namespace Portal.WebClient.Pages.Fuels.FuelEffeciency.Data;


public interface IEffeciencyService
{
    Task<FuelEfficiencyListContainer> GetFuelLogEfficiency(string assetCode, DateTime? dateFrom, DateTime? dateTo, bool isPostBack = false);
}

public class EffeciencyService : IEffeciencyService
{
    public HttpService _httpService;

    public EffeciencyService(HttpService httpService)
    {
        _httpService = httpService;
    }

    public async Task<FuelEfficiencyListContainer> GetFuelLogEfficiency(string assetCode, DateTime? dateFrom, DateTime? dateTo, bool isPostBack = false)
    {
        string dateString = "";
        if (dateFrom.HasValue && dateTo.HasValue)
        {
            dateString = $"dateFrom={dateFrom}&dateTo={dateTo}&";
        }
        var url = $"pmv/FuelLog/efficiency?assetCode={assetCode}&{dateString}isPostBack={isPostBack}";
        var response = await _httpService.GetAsync<FuelEfficiencyListContainer>(url);

        return response;
    }
}
