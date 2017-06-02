using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Microsoft.WindowsAzure.MobileServices;
using ProvaEvidencia.Services;
using ProvaEvidencia.Droid.Authorize;
using Android.Webkit;

[assembly:Xamarin.Forms.Dependency(typeof(AuthenticateAndroid))]
namespace ProvaEvidencia.Droid.Authorize
{
    public class AuthenticateAndroid : IAuthenticate
    {
        public async Task<MobileServiceUser> Authenticate(MobileServiceClient cliente, MobileServiceAuthenticationProvider provider)
        {
            try
            {
                return await cliente.LoginAsync(Xamarin.Forms.Forms.Context, MobileServiceAuthenticationProvider.Facebook);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Erro no Login " + ex.ToString());
                return null;
            }
        }

        public async Task LogoutAsync(MobileServiceClient client, MobileServiceAuthenticationProvider provider)
        {
            try
            {
                CookieManager.Instance.RemoveAllCookie();
                await client.LogoutAsync();              
            }
            catch (Exception ex)
            {

                System.Diagnostics.Debug.WriteLine("Erro ao tentar o Logout = " + ex.ToString());
            }
        }
    }
}