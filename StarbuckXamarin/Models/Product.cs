using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace StarbuckXamarin.Models
{
    public class Product
    {
        public string Name { get; set; }
        public string Image { get; set; }
        [JsonProperty("Tall")]
        public double ValueTall { get; set; }
        public string Description { get; set; }
        [JsonProperty("Fav")]
        public bool ProductFavItem { get; set; }
        public string SizeCoffee { get; set; }

        
    }
}
