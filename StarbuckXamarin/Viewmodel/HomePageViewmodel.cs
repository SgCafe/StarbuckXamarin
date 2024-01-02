using StarbuckXamarin.Models;
using StarbuckXamarin.Services;
using StarbuckXamarin.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace StarbuckXamarin.Viewmodel
{
    public class HomePageViewmodel : BaseViewmodel
    {
        #region properties
        public INavigation Navigation;

        private readonly IServiceProduct _serviceProduct;

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

        private bool _isFavorite = false;
        public bool IsFavorite
        {
            get => _isFavorite;
            set => SetProperty(ref _isFavorite, value);
        }
        #endregion

        #region constructor
        public HomePageViewmodel(INavigation navigation)
        {
            _serviceProduct = new ServiceProduct();
            CategorySelectorPrimary = "All";
            PopulateList();
            NavCartCommand = new Command(ExecuteNavCartCommand);
            NavItemCommand = new Command(ExecuteNavItemCommand);
            
            Navigation = navigation;
        }
        #endregion

        #region commands
        public ICommand NavCartCommand { get; set; }
        public ICommand NavItemCommand { get; set; }
        public ICommand AddFavouriteCommand { get; set; }
        #endregion

        #region methods
        private async void PopulateList()
        {
            var coffee = await _serviceProduct.GetProductAsync();
            if (coffee != null)
            {
                CoffeeList = new ObservableCollection<Product>();
                foreach (var item in coffee)
                {
                    CoffeeList.Add(new Product()
                    {
                        Name = item.Name,
                        Image = item.Image,
                        ValueTall = item.ValueTall,
                        
                    });
                    
                }
            }
        }



        private async void ExecuteNavCartCommand()
        {
            await Navigation.PushAsync(new CartPage());
        }

        private async void ExecuteNavItemCommand()
        {
            await Navigation.PushAsync(new DetailPage());
        }
        #endregion
    }
}
