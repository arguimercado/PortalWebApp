using Radzen;
using WebApp.Client.Pages.PMV.Assets.Data;
using WebApp.Client.Pages.PMV.Assets.Models;
using WebApp.UILibrary.Commons;
using WebApp.UILibrary.Components.Common.Spinners;

namespace WebApp.Client.Pages.PMV.Assets.Components.Entry.ViewModels;

public class AssetDocumentViewModel : ViewModelBase
{
    private readonly DialogService _dialogService;
    private readonly CustomSpinnerViewModel _spinner;
    private readonly IAssetService _assetService;
    private readonly NotificationService _notification;

    public AssetDocumentViewModel(DialogService dialogService,
        CustomSpinnerViewModel spinner, IAssetService assetService, NotificationService notification)
    {
        _dialogService = dialogService;
        _spinner = spinner;
        _assetService = assetService;
        _notification = notification;
    }

    public InternalAssetModel Asset { get; set; } = new();
    public List<AssetDocumentModel> Documents => Asset.Documents;

    public AssetDocumentModel CreateNew(int assetId)
    {
        return new AssetDocumentModel
        {
            Id = Guid.NewGuid().ToString(),
            AssetId = assetId,
        };
    }

    public async Task Update(AssetDocumentModel newDoc)
    {
        try
        {
            _spinner.Loading = true;

            await _assetService.UploadDocument(newDoc);
            var doc = Documents.FirstOrDefault(d => d.Id == newDoc.Id);

            if (doc is null)
            {
                Documents.Add(newDoc);
            }
            else
            {
                Documents.Remove(doc);
                Documents.Add(newDoc);
            }
            _notification.Notify(NotificationSeverity.Success, summary: "Successfully Save!");
            _spinner.Loading = false;
            Notify("Update");

        }
        catch (Exception ex)
        {
            _spinner.Loading = false;
            _notification.Notify(NotificationSeverity.Error, summary: $"Error: {ex.Message}");
        }
    }

    public async Task Delete(AssetDocumentModel newDoc)
    {
        var result = await _dialogService.Confirm(message: "Are you sure you want to delete this record?");
        if (result.Value)
        {
            _spinner.Loading = true;
            var doc = Documents.FirstOrDefault(d => d.Id == newDoc.Id);
            if (doc is not null)
            {
                Documents.Remove(doc);
            }
            _spinner.Loading = false;
        }

        Notify("Delete");
    }

}
