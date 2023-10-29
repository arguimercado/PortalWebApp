using Microsoft.AspNetCore.Components;

namespace WebApp.UILibrary.Components.Navigation;

public class PageNavigation
{
    private readonly NavigationManager _navigation;
    private List<string> _previousPages;

    private void ManualMenu()
    {
        
    }

    public PageNavigation(NavigationManager navigation)
    {
        _navigation = navigation;
       
        _previousPages = new List<string>();
    }


    


   
   
    public string CurrentUri()
    {
        return _navigation.Uri;
    }

    public void Reload()
    {
        _navigation.NavigateTo(_navigation.Uri, true);
    }

    public void AddPageHistory(string pageName = "")
    {
        if (string.IsNullOrEmpty(pageName))
            pageName = CurrentUri();

        if (!_previousPages.Any(p => p == pageName))
        {
            _previousPages.Add(pageName);
        }
    }

    public bool IsPreviousPage()
    {
        return _previousPages.Any(p => p == CurrentUri());
    }

    public void DirectNavigate(string navigate)
    {
        _navigation.NavigateTo(navigate);
    }

    public bool CanGoBack()
    {
        return _previousPages.Count > 1;
    }
    public void NavigateBack()
    {
        if (_previousPages.Count > 1)
        {
            _previousPages.RemoveAt(_previousPages.Count - 1);
            var page = _previousPages.LastOrDefault();
            if (!string.IsNullOrEmpty(page))
                _navigation.NavigateTo(page);
        }

    }

    public void NavigateTo(string title, string parameter = null, string key = "", bool forceLoad = false, bool replace = false)
    {
        var path = "";
        if (!string.IsNullOrEmpty(parameter))
            path = $"{title}/{parameter}";
        else
            path = title;


        _navigation.NavigateTo(path, forceLoad, replace);

    }
}