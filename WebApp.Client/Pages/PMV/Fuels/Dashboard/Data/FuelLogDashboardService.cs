

using WebApp.Client.Pages.PMV.Fuels.Dashboard.Models;
using WebApp.Service.Http;

namespace WebApp.Client.Pages.PMV.Fuels.Dashboard.Data;


public interface IFuelLogDashboardService
{   
    Task<FuelDashboardDataModel> GetDashboardReport();
}


public class FuelLogDashboardService : IFuelLogDashboardService
{
    private readonly HttpService _httpService;

    public FuelLogDashboardService(HttpService httpService)
    {
        _httpService = httpService;
    }


    public async Task<FuelDashboardDataModel> GetDashboardReport()
    {
        var url = $"pmv/fuellog/dashboard?isPostBack=false";

        var response = await _httpService.GetAsync<FuelDashboardDataModel>(url);

        return response;
    }

}
