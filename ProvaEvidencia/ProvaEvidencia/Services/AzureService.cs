using Microsoft.WindowsAzure.MobileServices;
using ProvaEvidencia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ProvaEvidencia.Services
{
    public class AzureService
    {
        static readonly string AppUrl = "http://provaevidencia.azurewebsites.net/";

        public MobileServiceClient Client { get; set; } = null;

        public void Initialize()
        {
            Client = new MobileServiceClient(AppUrl);
        }


        public async Task<MobileServiceUser> LoginAsync()
        {
            Initialize();

            var auth = DependencyService.Get<IAuthenticate>();
            var user = await auth.Authenticate(Client, MobileServiceAuthenticationProvider.Facebook);
            var appServiceIdentities = await Client.InvokeApiAsync<List<AppServiceIdentity>>("/.auth/me");

            GetUserInformationAsync();

            if (user == null)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await App.Current.MainPage.DisplayAlert("Ops", "Não Conseguimos Efetuar O login", "Ok");
                });
            }

            return user;
        }

        public async void GetUserInformationAsync()
        {
            var appServiceIdentities = await Client.InvokeApiAsync<List<AppServiceIdentity>>("/.auth/me");

            if (appServiceIdentities.Count <= 0)
                return;

            var appServiceIdentity = appServiceIdentities[0];

            var providerUserId = string.Empty;
            var email = string.Empty;
            var name = string.Empty;

            foreach (var userClaim in appServiceIdentities[0].UserClaims)
            {
                var item = userClaim.Type.Split('/').Last();
                switch (item)
                {
                    case "nameidentifier":
                        providerUserId = userClaim.Value;
                        break;

                    case "emailaddress":
                        email = userClaim.Value;
                        break;

                    case "name":
                        name = userClaim.Value;
                        break;
                }
            }

            var usuario = new Usuarios
            {
                ID = providerUserId,
                Name = name,
                Email = email,
                Photo = "http://graph.facebook.com/" + providerUserId + "/picture?type=large"
            };

            Settings.Email = usuario.Email;
            Settings.NameUsuario = usuario.Name;
            Settings.Image = usuario.Photo;
        }
    }
}


