using StarbuckXamarin.Models;
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

        private bool _productFavItem;
        public bool ProductFavItem
        {
            get => _productFavItem; 
            set => SetProperty(ref _productFavItem , value); 
        }

        private string _imageSource;
        public string ImageSource
        {
            get => _imageSource;
            set => SetProperty(ref _imageSource, value);
        }

        #endregion

        #region constructor
        public HomePageViewmodel(INavigation navigation)
        {
            CategorySelectorPrimary = "All";
            PopulateList();
            ChangeImageFav = new Command(ExecuteChangeImageFavCommand);
            NavCartCommand = new Command(ExecuteNavCartCommand);
            NavItemCommand = new Command(ExecuteNavItemCommand);
            Navigation = navigation;
        }
        #endregion

        #region commands
        public ICommand ChangeImageFav { get; set; }
        public ICommand NavCartCommand { get; set; }
        public ICommand NavItemCommand { get; set; }
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
                    Image = "Brigadeiro.png",
                    Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s.",
                    ProductFavItem = false,
                },
                new Product
                {
                    Name = "Tea Frappuccino",
                    ValueTall = 19.00,
                    Image = "chaVerde.png",
                    Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s.",
                    ProductFavItem = true,
                }
            };
        }

        private void ExecuteChangeImageFavCommand()
        {
            ProductFavItem = !ProductFavItem;

            if(ProductFavItem)
            {
                ImageSource = "love_filled.png";
            }
            else
            {
                ImageSource = "love_defaul.png";
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
