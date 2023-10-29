using Radzen;
using WebApp.Client.Pages.PMV.Assets.Data;
using WebApp.Client.Pages.PMV.Assets.Models;
using WebApp.UILibrary.Commons;
using WebApp.UILibrary.Components.Common.Spinners;

namespace WebApp.Client.Pages.PMV.Assets.ViewModels;

public class AssetDashboardViewModel : ViewModelBase
{
    private readonly IAssetService _service;
    private readonly CustomSpinnerViewModel _spinner;
    private readonly DialogService _dialogService;

    public AssetDashboardViewModel(IAssetService service, CustomSpinnerViewModel spinner, DialogService dialogService)
    {
        _service = service;
        _spinner = spinner;
        _dialogService = dialogService;
    }
    public AssetDashboardContainer Dashboard { get; set; } = new();
    public CostReportModel MCDashboard { get; set; } = new();

    public async Task LoadCategoryCount()
    {
        try
        {
            _spinner.Loading = true;
            Dashboard = await _service.GetCategoryCount();
            Notify("Load");

            _spinner.Loading = false;
        }
        catch (Exception ex)
        {
            await _dialogService.Alert(ex.Message);
            _spinner.Loading = false;
        }
    }

    public async Task LoadMaintenanceCost(MCRequest request, bool isPostback = false)
    {
        try
        {
            _spinner.Loading = true;
            request.IsPostback = isPostback;
            var results = await _service.GetMaintenanceCost(request);

            if (request.IsPostback)
            {
                MCDashboard.Summary = results.Transactions.GroupBy(c => c.AssetCode)
                                .Select(c => new AssetSIVSummary
                                {
                                    AssetCode = c.Key,
                                    Transactions = results.Transactions.Where(t => t.AssetCode == c.Key).ToList()
                                }).ToList();

            }
            else
            {
                MCDashboard.Categories = results.Categories;
                MCDashboard.SubCategories = results.SubCategories;
                MCDashboard.Assets = results.Assets;
            }


            Notify("Load");
            _spinner.Loading = false;
        }
        catch (Exception ex)
        {
            await _dialogService.Alert(ex.Message);
            _spinner.Loading = false;
        }
    }
}