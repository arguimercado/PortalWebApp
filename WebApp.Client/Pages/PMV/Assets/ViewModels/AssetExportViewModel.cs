using Module.PMV.Core.Assets.Features.DTOs.Assets.Request;
using Radzen;
using WebApp.Client.Pages.PMV.Assets.Data;
using WebApp.Client.Pages.PMV.Assets.Models;
using WebApp.UILibrary.Commons;
using WebApp.UILibrary.Components.Common.Spinners;
using WebApp.UILibrary.Providers;

namespace WebApp.Client.Pages.PMV.Assets.ViewModels;

public class AssetExportViewModel : ViewModelBase
{
    private readonly IAssetService _assetService;
    private readonly CustomSpinnerViewModel _spinner;
    private readonly NotificationService _notificationService;
    private readonly IConfiguration _configuration;
    private readonly JSInterOpProvider _jSRuntime;

    public AssetExportViewModel(IConfiguration configuration,
                            IAssetService assetService,
                            CustomSpinnerViewModel spinner,
                            NotificationService notificationService,
                            JSInterOpProvider jSRuntime)
    {

        _configuration = configuration;
        _assetService = assetService;
        _spinner = spinner;
        _notificationService = notificationService;
        _jSRuntime = jSRuntime;
    }

    public async Task ExportMaintenanceCost(MCRequest request)
    {
        var prms = request.GetType()
                           .GetProperties();

        string urlParam = "";

        if (!string.IsNullOrEmpty(request.CategoryId))
        {
            urlParam += $"CategoryId={request.CategoryId}&";
        }

        if (!string.IsNullOrEmpty(request.SubCategory))
        {
            urlParam += $"SubCategory={request.SubCategory}&";
        }

        if (request.DateFrom.HasValue && request.DateTo.HasValue)
        {
            urlParam += $"DateFrom={request.DateFrom}&DateTo={request.DateTo}&";
        }

        if (request.AssetCodes is not null && request.AssetCodes.Count() > 0)
        {
            foreach (var assetCode in request.AssetCodes)
            {
                urlParam += $"AssetCodes={assetCode}&";
            }
        }

        var baseUrl = $"{_configuration["Report:ExportUrl"]}/maintenance-export?{urlParam.Substring(0, urlParam.Length - 1)}";

        await _jSRuntime.Show(baseUrl);

        Notify("update");

    }

   
}
