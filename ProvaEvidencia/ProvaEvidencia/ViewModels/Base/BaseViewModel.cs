using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ProvaEvidencia.ViewModels.Base
{
    public class BaseViewModel: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _title;

        public string TituloPagina
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public virtual void OnPropertyChanged([CallerMemberName] string property = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        public bool SetProperty<T>(ref T store, T value, [CallerMemberName] string property = null)
        {
            if (EqualityComparer<T>.Default.Equals(store, value))
                return false;

            store = value;
            OnPropertyChanged(property);

            return true;
        }

        public async Task PushAsync<TViewModel, TPage>(params object[] paramts) where TViewModel : BaseViewModel where TPage : Page
        {
            var viewModelType = typeof(TViewModel);
            var ViewPage = typeof(TPage);

            var Page = Activator.CreateInstance(ViewPage) as Page;
            var ViewModel = Activator.CreateInstance(viewModelType, paramts);

            if (Page != null)
            {
                Page.BindingContext = ViewModel;
            }

            await Application.Current.MainPage.Navigation.PushAsync(Page);
        }

        public virtual async Task LoadAsync()
        {
            await Task.FromResult(0);
        }
    }
}
