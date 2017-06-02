using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProvaEvidencia.Services
{
    public interface IAuthenticate
    {
        Task<MobileServiceUser> Authenticate(MobileServiceClient cliente, MobileServiceAuthenticationProvider provider);
        Task LogoutAsync(MobileServiceClient client, MobileServiceAuthenticationProvider provider);
    }
}
