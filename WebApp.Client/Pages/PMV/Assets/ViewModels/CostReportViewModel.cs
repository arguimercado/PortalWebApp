using Radzen;
using WebApp.Client.Pages.PMV.Assets.Data;
using WebApp.Client.Pages.PMV.Assets.Models;
using WebApp.UILibrary.Commons;
using WebApp.UILibrary.Components.Common.Spinners;

namespace WebApp.Client.Pages.PMV.Assets.ViewModels;

public class CostReportViewModel : ViewModelBase
{
    private readonly IAssetService _service;
    private readonly CustomSpinnerViewModel _spinner;
    private readonly DialogService _dialogService;

    public CostReportViewModel(IAssetService service, CustomSpinnerViewModel spinner, DialogService dialogService)
    {
        _service = service;
        _spinner = spinner;
        _dialogService = dialogService;
    }

    public CostReportModel CostReport { get; set; } = new();

    public async Task LoadMaintenanceCost(MCRequest request, bool isPostback = false)
    {
        try
        {
            _spinner.Loading = true;
            request.IsPostback = isPostback;
            var results = await _service.GetMaintenanceCost(request);

            if (request.IsPostback)
            {
                CostReport.Summary = results.Transactions.GroupBy(c => c.AssetCode)
                                .Select(c => new AssetSIVSummary
                                {
                                    AssetCode = c.Key,
                                    Transactions = results.Transactions.Where(t => t.AssetCode == c.Key).ToList()
                                }).ToList();

            }
            else
            {
                CostReport.Categories = results.Categories;
                CostReport.SubCategories = results.SubCategories;
                CostReport.Assets = results.Assets;
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
