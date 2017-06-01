using Microsoft.WindowsAzure.MobileServices;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ProvaEvidencia.Services
{
    public class AzureService
    {
        public MobileServiceClient Client { get; set; } = null;
       

        public void Initialize()
        {
            Client = new MobileServiceClient(Constant.ApplicationURL);           
        }


        public async Task<MobileServiceUser> LoginAsync()
        {
            Initialize();

            var auth = DependencyService.Get<IAuthenticate>();
            var user = await auth.Authenticate(Client, MobileServiceAuthenticationProvider.Facebook);
            var appServiceIdentities = await Client.InvokeApiAsync<List<AppServiceIdentity>>("/.auth/me");

            await GetUserInformationAsync();

            if (user == null)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await App.Current.MainPage.DisplayAlert("Ops", "Não Conseguimos Efetuar O login", "Ok");
                });
            }

            Settings.UserId = user.UserId;
            Settings.TokenFacebook = user.MobileServiceAuthenticationToken;

            return user;
        }

        public async Task GetUserInformationAsync()
        {
            var appServiceIdentities = await Client.InvokeApiAsync<List<AppServiceIdentity>>("/.auth/me");

            if (appServiceIdentities.Count <= 0)
                return ;

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

            Settings.UserId = providerUserId;
            Settings.Email = email;
            Settings.NameUsuario = name;
            Settings.Image = "http://graph.facebook.com/" + providerUserId + "/picture?type=large";
        }
    }
}


