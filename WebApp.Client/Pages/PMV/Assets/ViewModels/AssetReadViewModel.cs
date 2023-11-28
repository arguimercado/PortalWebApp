using Radzen;
using WebApp.Client.Pages.PMV.Assets.Data;
using WebApp.Client.Pages.PMV.Assets.Models;
using WebApp.UILibrary.Commons;
using WebApp.UILibrary.Components.Common.Spinners;

namespace WebApp.Client.Pages.PMV.Assets.ViewModels;

public class AssetReadViewModel : ViewModelBase
{
    private readonly IAssetService _service;
    private readonly CustomSpinnerViewModel _spinner;
    private readonly DialogService _dialogService;

    public AssetReadViewModel(IAssetService service, CustomSpinnerViewModel spinner, DialogService dialogService)
    {
        _service = service;
        _spinner = spinner;
        _dialogService = dialogService;
    }

    public AssetReadModel Asset { get; set; } = new();


    public async Task Load(int id)
    {
        try
        {
            _spinner.Loading = true;
            Asset = await _service.ViewAsset(id) ?? new AssetReadModel();
            if(Asset!.Documents == null)
            {
                Asset.Documents = new List<AssetDocumentModel>();
            }
            Notify("Load");

            _spinner.Loading = false;
        }
        catch (Exception ex)
        {
            await _dialogService.Alert(ex.Message);
            _spinner.Loading = false;
        }
    }

   
}
