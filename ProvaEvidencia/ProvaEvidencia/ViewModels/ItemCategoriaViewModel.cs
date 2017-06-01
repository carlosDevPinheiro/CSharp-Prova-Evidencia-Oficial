using ProvaEvidencia.Models;
using ProvaEvidencia.ViewModels.Base;
using System.Threading.Tasks;

namespace ProvaEvidencia.ViewModels
{
    public class ItemCategoriaViewModel : BaseViewModel
    {
        public Categoria Category = null;

        private string _name;
        private string _photo;
        private string _description;

        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }
        public string Photo
        {
            get { return _photo; }
            set { SetProperty(ref _photo, value); }
        }
        public string Description
        {
            get { return _description; }
            set { SetProperty(ref _description, value); }
        }
        

        public ItemCategoriaViewModel(Categoria cat)
        {
            Title = "Categoria";

            Category = cat;
            Name = Category.Name;
            Photo = Category.Image;
            Description = Category.DetalhesCategoria;
            
        }       

    }
}
