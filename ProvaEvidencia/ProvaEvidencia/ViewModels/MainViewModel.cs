using ProvaEvidencia.Services;
using ProvaEvidencia.ViewModels.Base;
using ProvaEvidencia.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ProvaEvidencia.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public string Welcome { get; set; } = "Bem Vindo ";
        public string RazaoSocial { get; set; } = "Lafan Cabelo & Cia" + "₢";

        private string _usuarioId = "Vazio";

        public string UsuarioId
        {
            get { return _usuarioId; }
            set { SetProperty(ref _usuarioId, value); }
        }





        public Command MostrarViewCategoriaCommand { get; }

        private readonly AzureService AzureLogin;
        public MainViewModel()
        {
            Title = "Inicial";
            MostrarViewCategoriaCommand = new Command(ExecuteMostrarViewCategoriaCommad);

            AzureLogin = new AzureService();

        }




        private async void ExecuteMostrarViewCategoriaCommad(object obj)
        {
            //var user = await AzureLogin.LoginAsync();

            //if (user != null)
            //{
            //    UsuarioId = $"Bem Vindo {user.UserId}";
               
            //}

            //else
            //    UsuarioId = "Falha no Login, Tente Novamente";

            await PushAsync<CategoriaViewModel, CategoriaPage>(AzureLogin);
        }
    }
}
