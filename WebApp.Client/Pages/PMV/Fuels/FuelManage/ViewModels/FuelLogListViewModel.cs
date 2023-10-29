using Microsoft.JSInterop;
using Radzen;
using WebApp.Client.Pages.PMV.Fuels.FuelManage.Data;
using WebApp.Client.Pages.PMV.Fuels.FuelManage.Models;
using WebApp.UILibrary.Commons;
using WebApp.UILibrary.Components.Common.Spinners;
using WebApp.UILibrary.Providers;

namespace WebApp.Client.Pages.PMV.Fuels.FuelManage.ViewModels;

public class FuelLogListViewModel : ViewModelBase
{
    private readonly ILogListService _service;
    private readonly CustomSpinnerViewModel _spinner;
    private readonly IConfiguration _configuration;
    private readonly JSInterOpProvider _runtime;
    private readonly DialogService _dialogService;
    private readonly NotificationService _notificationService;

    public FuelLogListViewModel(ILogListService service,
        CustomSpinnerViewModel spinner,
        IConfiguration configuration,
        JSInterOpProvider runtime,
        DialogService dialogService,
        NotificationService notificationService)
    {


        _service = service;
        _spinner = spinner;
        _configuration = configuration;
        _runtime = runtime;
        _dialogService = dialogService;
        _notificationService = notificationService;
    }

    public FuelListContainer ReportContainer { get; set; } = new();
    public FuelSearchFilterParam Filter { get; set; } = new();

    public async Task LoadFuelLogReport()
    {
        _spinner.Loading = true;
        var result = await _service.GetFuelLogList(Filter.StringFuelStation, Filter.DateFrom, Filter.DateTo, Filter.IsPostBack);
        if (Filter.IsPostBack)
        {
            ReportContainer.Masters = result.Masters;
        }
        else
        {
            ReportContainer = result;
        }
        _spinner.Loading = false;
        Notify("Load");
    }

    public async Task FuelTransactionExportExcel(FuelSearchFilterParam args)
    {
        var baseUrl = $"{_configuration["Report:ExportUrl"]}/fueltransaction?stations={args.StringFuelStation}&dateFrom={args.DateFrom}&dateTo={args.DateTo}&isPostBack=true";

        await _runtime.Show(baseUrl);

    }

    public List<string> PostingIds { get; set; } = new();

    public void AddToPost(string postingId)
    {
        if (!PostingIds.Any(p => p == postingId))
        {
            PostingIds.Add(postingId);
        }
        Notify("Update");
    }

    public void RemoveFromPost(string postingId)
    {
        if (PostingIds.Any(p => p == postingId))
        {
            PostingIds.Remove(postingId);
        }
        Notify("Update");
    }

    public async Task<bool> MultiplePost()
    {
        try
        {
            var confirm = await _dialogService.Confirm("Are you sure you want to post transactions?");
            if (confirm.Value)
            {
                _spinner.Loading = true;
                var postIds = new Dictionary<string, string>();
                postIds.Add("Ids", string.Join(",", PostingIds));
                await _service.MultiplePost(postIds);
                _spinner.Loading = false;
                PostingIds.Clear();
                Notify("Update");
                return true;
            }

        }
        catch (Exception ex)
        {
            _spinner.Loading = false;
            _notificationService.Notify(NotificationSeverity.Error, ex.Message);

        }
        return false;
    }




}
