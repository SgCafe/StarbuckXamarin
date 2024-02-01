using Newtonsoft.Json;
using StarbuckXamarin.Viewmodel;
using System;
using System.Collections.Generic;
using System.Text;

namespace StarbuckXamarin.Models
{
    public class Cart : BaseViewmodel
    {
        public string FirebaseKey { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public double Price { get; set; }
        public   string Size { get; set; }
        private int _quantity;
        public int Quantity
        {
            get => _quantity; 
            set => SetProperty(ref _quantity, value);
        }
        private double _totalValue;
        public double TotalValue
        {
            get => _totalValue;
            set => SetProperty(ref _totalValue, value);
        }
    }
}
