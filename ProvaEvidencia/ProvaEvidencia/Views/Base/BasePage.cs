using ProvaEvidencia.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ProvaEvidencia.Views.Base
{
    public abstract class BasePage : ContentPage
    {
        private BaseViewModel ViewModel => BindingContext as BaseViewModel;

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            Title = ViewModel.TituloPagina;
            ViewModel.PropertyChanged += TitlePropertyChanged;
            await ViewModel.LoadAsync();
        }

        private void TitlePropertyChanged(object sender, PropertyChangedEventArgs e)
        {

            if (e.PropertyName != nameof(ViewModel.TituloPagina)) return;

            Title = ViewModel.TituloPagina;
        }
    }
}
