using WebApp.Client.Pages.PMV.Fuels.Stations.Models;
using WebApp.Service.Http;

namespace WebApp.Client.Pages.PMV.Fuels.Stations.Data;

public interface IStationService
{
    Task<FuelStationContainerModel> GetStations();

    Task<FuelStationModel> CreateUpdateStation(FuelStationModel model);
}
public class StationService : IStationService
{
    private readonly HttpService _httpService;

    public StationService(HttpService httpService)
    {
        _httpService = httpService;
    }

    public async Task<FuelStationModel> CreateUpdateStation(FuelStationModel model)
    {
        return await _httpService.PostAsync<FuelStationModel, FuelStationModel>("pmv/station", model);
    }

    public async Task<FuelStationContainerModel> GetStations()
    {
        return await _httpService.GetAsync<FuelStationContainerModel>("pmv/station");
    }
}
