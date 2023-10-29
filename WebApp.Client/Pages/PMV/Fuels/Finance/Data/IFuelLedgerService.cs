using Microsoft.JSInterop;
using WebApp.Client.Pages.PMV.Fuels.Finance.Models;
using WebApp.Service.Http;
using WebApp.UILibrary.Providers;

namespace Portal.WebClient.Pages.Fuels.Finance.Data;

public interface IFuelLedgerService
{
    Task<IEnumerable<TankerStockContainer>> Load();
    Task<TankerStockContainer> Create(int month,int year, bool forcePost);

    Task Export(int month, int year);
}

public class FuelLedgerService : IFuelLedgerService
{
    private readonly HttpService _httpService;
    private readonly JSInterOpProvider _jSRuntime;

    public FuelLedgerService(HttpService httpService,JSInterOpProvider jSRuntime) {
        _httpService = httpService;
        _jSRuntime = jSRuntime;
    }

    public async Task<TankerStockContainer> Create(int month,int year, bool forcePost) {
        return await _httpService.PostAsync<object, TankerStockContainer>("fuel/stock", 
            new { month = month, year = year, forcePost = forcePost }); 
    }

    public async Task<IEnumerable<TankerStockContainer>> Load()
    {
        return await _httpService.GetAsync<IEnumerable<TankerStockContainer>>("fuel/stock") ?? new List<TankerStockContainer>();
    }

    public async Task Export(int month,int year)
    {
        var url = $"{_httpService.GetUrlBase()}fuel/stock/export?Month={month}&Year={year}";
        
        await _jSRuntime.Show(url);
    }


}
