using Microsoft.JSInterop;
using Radzen;
using Portal.WebClient.Pages.Fuels.FuelTracking.Data;
using WebApp.Client.Pages.PMV.Fuels.FuelTracking.Models;
using WebApp.UILibrary.Commons;
using WebApp.UILibrary.Components.Common.Spinners;
using WebApp.Client.Pages.PMV.Fuels.Models;
using WebApp.UILibrary.Providers;

namespace Portal.WebClient.Pages.Fuels.FuelTracking.ViewModels;

public class FuelTrackingViewModel : ViewModelBase {
    
    private readonly IFuelTrackingService _fuelTrackingService;
    private readonly CustomSpinnerViewModel _spinner;
    private readonly NotificationService _notificationService;
    private readonly DialogService _dialogService;
    private readonly IConfiguration _configuration;
    private readonly JSInterOpProvider _jSRuntime;

    public FuelTrackingViewModel(
        CustomSpinnerViewModel spinner,
        NotificationService notificationService,
        DialogService dialogService,
        IConfiguration configuration,
        JSInterOpProvider jSRuntime,
        IFuelTrackingService fuelTrackingService) {
    
        
        _spinner = spinner;
        _notificationService = notificationService;
        _dialogService = dialogService;
        _configuration = configuration;
        _jSRuntime = jSRuntime;
        _fuelTrackingService = fuelTrackingService;
    }

    public StationLogContainerModel Container { get; set; } = new();
    public IEnumerable<StationLogModel> Stations => Container.Stations;
    public StationLogModel SelectedStation { get; set; } = new();
    public FuelLogModel? SelectedLog { get; set; } = null;
    public FuelTransactionModel FuelEntry { get; set; } = new();
    public EmployeeModel Employee { get; set; } = new();
    public AssignedAssetModel AssetInformation { get; set; } = new();

    public void CreateNewEntry()
    {
        FuelEntry = new();
    }

    public void SelectStation(StationLogModel station)
    {
        SelectedStation = station;
        SelectedLog = null;
        Notify("update");
    }

    public void SelectLog(FuelLogModel log)
    {
        SelectedLog = log;
        Notify("update");
    }

    public async Task ToggleLocked()
    {
        try
        {
            string message = "";
            string status = "";

            if (SelectedLog!.Status == EnumFuelLogStatus.Locked.ToString())
            {
                message = "unfreeze ";
            }
            else
            {
                message = "freeze ";
            }

            var confirmation = await _dialogService.Confirm($"Do you want to {message} the Log?", "Confirmation");
            if (confirmation.HasValue && confirmation.Value == true)
            {
                if (SelectedLog!.Status == EnumFuelLogStatus.Locked.ToString())
                {
                    if (SelectedLog.ApprovedDate != null)
                    {
                        status = EnumFuelLogStatus.Approved.ToString();
                    }
                    else
                    {
                        status = EnumFuelLogStatus.Pending.ToString();
                    }
                }
                else
                {
                    status = EnumFuelLogStatus.Locked.ToString();
                }


                _spinner.Loading = true;
                SelectedLog.Status = status;
                await _fuelTrackingService.UpdateStatus(SelectedLog?.Id!, status, true);
                _spinner.Loading = false;
                Notify("Update");
            }
        }
        catch (Exception ex)
        {
            _spinner.Loading = false;
            _notificationService.Notify(NotificationSeverity.Error, ex.Message);
        }
    }

    public async Task UpdateMeter(float newMeterValue,string type)
    {
        try
        {
            
            _spinner.Loading = true;
            var fuelLog = await _fuelTrackingService.UpdateMeter(SelectedLog!.Id!, newMeterValue,type);
            SelectedLog.OpeningMeter = fuelLog.OpeningMeter;
            SelectedLog.OpeningVariance = fuelLog.OpeningVariance;
            SelectedLog.ClosingMeter = fuelLog.ClosingMeter;
            SelectedLog.ClosingVariance = fuelLog.ClosingVariance;
            _spinner.Loading = false;
        }
        catch(Exception ex)
        {
            _spinner.Loading = false;
            _notificationService.Notify(NotificationSeverity.Error, ex.Message);
        }
    }

    public async Task SetStatus(string status)
    {
        try
        {

            var confirmation = await _dialogService.Confirm($"Do you want to {status} the Log?", "Confirmation");
            if (confirmation.HasValue && confirmation.Value == true)
            {
                if (SelectedLog is not null)
                {
                    _spinner.Loading = true;
                    await _fuelTrackingService.UpdateStatus(SelectedLog?.Id!, status);
                    SelectedLog!.Status = status;
                    _spinner.Loading = false;

                    Notify("Update");
                }
            }
        }
        catch (Exception ex)
        {
            _spinner.Loading = false;
            _notificationService.Notify(NotificationSeverity.Error, ex.Message);
        }
    }

    public string GetImagePath => _configuration.GetValue<string>("Attachments:ImageUrl") ?? "";
    public string GetImage(string name) => $"{_configuration.GetValue<string>("Attachments:ImageUrl")}?ImageName={name}";
    public async Task Load()
    {
        try
        {
            _spinner.Loading = true;
            Container = await _fuelTrackingService.GetPendingLogs(true);
            Notify("update");
            _spinner.Loading = false;
        }
        catch (Exception ex)
        {
            _notificationService.Notify(NotificationSeverity.Error, ex.Message);
            _spinner.Loading = false;
        }
    }

    public async Task ReInitialize()
    {
        try
        {
            if (Container.AssetCodes.Count() <= 0)
            {
                _spinner.Loading = true;
                var container = await _fuelTrackingService.GetPendingLogs(false);
                Container.AssetCodes = container.AssetCodes;
                Container.ProjectCodes = container.ProjectCodes;
                Container.LogTypes = container.LogTypes;
                Notify("update");
                _spinner.Loading = false;
            }

        }
        catch (Exception ex) {
            _notificationService.Notify(NotificationSeverity.Error, ex.Message);
            _spinner.Loading = false;
        }
    
    }

    public async Task PrintLog() {

        var confirmResult = await _dialogService.Confirm("Do you want to print?", "Print Dialog");
        if (confirmResult.Value) {
            var url = $"{_configuration["Report:ReportUrl"]}/pmv/fuel?documentNo={SelectedLog!.DocumentNo}";
            await _jSRuntime.Show(url);
            Notify("Print");
        }
    }

    public async Task Void()
    {
        try
        {
            _spinner.Loading = true;
            var result = _fuelTrackingService.Void(FuelEntry);
            Notify("Update");
            _spinner.Loading = false;
        }
        catch (Exception ex)
        {
            _notificationService.Notify(NotificationSeverity.Error, ex.Message);
            _spinner.Loading = false;
        }
    }

    public async Task AddTransaction()
    {
        try
        {
            if (SelectedLog is not null)
            {
                IsLoading = true;
                FuelEntry.FuelLogId = SelectedLog.Id!;
                await _fuelTrackingService.DirectUpdate(FuelEntry);
                
                var container = await _fuelTrackingService.GetPendingLogs(true);

                Container.Stations = container.Stations;
                SelectedStation = Container.Stations.First(s => s.StationCode == SelectedStation.StationCode);
                SelectedLog = SelectedStation.Logs.First(l => l.Id == SelectedLog.Id);
                IsLoading = false;

                Notify("Add");
            }


        }
        catch (Exception ex)
        {
            _notificationService.Notify(NotificationSeverity.Error, ex.Message);
            IsLoading = false;
        }
    }

    public async Task Post()
    {
        try
        {
            _spinner.Loading = true;
            
            await _fuelTrackingService
                     .MultiplePost(new Dictionary<string, string> { { "Ids", SelectedLog!.Id!.ToString() } });
            
            var container = await _fuelTrackingService.GetPendingLogs(true);

            Container.Stations = container.Stations;
            SelectedStation = Container.Stations.First(s => s.StationCode == SelectedStation.StationCode);

            _spinner.Loading = false;
            
            Notify("Update");

        }
        catch (Exception ex)
        {
            _notificationService.Notify(NotificationSeverity.Error, ex.Message);
            _spinner.Loading = false;
        }
    }

    public async Task GetEmployeeInfo(string employeeId)
    {
        try
        {
            _spinner.Loading = true;
            Employee = await _fuelTrackingService.GetEmployeeInfo(employeeId);
            _spinner.Loading = false;
            Notify("Update");

        }
        catch(Exception ex)
        {
            _notificationService.Notify(NotificationSeverity.Error, ex.Message);
            _spinner.Loading = false;
        }
    }

    public async Task GetAssetDetail(string assetCode)
    {
        try
        {
            _spinner.Loading = true;
            AssetInformation = await _fuelTrackingService.GetAssetDetailExpress(assetCode);
            _spinner.Loading = false;
            Notify("Update");

        }
        catch (Exception ex)
        {
            _notificationService.Notify(NotificationSeverity.Error, ex.Message);
            _spinner.Loading = false;
        }
    }

    
}
