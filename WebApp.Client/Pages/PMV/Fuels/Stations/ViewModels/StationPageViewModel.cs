using Radzen;
using WebApp.Client.Pages.PMV.Fuels.Stations.Data;
using WebApp.Client.Pages.PMV.Fuels.Stations.Models;
using WebApp.UILibrary.Commons;
using WebApp.UILibrary.Components.Common.Spinners;


namespace WebApp.Client.Pages.PMV.Fuels.Stations.ViewModels
{
    public class StationPageViewModel : ViewModelBase
    {
        private readonly IStationService _stationService;
        private readonly NotificationService _notificationService;
        private readonly DialogService _dialogService;
        private readonly CustomSpinnerViewModel _spinner;

        public StationPageViewModel(
            IStationService stationService,
            NotificationService notificationService,
            DialogService dialogService,
            CustomSpinnerViewModel spinner)
        {
            _stationService = stationService;
            _notificationService = notificationService;
            _dialogService = dialogService;
            _spinner = spinner;
        }

        public FuelStationContainerModel Container { get; set; } = new();

        public FuelStationModel Station { get; set; } = new();

        public void Clear()
        {
            Station = new();
            Notify("Clear");
        }

        public async Task Add()
        {
            try
            {

                var confirm = await _dialogService.Confirm("Do you want to save?");
                if (confirm.Value == true)
                {
                    _spinner.Loading = true;
                    var result = await _stationService.CreateUpdateStation(Station);
                    IList<FuelStationModel> stations = Container.FuelStationList.ToList();
                    stations.Add(result);
                    Container.FuelStationList = stations.ToList();
                    _spinner.Loading = false;
                    Notify("Add");
                }
            }
            catch (Exception ex)
            {
                _spinner.Loading = false;
                _notificationService.Notify(NotificationSeverity.Error, summary: ex.Message);
            }
        }



        public async Task Load()
        {
            try
            {
                _spinner.Loading = true;
                Container = await _stationService.GetStations();
                _spinner.Loading = false;

                Notify("load");

            }
            catch (Exception ex)
            {
                _spinner.Loading = false;
                _notificationService.Notify(NotificationSeverity.Error, summary: ex.Message);
            }
        }

        public async Task CreateUpdate(FuelStationModel fuelStationModel)
        {
            try
            {

                var confirm = await _dialogService.Confirm("Do you want to save?");
                if (confirm.Value == true)
                {
                    _spinner.Loading = true;
                    await _stationService.CreateUpdateStation(fuelStationModel);
                    _spinner.Loading = false;
                    Notify("CreateUpdate");
                }
            }
            catch (Exception ex)
            {
                _spinner.Loading = false;
                _notificationService.Notify(NotificationSeverity.Error, summary: ex.Message);
            }
        }
    }
}
