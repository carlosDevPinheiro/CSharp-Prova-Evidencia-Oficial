using ProvaEvidencia.Models;
using ProvaEvidencia.ViewModels;
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

            Title = ViewModel.Title;
            ViewModel.PropertyChanged += TitlePropertyChanged;
            await ViewModel.LoadAsync();
        }

        private void TitlePropertyChanged(object sender, PropertyChangedEventArgs e)
        {

            if (e.PropertyName != nameof(ViewModel.Title)) return;

            Title = ViewModel.Title;
        }

        public void ListView_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var cat = (sender as ListView)?.SelectedItem as Categoria;
            (BindingContext as CategoriaViewModel)?.SelectCategoryCommand.Execute(cat);
        }
    }
}
