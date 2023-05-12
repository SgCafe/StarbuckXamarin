using StarbuckXamarin.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace StarbuckXamarin.Viewmodel
{
    public class HomePageViewmodel : BaseViewmodel
    {
        #region properties
        private ObservableCollection<Product> _coffeeList;
        public ObservableCollection<Product> CoffeeList
        {
            get => _coffeeList; 
            set => SetProperty(ref _coffeeList, value); 
        }

        private string _categorySelectorPrimary;
        public string CategorySelectorPrimary
        {
            get => _categorySelectorPrimary;
            set => SetProperty(ref _categorySelectorPrimary, value);
        }   


        #endregion

        #region constructor
        public HomePageViewmodel() 
        {
            CategorySelectorPrimary = "All";
            PopulateList();
        }
        #endregion

        #region commands

        #endregion

        #region methods
        private void PopulateList()
        {
            CoffeeList = new ObservableCollection<Product>()
            {
                new Product
                {
                    Name = "Chocolate Frappuccino",
                    ValueTall = 20.00,
                    Image = "Brigadeiro.png"
                },
                new Product
                {
                    Name = "Tea Frappuccino",
                    ValueTall = 19.00,
                    Image = "chaVerde.png"
                }
            };
        }
        #endregion
    }
}
