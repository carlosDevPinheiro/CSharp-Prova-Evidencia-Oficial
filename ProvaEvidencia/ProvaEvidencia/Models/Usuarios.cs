using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProvaEvidencia.Models
{
    public class Usuarios
    {
        private string _id;
        [JsonProperty(PropertyName = "id")]
        public string ID
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _name;
        [JsonProperty(PropertyName = "name")]
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private string _phone;
        [JsonProperty(PropertyName = "phone")]
        public string Phone
        {
            get { return _phone; }
            set { _phone = value; }
        }

        private string _version;
        [JsonProperty(PropertyName = "version")]
        public string Version
        {
            get { return _version; }
            set { _version = value; }
        }

        private bool _done;
        [JsonProperty(PropertyName = "complete")]
        public bool Done
        {
            get { return _done; }
            set { _done = value; }
        }

        private string _email;
        [JsonProperty(PropertyName = "email")]
        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        private string _image;
        [JsonProperty(PropertyName = "image")]
        public string Photo
        {
            get { return _image; }
            set { _image = value; }
        }
    }
}
