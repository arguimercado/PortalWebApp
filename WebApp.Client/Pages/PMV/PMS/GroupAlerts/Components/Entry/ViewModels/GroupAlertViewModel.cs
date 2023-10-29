using Radzen;
using WebApp.Client.Pages.PMV.PMS.GroupAlerts.Data;
using WebApp.Client.Pages.PMV.PMS.GroupAlerts.Models;
using WebApp.UILibrary.Commons;
using WebApp.UILibrary.Components.Common.Spinners;

namespace WebApp.Client.Pages.PMV.PMS.GroupAlerts.Components.Entry.ViewModels;

public class GroupAlertViewModel : ViewModelBase
{
    private readonly IGroupAlertService _service;
    private readonly CustomSpinnerViewModel _spinner;
    private readonly DialogService _dialogService;
    private readonly NotificationService _notificationService;

    public GroupAlertViewModel(
        IGroupAlertService service,
        CustomSpinnerViewModel spinner,
        DialogService dialogService,
        NotificationService notificationService)
    {
        _service = service;
        _spinner = spinner;
        _dialogService = dialogService;
        _notificationService = notificationService;
    }
    public GroupAlertContainerModel GroupAlertContainer { get; set; } = new();

    public void AddUpdateDetail(GroupAlertDetailModel detail)
    {
        GroupAlertContainer.GroupAlert.AddUpdateDetail(detail);
        Notify("add");
    }

    public void DeleteDetail(GroupAlertDetailModel detail)
    {
        GroupAlertContainer.GroupAlert.RemoveDetail(detail);
        Notify("delete");
    }


    public async Task LoadAsync(string? id)
    {
        bool isPostBack = false;
        if (GroupAlertContainer.AssetServices.Count() > 0)
        {
            isPostBack = true;
        }
        if (string.IsNullOrEmpty(id))
        {
            if (!isPostBack)
                GroupAlertContainer = await _service.New(isPostBack);
            else
                GroupAlertContainer.GroupAlert = new();
        }
        else
        {
            var result = await _service.Edit(id, isPostBack);
            if (isPostBack)
            {
                GroupAlertContainer.GroupAlert = result.GroupAlert;
            }
            else
            {
                GroupAlertContainer = result;
            }
        }
        Notify("Load");
    }

    public async Task Save()
    {
        try
        {
            _spinner.Loading = true;
            await _service.CreateUpdate(GroupAlertContainer.GroupAlert);
            _spinner.Loading = false;
            _notificationService.Notify(NotificationSeverity.Success, detail: "Successfully save");
            Notify("Save");
        }
        catch (Exception ex)
        {
            _notificationService.Notify(NotificationSeverity.Error, detail: ex.Message);
        }

    }
}
