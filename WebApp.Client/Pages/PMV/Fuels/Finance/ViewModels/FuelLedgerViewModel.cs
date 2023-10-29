using Portal.WebClient.Pages.Fuels.Finance.Data;
using Radzen;
using WebApp.Client.Pages.PMV.Fuels.Finance.Models;
using WebApp.UILibrary.Commons;
using WebApp.UILibrary.Components.Common.Spinners;

namespace Portal.WebClient.Pages.Fuels.Finance.ViewModels;

public class FuelLedgerViewModel : ViewModelBase
{
    private readonly IFuelLedgerService _fuelLedgerService;
    private readonly DialogService _dialogService;
    private readonly CustomSpinnerViewModel _spinner;

    public IEnumerable<TankerStockContainer> ContainerList { get; set; } = new List<TankerStockContainer>();

    public FuelLedgerViewModel(IFuelLedgerService fuelLedgerService,DialogService dialogService,CustomSpinnerViewModel spinner)
    {
        _fuelLedgerService = fuelLedgerService;
        _dialogService = dialogService;
        _spinner = spinner;
    }

    public int Month { get; set; } = DateTime.Now.Month;
    public int Year { get; set; } = DateTime.Now.Year;

    public bool ForcePost { get; set; } = false;

    public IEnumerable<SelectItem> GetMonths()
    {   
        var intMonth = DateTime.Now.Month;
        var items = new List<SelectItem>();

        for(var i = intMonth; i > 0; i--) {
            items.Add(new SelectItem { Value = i.ToString(), Text = new DateTime(2023, i, 1).ToString("MMMM") });
        }
        return items;
    }

    public void SetMonth(object args)
    {
       
        if(args is not null)
        {
            Month = int.Parse(args.ToString()!);
            Notify("Update");
        }
    }



    public async Task Extract()
    {
        try
        {
            //check if the month already exist
            var isExist = ContainerList.Any(c => c.Month == Month && c.Year == Year);
            var monthName = new DateTime(Year, Month, 1).ToString("MMMM");
            if(!isExist)
            {
                var confirmation = await _dialogService.Confirm($"Do you want to extract fuel for the month of {monthName}");
                if(confirmation.HasValue && confirmation.Value)
                {
                    _spinner.Loading = true;
                    var result = await _fuelLedgerService.Create(Month, Year, ForcePost);
                    _spinner.Loading = false;
                    Notify("Update");
                }

            }
            else
            {
                var confirmation = await _dialogService.Confirm($"the month of {monthName} is exist already? do you want to update?");
                if (confirmation.HasValue && confirmation.Value)
                {
                    _spinner.Loading = true;
                    var result = await _fuelLedgerService.Create(Month, Year,ForcePost);
                    _spinner.Loading = false;
                    Notify("Update");
                }
            }

        }
        catch(Exception ex)
        {
            _spinner.Loading = false;
            await _dialogService.Alert(ex.Message);
        }
    }

    public async Task Load()
    {
        try
        {
            _spinner.Loading = true;
            ContainerList = await _fuelLedgerService.Load();
            _spinner.Loading = false;
            Notify("Load");
        }
        catch (Exception ex)
        {
            _spinner.Loading = false;
            await _dialogService.Alert(ex.Message);

        }
    }

    public async Task Exports(int month,int year)
    {
        try {
            _spinner.Loading = true;
            await _fuelLedgerService.Export(month, year);
            _spinner.Loading = false;
        }
        catch (Exception ex) {

            _spinner.Loading = false;
            
        }
    }

}
