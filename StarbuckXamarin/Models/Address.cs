using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace StarbuckXamarin.Models
{
    public class Address
    {
        [JsonProperty("bairro")]
        public string Bairro { get; set; }
        [JsonProperty("localidade")]
        public string Cidade { get; set; }
    }
}
