using ProvaEvidencia.Models;
using ProvaEvidencia.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ProvaEvidencia.ViewModels
{
    public class NewCategoriaViewModel : BaseViewModel
    {
        

        private string _name;
        private string _phone;
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        public string Detalhes
        {
            get { return _phone; }
            set { SetProperty(ref _phone, value); }
        }

        public Command AddCategoriaCommand { get; }


        private CategoriaItemManager _service;
        public NewCategoriaViewModel(CategoriaItemManager service)
        {
            _service = service;
            Name = "Digite Nome da Categoria";
            Detalhes = "Digite Detalhes de Categoria ";
            
            AddCategoriaCommand = new Command(AddCategoria);
        }

        private async void AddCategoria(object obj)
        {
            var categoria = new Categoria
            {
                Name = Name,
                Image = "https://s3-sa-east-1.amazonaws.com/brandsite-prod-gzip/user/Upload/Category/banner-categoria-cabelos_20140726101450968.jpg",
                DetalhesCategoria = Detalhes,
                Done = true
                 
            };

            await _service.SaveTaskAsync(categoria);
        }

       
    }
}
