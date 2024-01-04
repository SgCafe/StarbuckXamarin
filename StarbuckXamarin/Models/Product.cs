using Newtonsoft.Json;
using StarbuckXamarin.Viewmodel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace StarbuckXamarin.Models
{
    public class Product : BaseViewmodel
    {
        public string Name { get; set; }
        public string Image { get; set; }
        [JsonProperty("Tall")]
        public double ValueTall { get; set; }
        public string Description { get; set; }
        [JsonProperty("Fav")]
        //public bool ProductFavItem { get; set; }

        bool _productFavItem;
        public bool ProductFavItem
        {
            get => _productFavItem;
            set => SetProperty(ref _productFavItem, value);
        }
        public string SizeCoffee { get; set; }

       
    }
}
