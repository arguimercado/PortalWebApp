using WebApp.UILibrary.Commons;

namespace WebApp.UILibrary.Components.Common.Spinners;

public class CustomSpinnerViewModel : ViewModelBase
{
    private bool _loading = false;
    public bool Loading
    {
        get => _loading;
        set
        {
            _loading = value;
            Notify("Loading");
        }
    }

}
