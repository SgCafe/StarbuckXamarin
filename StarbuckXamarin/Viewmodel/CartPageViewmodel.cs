using StarbuckXamarin.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace StarbuckXamarin.Viewmodel
{
    public class CartPageViewmodel : BaseViewmodel
    {
        #region properties
        private ObservableCollection<Product> _coffeeList;
        public ObservableCollection<Product> CoffeeList
        {
            get => _coffeeList;
            set => SetProperty(ref _coffeeList, value);
        }
        #endregion

        #region constructor
        public CartPageViewmodel()
        {
            PopulateList();
            BackHomePageCommand = new Command(ExecuteBackHomePageCommand);
        }
        #endregion

        #region commands
        public ICommand BackHomePageCommand { get; set; }
        #endregion

        #region methods
        private async void ExecuteBackHomePageCommand()
        {
            await Shell.Current.GoToAsync("///homeShell");
        }

        private void PopulateList()
        {
            CoffeeList = new ObservableCollection<Product>()
            {
                new Product
                {
                    Name = "Chocolate Frappuccino",
                    ValueTall = 21.00,
                    Image = "Brigadeiro.png",
                    Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s.",
                    ProductFavItem = false,
                    SizeCoffee = "Grande"
                },
                new Product
                {
                    Name = "Tea Frappuccino",
                    ValueTall = 19.00,
                    Image = "chaVerde.png",
                    Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s.",
                    ProductFavItem = true,
                    SizeCoffee = "Tall"
                },
                new Product
                {
                    Name = "Caramelo Frappuccino",
                    ValueTall = 20.00,
                    Image = "Caramelo.png",
                    Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s.",
                    ProductFavItem = true,
                    SizeCoffee = "Venti"
                }
            };
        }


    }
    #endregion

}
