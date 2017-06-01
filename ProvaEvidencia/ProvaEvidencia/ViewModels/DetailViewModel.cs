using System;
using ProvaEvidencia.ViewModels.Base;
using ProvaEvidencia.Views;
using Xamarin.Forms;

namespace ProvaEvidencia.ViewModels
{
    public class DetailViewModel: BaseViewModel
    {
        public Command ShowCategoryCommand { get;}
        public Command ShowAboutCommand { get; }

        public DetailViewModel()
        {
            Title = "Pagina Inicial";

            ShowCategoryCommand = new Command(ExecuteShowCategoryCommand);
            ShowAboutCommand = new Command(ExecuteShowAboutCommand);
        }

        private async void ExecuteShowAboutCommand(object obj)
        {
            await PushAsync<AboutViewModel, AboutPage>();
        }

        private async void ExecuteShowCategoryCommand(object obj)
        {
             await PushAsync<CategoriaViewModel, CategoriaPage>();          
        }
    }
}
