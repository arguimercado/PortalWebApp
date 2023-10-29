using Microsoft.JSInterop;
using Radzen;
using WebApp.Client.Pages.PMV.Assets.Data;
using WebApp.Client.Pages.PMV.Assets.Models;
using WebApp.UILibrary.Commons;
using WebApp.UILibrary.Components.Common.Spinners;
using WebApp.UILibrary.Providers;


namespace WebApp.Client.Pages.PMV.Assets.ViewModels;

public class AssetListViewModel : ViewModelBase
{
    private readonly IAssetService _assetService;
    private readonly CustomSpinnerViewModel _spinner;
    private readonly NotificationService _notificationService;
    private readonly IConfiguration _configuration;
    private readonly JSInterOpProvider _jSRuntime;

    public AssetListViewModel(IAssetService assetService,
        CustomSpinnerViewModel spinner,
        NotificationService notificationService,
        IConfiguration configuration,
        JSInterOpProvider jSRuntime)
    {

        _assetService = assetService;
        _spinner = spinner;
        _notificationService = notificationService;
        _configuration = configuration;
        _jSRuntime = jSRuntime;

    }

    public AssetListContainerModel AssetListContainer { get; set; } = new();
    public FilterAssetParam FilterAsset { get; set; } = new();

    public string BaseUrl { get; set; } = "";


    public async Task Initialize()
    {
        try
        {
            _spinner.Loading = true;
            FilterAsset.IsPostBack = false;
            FilterAsset.AssetType = "internal";
            AssetListContainer = await _assetService.GetAssets(FilterAsset);
            _spinner.Loading = false;
            Notify("Initialize");

        }
        catch (Exception ex)
        {
            _spinner.Loading = false;
            _notificationService.Notify(NotificationSeverity.Error, detail: ex.Message);
        }
    }

    public async Task Filter()
    {
        try
        {
            _spinner.Loading = true;
            //check if need doest not contain selection
            if (AssetListContainer.Categories.Count > 0)
            {
                FilterAsset.IsPostBack = true;
            }
            var response = await _assetService.GetAssets(FilterAsset);


            if (FilterAsset.IsPostBack)
            {
                if (FilterAsset.AssetType.ToLower() == "internal")
                    AssetListContainer.AssetData = response.AssetData;
                else
                    AssetListContainer.ExternalData = response.ExternalData;
            }

            if (FilterAsset.IsRefresh)
            {
                AssetListContainer.Categories = response.Categories;
                AssetListContainer.SubCategories = response.SubCategories;
                AssetListContainer.Companies = response.Companies;
                AssetListContainer.Vendors = response.Vendors;
                AssetListContainer.Brands = response.Brands;
                AssetListContainer.Statuses = response.Statuses;
                AssetListContainer.HireSub = response.HireSub;
                AssetListContainer.PlateTypes = response.PlateTypes;
            }

            _spinner.Loading = false;

            Notify("Filter");
        }
        catch (Exception ex)
        {
            _notificationService.Notify(NotificationSeverity.Error, detail: ex.Message);
        }

    }

    public async Task ExportToExcel()
    {
        var prms = FilterAsset.GetType()
                           .GetProperties();

        string urlParam = "";
        foreach (var prop in prms)
        {
            string name = prop.Name;
            object? value = prop.GetValue(FilterAsset);
            if (value is not null)
            {
                urlParam += $"{prop.Name}={value}&";
            }
        }

        var baseUrl = $"{_configuration["Report:ExportUrl"]}/assetexport?{urlParam.Substring(0, urlParam.Length - 1)}";
        await _jSRuntime.Show(baseUrl);

        Notify("update");

    }


}
