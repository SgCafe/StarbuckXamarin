using StarbuckXamarin.Models;
using StarbuckXamarin.Services;
using StarbuckXamarin.Views;
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
        public INavigation Navigation;
        private readonly IServiceProduct _serviceProduct;

        private ObservableCollection<Cart> _coffeeList;
        public ObservableCollection<Cart> CoffeeList
        {
            get => _coffeeList;
            set => SetProperty(ref _coffeeList, value);
        }
        #endregion

        #region constructor
        public CartPageViewmodel(INavigation navigation)
        {
            _serviceProduct = new ServiceProduct();
            PopulateList();
            BackHomePageCommand = new Command(ExecuteBackHomePageCommand);
            AddMoreItemsCommand = new Command(ExecuteAddMoreItemsCommand);
            Navigation = navigation;
        }
        #endregion

        #region commands
        public ICommand BackHomePageCommand { get; set; }
        public ICommand AddMoreItemsCommand { get; set; }
        #endregion

        #region methods

        private async void PopulateList()
        {
            var items = await _serviceProduct.GetItemsCart();
            if (items != null)
            {
                CoffeeList = new ObservableCollection<Cart>();
                foreach (var item in items)
                {
                    CoffeeList.Add(new Cart
                    {
                        Name = item.Name,
                        Image = item.Image,
                        Price = item.Price,
                        Size = item.Size
                    });
                }
            }
        }

        private async void ExecuteAddMoreItemsCommand()
        {
            await Navigation.PushAsync(new HomePage());
        }

        private async void ExecuteBackHomePageCommand()
        {
            await Navigation.PopAsync();
        }
    }
    #endregion

}
