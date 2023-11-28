using Radzen;
using WebApp.Client.Pages.PMV.Assets.Data;
using WebApp.Client.Pages.PMV.Assets.Models;
using WebApp.UILibrary.Commons;
using WebApp.UILibrary.Components.Common.Spinners;

namespace WebApp.Client.Pages.PMV.Assets.ViewModels;

public class AssetViewModel : ViewModelBase
{

    private readonly IAssetService _service;
    private readonly DialogService _dialogService;
    private readonly NotificationService _notificationService;
    private readonly CustomSpinnerViewModel _spinner;

    public AssetViewModel(IAssetService service,
        DialogService dialogService,
        NotificationService notificationService,
        CustomSpinnerViewModel spinner)
    {
        _service = service;
        _dialogService = dialogService;
        _notificationService = notificationService;
        _spinner = spinner;
    }

    public AssetContainerModel AssetContainer { get; set; } = new();

    public InternalAssetModel Asset => AssetContainer.InternalAsset;


    public IEnumerable<OperatorDriverModel> GetDrivers()
    {
        return AssetContainer.InternalAsset.Drivers;
    }

    public async Task NewEditAsync(string? assetCode, string type)
    {
        try
        {
            Func<string, bool> CheckAssetType = (assetType) =>
            {
                return assetType switch
                {
                    "internal" => AssetContainer.Categories.Count() > 0,
                    _ => AssetContainer.PlateTypes.Count() > 0
                };
            };

            _spinner.Loading = true;
            bool isPostback = CheckAssetType(type);

            if (!string.IsNullOrEmpty(assetCode))
            {
                AssetContainerModel? result = null;
                if (type == "internal")
                {
                    result = await _service.GetAsset(assetCode, "code", type, isPostback);
                    if (isPostback)
                        AssetContainer.InternalAsset = result!.InternalAsset;
                    else
                        AssetContainer = result;
                }
                else
                {
                    var curId = string.IsNullOrEmpty(assetCode) ? "" : assetCode;
                    result = await _service.GetAsset(assetCode, "code", type, isPostback);
                    if (isPostback)
                        AssetContainer.ExternalAsset = result!.ExternalAsset;
                    else
                        AssetContainer = result!;
                }
            }
            else
            {
                var result = await _service.CreateNew(type, isPostback);
                AssetContainer = result!;
                AssetContainer.InternalAsset = new();
                AssetContainer.ExternalAsset = new();
            }

            Notify("NewEdit");
            _spinner.Loading = false;
        }
        catch (Exception ex)
        {
            _spinner.Loading = false;
            _notificationService.Notify(NotificationSeverity.Error, detail: ex.Message);
        }
    }

    public void Clear(string type)
    {
        if (type == "internal")
        {
            AssetContainer.InternalAsset = new();
        }
        else
        {
            AssetContainer.ExternalAsset = new();
        }
    }

    public async Task<bool> SaveAsync(string type)
    {
        try
        {
            var result = await _dialogService.Confirm("Do you want to save?", "Save Dialog");
            if (result.Value)
            {
                _spinner.Loading = true;
                var newEntryContainer = AssetContainerModel.Create();

                if (type == "internal")
                {

                    //validate


                    newEntryContainer.InternalAsset = AssetContainerModel
                                                        .CreateInternal(AssetContainer.InternalAsset);
                }
                else
                {
                    newEntryContainer.ExternalAsset = AssetContainer.ExternalAsset;
                }

                await _service.Save(newEntryContainer, type);

                _spinner.Loading = false;
                _notificationService.Notify(NotificationSeverity.Success, "Save successfully");
                Notify("Save");

                return true;
            }
        }
        catch (Exception ex)
        {
            _spinner.Loading = false;
            _notificationService.Notify(NotificationSeverity.Error, ex.Message);
        }

        return false;
    }

    public AssetDocumentModel CreateNew(int assetId)
    {
        return new AssetDocumentModel
        {
            Id = Guid.NewGuid().ToString(),
            AssetId = assetId,
        };
    }

    public async Task UpdateDocument(AssetDocumentModel newDoc)
    {
        try
        {

            _spinner.Loading = true;
            newDoc.AssetId = Asset.SlNo;
            var returnId = await _service.UploadDocument(newDoc);
            if (returnId != null)
            {
                var doc = Asset.Documents.FirstOrDefault(d => d.Id == returnId.Id);
                if (doc == null)
                {
                    Asset.Documents.Add(returnId);
                }
            }

            _notificationService.Notify(NotificationSeverity.Success, summary: "Successfully Save!");
            _spinner.Loading = false;
            Notify("Update");

        }
        catch (Exception ex)
        {
            _spinner.Loading = false;
            _notificationService.Notify(NotificationSeverity.Error, summary: $"Error: {ex.Message}");
        }
    }

    public async Task DeleteDocument(AssetDocumentModel newDoc)
    {

        try
        {
            var result = await _dialogService.Confirm(message: "Are you sure you want to delete this record?");
            if (result.Value)
            {
                _spinner.Loading = true;
                var doc = Asset.Documents.FirstOrDefault(d => d.Id == newDoc.Id);
                if (doc is not null)
                {
                    await _service.DeleteDocument(doc.Id);
                    Asset.Documents.Remove(doc);
                }
                _spinner.Loading = false;
            }
            _notificationService.Notify(NotificationSeverity.Success, "Successfully Deleted");
            Notify("Delete");

        }
        catch (Exception ex)
        {
            _spinner.Loading = false;
            _notificationService.Notify(NotificationSeverity.Error, summary: $"Error: {ex.Message}");
        }
    }

    public async Task GetAssetNo(string subCategory)
    {
        Asset.AssetCode = await _service.GetAssetNo(subCategory);
        Notify("GetAssetNo");
    }
}
