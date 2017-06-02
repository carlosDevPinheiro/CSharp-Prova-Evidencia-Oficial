using ProvaEvidencia.Services;
using ProvaEvidencia.ViewModels.Base;
using System.Threading.Tasks;
using Xamarin.Forms;
using System;

namespace ProvaEvidencia.ViewModels
{
    public class MasterViewModel : BaseViewModel
    {

        private string _userId;
        private string _name;
        private string _photo;
        private string _email;
        private string _login;

        public string UserId
        {
            get { return _userId; }
            set { SetProperty(ref _userId, value); }
        }
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name,value); }
        }  
        public string Photo
        {
            get { return _photo; }
            set {SetProperty(ref _photo,value); }
        }
        public string Email
        {
            get { return _email; }
            set { SetProperty(ref _email,value); }
        }

        public string Login
        {
            get { return _login; }
            set { SetProperty(ref _login, value); }
        }
        public Command LoginCommand { get; }
        public Command LogoutCommand { get; }
        public AzureService _azureLogin { get; set; }
        public MasterViewModel()
        {
            LoginCommand = new Command(ExecuteLoginCommand);
            LogoutCommand = new Command(ExecuteLogoutCommand);
            _azureLogin = new AzureService();

            UserId = string.Empty;
            Name = string.Empty;
            Email = string.Empty;
            Photo = string.Empty;
            Login = "Login";
        }

        private async  void ExecuteLogoutCommand(object obj)
        {
            var resp = await _azureLogin.LogoutAsync();

            if (!resp)
                return;

            UserId = string.Empty;
            Name = string.Empty;
            Email = string.Empty;
            Photo = string.Empty;
            Login = "Login";
        }

        private async void ExecuteLoginCommand(object obj)
        {
           
            var user =  await _azureLogin.LoginAsync()
                .ContinueWith(async t => {                      
                    await LoadAsync();                   
                });

            if (user != null)
            {
                Login = string.Empty;
                UserId = $"Bem Vindo {Settings.UserId}";
            }           
               
            else
               UserId = "Falha no Login, Tente Novamente";
        }

        public async override Task LoadAsync()
        {
            await Task.Run(() =>
             {                 
                 Name = $"{Settings.NameUsuario}";
                 Email = Settings.Email;
                 Photo = Settings.Image;
             });
        }


    }
}
