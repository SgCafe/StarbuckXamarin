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
        public string CategoryName { get; set; }
        public string Name { get; set; } 
        public string Image { get; set; }
        public double Quality { get; set; }
        [JsonProperty("Tall")]
        public double ValueTall { get; set; }
        public double Grande { get; set; }
        public double Venti { get; set; }
        private bool _productFavItem;
        public bool ProductFavItem
        {
            get => _productFavItem;
            set => SetProperty(ref _productFavItem, value);
        }
    }
}
