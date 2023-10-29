using Radzen;
using WebApp.Client.Pages.PMV.Fuels.Dashboard.Data;
using WebApp.Client.Pages.PMV.Fuels.Dashboard.Models;
using WebApp.UILibrary.Commons;
using WebApp.UILibrary.Components.Common.Spinners;

namespace WebApp.Client.Pages.PMV.Fuels.Dashboard.ViewModels;

public class FuelDashboardViewModel : ViewModelBase
{
    private readonly IFuelLogDashboardService _service;
    private readonly CustomSpinnerViewModel _spinner;
    private readonly DialogService _dialogService;
    public FuelDashboardViewModel(IFuelLogDashboardService service, CustomSpinnerViewModel spinner, DialogService dialogService)
    {
        _service = service;
        _spinner = spinner;
        _dialogService = dialogService;
    }

    public FuelDashboardDataModel FuelDashboard { get; set; } = new();

    public async Task Load()
    {
        try
        {
            _spinner.Loading = true;
            FuelDashboard = await _service.GetDashboardReport();
            Notify("Load");
            _spinner.Loading = false;
        }
        catch (Exception ex)
        {
            _dialogService.Alert(ex.Message);
            _spinner.Loading = false;
        }
    }
}
