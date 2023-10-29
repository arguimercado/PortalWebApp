
using Microsoft.JSInterop;
using Radzen;
using WebApp.Client.Pages.PMV.Fuels.FuelEntry.Data;
using WebApp.Client.Pages.PMV.Fuels.FuelEntry.Models;
using WebApp.Client.Pages.PMV.Fuels.Models;
using WebApp.UILibrary.Commons;
using WebApp.UILibrary.Components.Common.Spinners;
using WebApp.UILibrary.Providers;

namespace WebApp.Client.Pages.PMV.Fuels.FuelEntry.ViewModels;

public class FuelLogEntryViewModel : ViewModelBase
{
    private readonly IFuelEntryService _service;
    private readonly CustomSpinnerViewModel _spinner;
    private readonly DialogService _dialogService;
    private readonly NotificationService _notification;
    private readonly JSInterOpProvider _jsRuntime;
    private readonly IConfiguration _configuration;

    public FuelLogEntryViewModel(IFuelEntryService service,
        CustomSpinnerViewModel spinner,
        DialogService dialogService,
        NotificationService notificationService,
        IConfiguration configuration,
        JSInterOpProvider jsRuntime)
    {
        _service = service;
        _spinner = spinner;
        _dialogService = dialogService;
        _notification = notificationService;
        _configuration = configuration;
        _jsRuntime = jsRuntime;
    }

    public FuelLogContainerModel FuelLogContainer { get; set; } = new FuelLogContainerModel();
    public IEnumerable<FuelLogEffeciencyModel> Efficiencies { get; set; } = new List<FuelLogEffeciencyModel>();
    public FuelTransactionModel Detail { get; private set; } = new();
    public List<SelectItem> AssetSelection { get; set; } = new();
    public string LogType { get; set; } = "";


    public bool IsLockForEditing()
    {
        return !string.IsNullOrEmpty(FuelLogContainer.FuelLog.Id);
    }

    public bool IsReadonly() => FuelLogContainer.FuelLog.IsPosted;


    #region Grid
    //row
    public void ClearRow()
    {
        Detail = new();
        Detail.FuelDateTime = FuelLogContainer.FuelLog.FueledDate;
        Notify("ClearRow");
    }

    public void EditRow(string Id)
    {
        var transaction = FuelLogContainer
                            .FuelLog
                            .FuelTransactions
                            .Where(d => d.Id == Id).FirstOrDefault();
        if (transaction is not null)
        {
            Detail = FuelTransactionModel.Clone(transaction);
        }

        Notify("EditRow");
    }

    public string GetImage(string name) => $"{_configuration.GetValue<string>("Attachments:ImageUrl")}?ImageName={name}";
    public string GetImagePath() => _configuration.GetValue<string>("Attachments:ImageUrl") ?? "";

    public async Task UpdateMeter(float newMeterValue, string type)
    {
        try
        {
            _spinner.Loading = true;
            var fuelLog = await _service.UpdateMeter(FuelLogContainer.FuelLog.Id!, newMeterValue, type);
            FuelLogContainer.FuelLog.OpeningMeter = fuelLog.OpeningMeter;
            FuelLogContainer.FuelLog.OpeningVariance = fuelLog.OpeningVariance;
            FuelLogContainer.FuelLog.ClosingMeter = fuelLog.ClosingMeter;
            FuelLogContainer.FuelLog.ClosingVariance = fuelLog.ClosingVariance;
            _spinner.Loading = false;
        }
        catch (Exception ex)
        {
            _spinner.Loading = false;
            _notification.Notify(NotificationSeverity.Error, ex.Message);
        }
    }


    public async Task UpdateRow()
    {
        try
        {
            bool isValid = false;
            _spinner.Loading = true;



            if (string.IsNullOrEmpty(Detail.Id))
            {
                //check if entry is exist
                var isExist = FuelLogContainer.FuelLog.FuelTransactions.Any(d => d.AssetCode == Detail.AssetCode && d.AssetCode.ToUpper() != "NONEASSET"
                    && d.Quantity == Detail.Quantity
                    && d.Reading == Detail.Reading);

                if (!isExist)
                {
                    FuelLogContainer.FuelLog.AddDetail(Detail);
                    isValid = true;
                }
                else
                {
                    await _dialogService.Alert($"Asset Code {Detail.AssetCode} is already exist!");
                    isValid = false;
                }
            }
            else
            {
                var transaction = FuelLogContainer.FuelLog.FuelTransactions.Where(d => d.Id == Detail.Id).FirstOrDefault();
                if (transaction is not null)
                {
                    //make sure to have parent id
                    if (string.IsNullOrEmpty(transaction.FuelLogId))
                    {
                        transaction.FuelLogId = FuelLogContainer.FuelLog.Id!;
                    }

                    transaction.AssetCode = Detail.AssetCode;
                    transaction.ProjectCode = Detail.ProjectCode;
                    transaction.FuelDateTime = Detail.FuelDateTime;
                    transaction.Quantity = Detail.Quantity;
                    transaction.Reading = Detail.Reading;
                    transaction.OperatorDriver = Detail.OperatorDriver;
                    transaction.Remarks = Detail.Remarks;
                    transaction.LogType = Detail.LogType;

                    isValid = true;
                }
            }

            //direct update only if the header is save already
            if (!string.IsNullOrEmpty(FuelLogContainer.FuelLog.Id) && isValid)
            {
                await _service.DirectUpdate(Detail);
                ClearRow();
            }
            _spinner.Loading = false;
            Notify("UpdateRow");
        }
        catch (Exception ex)
        {
            _notification.Notify(NotificationSeverity.Error, summary: ex.Message);
            _spinner.Loading = false;
        }
    }

    public void GetAssetSelection(string logType)
    {

        var items = new List<SelectItem>();



        if (logType == "Dispense" || string.IsNullOrEmpty(logType))
        {
            items = FuelLogContainer.AssetCodeSelections.ToList();
        }
        else
        {
            items = FuelLogContainer.StationSelections
                        .Select(s => new SelectItem { Text = s.Code, Value = s.Code })
                        .ToList();
        }
        AssetSelection = items;
        Notify("Update");
    }


    public async Task RemoveDetail(FuelTransactionModel transaction)
    {
        if (FuelLogContainer.FuelLog.FuelTransactions.Any(t => t.Id == transaction.Id))
        {
            var result = await _dialogService.Confirm(message: "Are you sure you want to delete this record?");
            if (result.Value)
            {
                //direct update only if the header is save already
                if (!string.IsNullOrEmpty(FuelLogContainer.FuelLog.Id))
                {

                    transaction.MarkDelete = true;
                    transaction.FuelLogId = FuelLogContainer.FuelLog.Id;

                    await _service.DirectUpdate(transaction);

                    FuelLogContainer.FuelLog.RemoveDetail(transaction);
                    Detail = new();
                    Notify("Remove");
                }
            }
        }
    }
    #endregion
    public async Task GetAssetEfficiency(string logType, string assetCode)
    {
        if (logType.ToLower() == EnumLogType.Dispense.ToString().ToLower())
        {
            IsLoading = true;
            var container = await _service.GetFuelLogEfficiency(assetCode, null, null, true);
            if (container is not null)
            {
                Efficiencies = container.EffeciencyLists;
            }

            IsLoading = false;
        }
    }



    public void ClearAssetEfficiency()
    {
        Efficiencies = new List<FuelLogEffeciencyModel>();
        Notify("Clear");
    }

    public async Task PrintLog()
    {
        try
        {
            var confirmResult = await _dialogService.Confirm("Do you want to print?", "Print Dialog");
            if (confirmResult.HasValue && confirmResult.Value)
            {
                var url = $"{_configuration["Report:ReportUrl"]}/pmv/fuel?documentNo={FuelLogContainer.FuelLog.DocumentNo}";
                await _jsRuntime.Show(url);
                Notify("Print");
            }
        }
        catch (Exception ex)
        {
            _notification
                .Notify(new NotificationMessage
                {
                    Severity = NotificationSeverity.Error,
                    Detail = ex.Message
                });
        }

    }

    public async Task Load(string? id = null, string? type = null)
    {
        try
        {

            bool isPostback = false;
            _spinner.Loading = true;

            if (id is null)
            {
                FuelLogContainer = (await _service.CreateNew())!;
            }
            else
            {
                if (FuelLogContainer.StationSelections.Count() > 0)
                {
                    isPostback = true;
                }
                var result = (await _service.EditFuelLog(id, type, isPostback))!;
                if (isPostback)
                {
                    FuelLogContainer.FuelLog = result.FuelLog;
                }
                else
                {
                    FuelLogContainer = result;
                }
            }

            Detail = new();
            GetAssetSelection(FuelLogContainer.FuelLog.LogType);
            _spinner.Loading = false;

            Notify("Load");
        }
        catch (Exception ex)
        {
            _notification.Notify(NotificationSeverity.Error, summary: ex.Message);
            _spinner.Loading = false;
        }

    }

    public async Task<int> SaveUpdate(string post)
    {
        return string.IsNullOrEmpty(FuelLogContainer.FuelLog.Id) ? await Save(post) : await Update(post);
    }



    private async Task<int> Save(string post)
    {
        try
        {
            var confirmResult = await _dialogService.Confirm("Do you want to save?", "Save Dialog");
            if (confirmResult.Value)
            {
                _spinner.Loading = true;
                if (post == "post")
                {
                    FuelLogContainer.FuelLog.IsPosted = true;
                }
                else if (post == "send")
                {
                    FuelLogContainer.FuelLog.IsSubmitted = true;
                }

                var response = await _service.CreateLog(FuelLogContainer.FuelLog);
                if (response is not null)
                {
                    FuelLogContainer.FuelLog.DocumentNo = response.DocumentNo;
                    FuelLogContainer.FuelLog.Id = response.Id;
                }

                _notification
                   .Notify(new NotificationMessage
                   {
                       Severity = NotificationSeverity.Success,
                       Detail = "Save Successfully"
                   });

                _spinner.Loading = false;
                Notify("Save");
                return 0;
            }
        }
        catch (Exception ex)
        {
            _notification
                .Notify(new NotificationMessage
                {
                    Severity = NotificationSeverity.Error,
                    Detail = ex.Message
                });
            _spinner.Loading = false;
        }
        return -1;
    }

    private async Task<int> Update(string post)
    {
        try
        {
            string message = string.Empty;
            if (post == "post")
            {
                message = "Do you want to post?";
            }
            else if (post == "unpost")
            {
                message = "Do you want to unpost?";
            }
            else
            {
                message = "Do you want to update?";
            }

            var confirmResult = await _dialogService.Confirm(message, "Save Dialog");
            if (confirmResult.Value)
            {
                _spinner.Loading = true;
                if (post == "post" || post == "unpost")
                {
                    FuelLogContainer.FuelLog.IsPosted = true;
                }
                else if (post == "send")
                {
                    FuelLogContainer.FuelLog.IsSubmitted = true;
                }

                await _service.UpdateLog(FuelLogContainer.FuelLog);
                _spinner.Loading = false;

                _notification
                   .Notify(new NotificationMessage
                   {
                       Severity = NotificationSeverity.Success,
                       Detail = "Save Successfully"
                   });

                Notify("Update");
                return 1;
            }
        }
        catch (Exception ex)
        {
            _notification
                .Notify(new NotificationMessage
                {
                    Severity = NotificationSeverity.Error,
                    Detail = ex.Message
                });
            _spinner.Loading = false;
        }

        return -1;
    }


}

