using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Radzen;
using Radzen.Blazor;
using WebApp.Client.Pages.PMV.Assets.ViewModels;
using WebApp.UILibrary.Commons;

namespace WebApp.Client.Pages.PMV.Assets.Components.Manage.List;

public partial class List 
{

    [Inject]
    public DialogService DialogService { get; set; }

    [Parameter]
    public string? Status { get; set; } = "";

    [Parameter]
    public string? Category { get; set; } = "";

    [Parameter]
    public string? Type { get; set; } = "";

    [Parameter]
    public string View { get; set; } = "";

    [CascadingParameter]
    public required Task<AuthenticationState> AuthState { get; set; }
    
    public int SelectedIndex { get; set; } = 0;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {

            if (!string.IsNullOrEmpty(Status))
            {
                if (Type == "external")
                {
                    Model.FilterAsset.PlateType = Category;
                }
                else
                {
                    Model.FilterAsset.Status = Status;
                    Model.FilterAsset.Category = Category;
                }

                Model.FilterAsset.AssetType = Type ?? "internal";
                Model.FilterAsset.IsRefresh = true;
                Model.FilterAsset.IsPostBack = true;
                SelectedIndex = Type == "external" ? 1 : 0;

                await Model.Filter();
            }
            else
            {
                //initialize when first render
                if (Model.AssetListContainer.Categories.Count <= 0)
                {
                    await Model.Initialize();
                }
            }
        }
    }

    private void HandleAction(RadzenSplitButtonItem item)
    {
        if (item.Value is not null)
        {
            var navigate = item.Value;
            PageNavigation.NavigateTo(navigate);

        }
    }

    private async Task HandleExportClick()
    {
        var result = await DialogService.OpenAsync<AssetExportDialog>("Export Excel",
            new Dictionary<string, object>() { { "Filter", Model.FilterAsset } });

        //Model.ExportToExcel();
    }
}
