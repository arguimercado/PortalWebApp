using Radzen;
using WebApp.Client.Pages.PMV.Assets.Data;
using WebApp.Client.Pages.PMV.Assets.Models;
using WebApp.UILibrary.Commons;
using WebApp.UILibrary.Components.Common.Spinners;

namespace WebApp.Client.Pages.PMV.Assets.ViewModels
{
    public class AssignedDriverViewModel : ViewModelBase
    {
        private readonly IAssetService assetService;
        private readonly CustomSpinnerViewModel spinner;
        private readonly NotificationService notificationService;


        public AssignedDriverViewModel(IAssetService assetService, CustomSpinnerViewModel spinner, NotificationService notificationService)
        {
            this.assetService = assetService;
            this.spinner = spinner;
            this.notificationService = notificationService;

        }

        public IEnumerable<AssignedDriver> Drivers { get; set; } = new List<AssignedDriver>();

        public AssignedDriver Driver { get; set; } = new();


        public void EditDriver(AssignedDriver assignedDriver)
        {
            Clear();
            Driver = assignedDriver;
            if (string.IsNullOrEmpty(assignedDriver.EmpType))
            {
                Driver.EmpType = "employee";
            }
            Notify("Edit");
        }

        public void Clear()
        {
            Driver = new();
            Notify("Clear");
        }

        public async Task AddDriver(string assetCode, string assetType)
        {
            try
            {

                Driver.AssetCode = assetCode;
                spinner.Loading = true;

                await assetService.UpdateAssignedDriver(Driver, assetType);
                spinner.Loading = false;
                notificationService.Notify(NotificationSeverity.Success, "Successfully Save");
                Notify("Add");
            }
            catch (Exception ex)
            {
                spinner.Loading = false;
                notificationService.Notify(NotificationSeverity.Error, ex.Message);
            }
        }

        public async Task DeleteDriver(AssignedDriver assignedDriver)
        {
            try
            {
                spinner.Loading = true;
                await assetService.DeleteAssignedDriver(assignedDriver.Id);
                spinner.Loading = false;
            }
            catch (Exception ex)
            {
                spinner.Loading = false;
                notificationService.Notify(NotificationSeverity.Error, ex.Message);
            }
        }

        public async Task<IEnumerable<AssignedDriver>> GetDrivers(string assetCode)
        {
            return await assetService.GetAllAssignedDrivers(assetCode);
        }


    }
}
