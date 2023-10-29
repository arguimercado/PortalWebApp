using Radzen;
using WebApp.Client.Pages.PMV.PMS.GroupAlerts.Data;
using WebApp.Client.Pages.PMV.PMS.GroupAlerts.Models;
using WebApp.UILibrary.Commons;
using WebApp.UILibrary.Components.Common.Spinners;

namespace WebApp.Client.Pages.PMV.PMS.GroupAlerts.Components.Manage.ViewModels
{
    public class GroupAlertListViewModel : ViewModelBase
    {
        private readonly IGroupAlertService _service;
        private readonly CustomSpinnerViewModel _spinner;
        private readonly NotificationService _notificationService;

        public GroupAlertListViewModel(IGroupAlertService service,
            CustomSpinnerViewModel spinner, NotificationService notificationService)
        {
            _service = service;
            _spinner = spinner;
            _notificationService = notificationService;
        }

        public IEnumerable<GroupAlertModel> List { get; set; } = new List<GroupAlertModel>();

        public GroupAlertModel? SelectedItem { get; set; }
        public IEnumerable<SelectItem> Categories { get; set; } = new List<SelectItem>();
        public IEnumerable<SelectItem> SubCategories { get; set; } = new List<SelectItem>();
        public IEnumerable<SelectItem> GetSubCategories(string cid) => SubCategories.Where(s => s.Type == cid).ToList();
        public string SelectedCategory { get; set; } = "";
        public string SelectedSubCategory { get; set; } = "";
        public int ApplyTo { get; set; } = 0;
        public async Task Filter()
        {
            try
            {
                _spinner.Loading = true;
                var container = await _service.GetAll();
                List = container.GroupAlerts.ToList();
                Categories = container.Categories.ToList();
                SubCategories = container.SubCategories.ToList();
                _spinner.Loading = false;
                Notify("Filter");

            }
            catch (Exception ex)
            {
                _spinner.Loading = false;
                _notificationService.Notify(NotificationSeverity.Error, ex.Message);
            }
        }

        public async Task Apply()
        {
            try
            {

                if (SelectedItem == null)
                    return;

                IsLoading = true;
                await _service.ApplyToAsset(SelectedItem.Id, SelectedSubCategory, ApplyTo);
                IsLoading = false;
                _notificationService.Notify(NotificationSeverity.Success, "Successfully applied");
                Notify("Apply");
            }
            catch (Exception ex)
            {
                IsLoading = false;
                _notificationService.Notify(NotificationSeverity.Error, ex.Message);
            }
        }

        public void Select(string id)
        {
            SelectedItem = List.SingleOrDefault(g => g.Id == id);
        }
    }
}
