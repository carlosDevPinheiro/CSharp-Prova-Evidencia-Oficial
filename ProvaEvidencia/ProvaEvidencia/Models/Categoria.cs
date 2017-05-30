using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProvaEvidencia.Models
{
    public class Categoria
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

        private string _image;
        [JsonProperty(PropertyName = "image")]
        public string Image
        {
            get { return _image; }
            set { _image = value; }
        }

        //private string _version;
        //[JsonProperty(PropertyName = "version")]
        //public string Version
        //{
        //    get { return _version; }
        //    set { _version = value; }
        //}

        private string _detalhes;
        [JsonProperty(PropertyName = "detalhes")]
        public string DetalhesCategoria
        {
            get { return _detalhes; }
            set { _detalhes = value; }
        }

        private bool _done;
        [JsonProperty(PropertyName = "complete")]
        public bool Done
        {
            get { return _done; }
            set { _done = value; }
        }
    }
}
