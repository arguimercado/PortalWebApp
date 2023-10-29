using Microsoft.JSInterop;
using Portal.WebClient.Pages.Fuels.FuelEffeciency.Data;
using WebApp.Client.Pages.PMV.Fuels.FuelEffeciency.Models;
using WebApp.UILibrary.Commons;
using WebApp.UILibrary.Components.Common.Spinners;
using WebApp.UILibrary.Providers;


namespace Portal.WebClient.Pages.Fuels.FuelEffeciency.ViewModels;

public class FuelLogEfficiencyViewModel : ViewModelBase
{

    private readonly IEffeciencyService _service;
    private readonly CustomSpinnerViewModel _spinner;
    private readonly IConfiguration _configuration;
    private readonly JSInterOpProvider _runtime;

    public FuelLogEfficiencyViewModel(
        IEffeciencyService service,
        CustomSpinnerViewModel spinner,
        IConfiguration configuration,
        JSInterOpProvider runtime)
    {
        _service = service;
        _spinner = spinner;
        _configuration = configuration;
        _runtime = runtime;
    }


    public FuelEfficiencyListContainer EffeciencyContainer { get; set; } = new();
    public async Task LoadFuelEffeciency(FuelEfficiencyFilterParam args)
    {
        try
        {

            _spinner.Loading = true;
            var result = await _service
                                .GetFuelLogEfficiency(args.StringAssets, args.DateFrom, args.DateTo, args.IsPostBack);

            if (args.IsPostBack) {
                EffeciencyContainer.EffeciencyLists = result.EffeciencyLists;
            }
            else
            {
                EffeciencyContainer = result;
            }
            _spinner.Loading = false;
            Notify("Load");
        }
        catch (Exception ex)
        {
            _spinner.Loading = false;
            Notify("Load");
        }
    }


    public async Task EffeciencyExportExcel(FuelEfficiencyFilterParam args)
    {
        var prms = args.GetType()
                          .GetProperties();

        string urlParam = "";

        foreach (var prop in prms)
        {
            string name = prop.Name;
            object? value = prop.GetValue(args);
            if (value is not null)
            {
                urlParam += $"{prop.Name}={value}&";
            }
        }

        var baseUrl = $"{_configuration["Report:ExportUrl"]}/efficiency?assetCode={args.StringAssets}&dateFrom={args.DateFrom}&dateTo={args.DateTo}&isPostBack=true";

        await _runtime.Show(baseUrl);
        Notify("update");
    }



}
