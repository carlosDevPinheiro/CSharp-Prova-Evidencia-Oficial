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

        private CategoriaItemManager service;
        private AzureService _azureLogin;
        public ObservableCollection<Categoria> Resultados { get; private set; }

        public Command<Categoria> SelectCategoryCommand { get; }

        public CategoriaViewModel()
        {
            Title = "Selecione Categoria";

            service = new CategoriaItemManager();
            _azureLogin = new AzureService();
            Resultados = new ObservableCollection<Categoria>();

            SelectCategoryCommand = new Command<Categoria>(ExecuteSelectCategoryCommand);
        }        

        private async void ExecuteSelectCategoryCommand(Categoria cat)
        {
            await PushAsync<ItemCategoriaViewModel, ItemCategoryPage>(cat);
        }

        public async Task<ObservableCollection<Categoria>> ListCategories()
        {

            var categories = await service.todoTable.ToListAsync();

            Resultados.Clear();
            foreach (var item in categories.OrderBy(x => x.Name))
            {
                Resultados.Add(item);
            }

            return Resultados;
        }

        public async override Task LoadAsync()
        {
            await ListCategories();
        }

    }
}
