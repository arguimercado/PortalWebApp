using Microsoft.AspNetCore.Components;
using WebApp.UILibrary.Components.Navigation;

namespace WebApp.UILibrary.Commons;

public class ComponentViewModelBase<TModel> : ComponentBase, IDisposable
    where TModel : ViewModelBase
{
   
    protected bool IsFirstLoad = true;
    
    protected bool AuthenticateRequire = true;

    [Inject]
    public required TModel Model { get; set; }

    [Inject]
    public required PageNavigation PageNavigation { get; set; }

    


    public void Dispose()
    {
        Model.PropertyChanged -= HandleChanged;
    }

    protected override void OnInitialized()
    {

        Model.PropertyChanged += HandleChanged;
        if (!PageNavigation.IsPreviousPage())
        {
            PageNavigation.AddPageHistory();
            IsFirstLoad = true;
        }
        else
        {
            IsFirstLoad = false;
        }
        OnAfterPropertyChanged();
    }

    protected void HandleChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        InvokeAsync(StateHasChanged);
    }

    protected void OnAfterPropertyChanged() { }

}
