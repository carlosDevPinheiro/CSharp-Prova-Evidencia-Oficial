using ProvaEvidencia.Models;
using ProvaEvidencia.Services;
using ProvaEvidencia.ViewModels.Base;
using ProvaEvidencia.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ProvaEvidencia.ViewModels
{
    public class CategoriaViewModel : BaseViewModel
    {
        private string _email;

        public string Email
        {
            get { return _email; }
            set { SetProperty(ref _email, value); }
        }

        private string _name;

        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        private string _image;

        public string Photo
        {
            get { return _image; }
            set { SetProperty(ref _image, value); }
        }


        private CategoriaItemManager service;
        private AzureService _azureLogin;
        public ObservableCollection<Categoria> Resultados { get; private set; }

        public Command NewUsuarioCommand { get; }

        public CategoriaViewModel(AzureService AzureLogin)
        {
            service = new CategoriaItemManager();
            _azureLogin = AzureLogin;
            Resultados = new ObservableCollection<Categoria>();

             NewUsuarioCommand = new Command(ExecuteNewUsuarioCommand);

            Name = "Name Vazio";
            Email = "Vazio";
            Photo = "vazio";
        }

        private async void ExecuteNewUsuarioCommand(object obj)
        {
            await PushAsync<NewCategoriaViewModel, NewCategoriaPage>(service);
        }

        public async Task<ObservableCollection<Categoria>> ListUsuario()
        {

            var categorias = await service.todoTable.ToListAsync();          

            Name = Settings.NameUsuario;
            Email = Settings.Email;
            Photo = Settings.Image;


            Resultados.Clear();
            foreach (var item in categorias)
            {
                Resultados.Add(item);
            }

            return Resultados;
        }

        public async override Task LoadAsync()
        {
            await ListUsuario();
        }

    }
}
