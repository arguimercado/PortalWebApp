using Radzen;
using WebApp.Client.Pages.PMV.PMS.GroupAlerts.Data;
using WebApp.Client.Pages.PMV.PMS.GroupAlerts.Models;
using WebApp.UILibrary.Commons;
using WebApp.UILibrary.Components.Common.Spinners;

namespace WebApp.Client.Pages.PMV.PMS.GroupAlerts.Components.Entry.ViewModels;

public class ServiceAlertViewModel : ViewModelBase
{
    private readonly IGroupAlertService _service;
    private readonly CustomSpinnerViewModel _spinner;
    private readonly NotificationService _notificationService;

    public ServiceAlertModel ServiceAlert { get; set; } = new();
    public IEnumerable<ServiceAlertModel>? ServiceAlerts { get; set; } = new List<ServiceAlertModel>();


    public ServiceAlertViewModel(IGroupAlertService service,
        CustomSpinnerViewModel spinner, NotificationService notificationService)
    {

        _service = service;
        _spinner = spinner;
        _notificationService = notificationService;
    }


    public async Task Load()
    {
        try
        {
            _spinner.Loading = true;
            ServiceAlerts = await _service.GetServiceAlerts();
            _spinner.Loading = false;

        }
        catch (Exception ex)
        {
            _spinner.Loading = false;
            _notificationService.Notify(NotificationSeverity.Error, ex.Message);
        }
    }

    public async Task Save(Action<ServiceAlertModel> callback)
    {

        try
        {
            _spinner.Loading = true;
            await _service.CreateServiceAlert(ServiceAlert);
            _spinner.Loading = false;
            callback(ServiceAlert);
            Notify("Save");
        }
        catch (Exception ex)
        {
            _spinner.Loading = false;
            _notificationService.Notify(NotificationSeverity.Error, ex.Message);
        }
    }

}
