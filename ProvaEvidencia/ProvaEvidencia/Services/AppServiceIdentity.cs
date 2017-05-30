using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProvaEvidencia.Services
{
    public class AppServiceIdentity
    {
        [JsonProperty("id_token")]
        public string IdToken { get; set; }

        [JsonProperty("provider_name")]
        public string ProviderName { get; set; }

        [JsonProperty("user_id")]
        public string UserId { get; set; }

        [JsonProperty("user_claims")]
        public List<UserClaim> UserClaims { get; set; }
    }

    public class UserClaim
    {
        [JsonProperty("typ")]
        public string Type { get; set; }

        [JsonProperty("val")]
        public string Value { get; set; }
    }
}
