using System.ComponentModel;

namespace WebApp.UILibrary.Commons
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void Notify(string propertyName, object value = null)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private bool _isLoading;
        public bool IsLoading { get => _isLoading; set { _isLoading = value; Notify("IsLoading"); } }

        public virtual void OnInitialized()
        {

        }

        public virtual Task OnInitializeAsync()
        {
            return Task.CompletedTask;
        }

        
    }
}
