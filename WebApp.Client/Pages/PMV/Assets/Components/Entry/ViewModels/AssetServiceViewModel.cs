using Radzen;
using WebApp.Client.Pages.PMV.Assets.Data;
using WebApp.Client.Pages.PMV.Assets.Models;
using WebApp.UILibrary.Commons;
using WebApp.UILibrary.Components.Common.Spinners;

namespace WebApp.Client.Pages.PMV.Assets.Components.Entry.ViewModels
{
    public class AssetServiceViewModel : ViewModelBase
    {
        private readonly IAssetService _service;
        private readonly DialogService _dialogService;
        private readonly NotificationService _notificationService;
        private readonly CustomSpinnerViewModel _spinner;

        public AssetServiceViewModel(IAssetService service,
            CustomSpinnerViewModel spinner,
            NotificationService notificationService,
            DialogService dialogService)
        {
            _service = service;
            _dialogService = dialogService;
            _notificationService = notificationService;
            _spinner = spinner;
        }

        public ICollection<ServiceDueEntryModel> ServiceDues { get; set; } = new List<ServiceDueEntryModel>();

        public ServiceDueEntryModel Entry { get; set; } = new();

        public string ServiceDueId { get; set; }


        public async Task CreateServiceDue(int slNo)
        {
            try
            {
                _spinner.Loading = true;
                var result = await _service.CreateServiceDue(slNo, ServiceDueId);
                ServiceDues = result.ToList();
                _spinner.Loading = false;
                _notificationService.Notify(NotificationSeverity.Success, "Update Successfull");
                Notify("Update");
            }
            catch (Exception ex)
            {
                _spinner.Loading = false;
                _notificationService.Notify(NotificationSeverity.Error, ex.Message);
            }
        }

        public async Task<bool> UpdateServiceDue(int assetId, ServiceDueEntryModel due)
        {
            try
            {
                var confirm = await _dialogService.Confirm("Do you want to update this service?", "Update Dialog");
                if (confirm.Value)
                {

                    var selected = ServiceDues.FirstOrDefault(d => d.Id == due.Id);
                    if (selected != null)
                    {
                        _spinner.Loading = true;
                        selected.MarkDeleted = false;
                        if (due.LastServiceDate.HasValue)
                        {
                            selected.LastServiceDate = due.LastServiceDate;
                        }
                        selected.LastSMUReading = due.LastSMUReading;
                        await _service.UpdateServiceDue(assetId, due);
                        selected.Recalculate();
                        _spinner.Loading = false;

                        Notify("Update");

                        return true;
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                _spinner.Loading = false;
                _notificationService.Notify(NotificationSeverity.Error, ex.Message);
                return false;
            }
        }

        public async Task DeleteServiceDue(ServiceDueEntryModel due)
        {
            try
            {
                var confirm = await _dialogService.Confirm("Do you want to delete this service?", "Delete Dialog");
                if (confirm.Value)
                {
                    var selected = ServiceDues.FirstOrDefault(d => d.Id == due.Id);
                    if (selected != null)
                    {

                        selected.MarkDeleted = true;
                        _spinner.Loading = true;
                        await _service.DeleteServiceDue(selected.Id);
                        ServiceDues.Remove(selected);
                        _spinner.Loading = false;
                        Notify("Delete");
                    }
                }
            }
            catch (Exception ex)
            {
                _spinner.Loading = false;
                _notificationService.Notify(NotificationSeverity.Error, ex.Message);
            }

        }
    }
}
