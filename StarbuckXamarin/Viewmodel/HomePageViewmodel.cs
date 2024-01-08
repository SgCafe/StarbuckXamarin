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

        private ObservableCollection<Product> _categoryFilter;

        public ObservableCollection<Product> CategoryFilter
        {
            get => _categoryFilter;
            set => SetProperty(ref _categoryFilter, value);
        }

        private ObservableCollection<Product> _coffeeList;
        public ObservableCollection<Product> CoffeeList
        {
            get => _coffeeList; 
            set => SetProperty(ref _coffeeList, value); 
        }

        private Product _categorySelector;
        public Product CategorySelector
        {
            get => _categorySelector;
            set
            {
                if (SetProperty(ref _categorySelector, value) && value != null)
                {
                    NavigateToCategory(value);
                    CategorySelector = null;
                }
            }
        }

        private Product _selectedProduct;
        public Product SelectedProduct
        {
            get => _selectedProduct;
            set
            {
               if(SetProperty(ref _selectedProduct, value) && value != null)
                {
                    NavigateToDetailPage(value);
                    SelectedProduct = null;
                }
            }
        }

        private Product _quality;
        public Product Quality
        {
            get => _quality;
            set => SetProperty(ref _quality, value);
        }

        #endregion

        #region constructor
        public HomePageViewmodel(INavigation navigation)
        {
            _serviceProduct = new ServiceProduct();
            PopulateList();
            LoadCategoriesFilter();
            NavCartCommand = new Command(ExecuteNavCartCommand);
            AddFavouriteCommand = new Command<Product>(ExecuteAddFavouriteCommand);
            NavToAllItemsPageCommand = new Command(ExecuteNavToAllItemsPageCommand);
            Navigation = navigation;
        }

        #endregion

        #region commands
        public ICommand NavCartCommand { get; set; }
        public ICommand AddFavouriteCommand { get; set; }
        public ICommand CheckChangedCommand { get; set; }
        public ICommand NavToAllItemsPageCommand { get; set; }
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
                        CategoryName = item.CategoryName,
                        Name = item.Name,
                        Image = item.Image,
                        ValueTall = item.ValueTall,
                        Grande = item.Grande,
                        Venti = item.Venti,
                        ProductFavItem = item.ProductFavItem,
                        Quality = item.Quality,                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                     
                    });
                }
            }
        }

        private void LoadCategoriesFilter()
        {
            CategoryFilter = new ObservableCollection<Product>
            {
                new Product()
                {
                    CategoryName = "Especiais"
                },
                new Product()
                {
                    CategoryName = "Expressos e Cafés"
                },
                new Product()
                {
                    CategoryName = "Frappuccino"
                },
            };
        }

        private async void ExecuteAddFavouriteCommand(Product prod)
        {
            if (prod != null)
            {
                
                string nameProd = prod.Name;
                bool newFavValue = !prod.ProductFavItem;

                var changeFavItem = await _serviceProduct.ChangeFavoriteItem(nameProd, newFavValue);

                if (changeFavItem)
                {
                    prod.ProductFavItem = newFavValue;

                    System.Diagnostics.Debug.WriteLine("---------> item " + prod.Name + " is favourited or not: " + prod.ProductFavItem);
                }
            }
        }

        public void ExecuteLoadFavItems()
        {
            PopulateList();
        }
        private async void ExecuteNavToAllItemsPageCommand()
        {
            await Navigation.PushAsync(new AllItemsPage());
        }

        private async void NavigateToCategory(Product category)
        {
            var parameters = new Dictionary<string, object>
            {
                {"Category", category }
            };

            CategoryItemsPage categoryPage = new CategoryItemsPage(parameters);
            await Navigation.PushAsync(categoryPage);
        }

        private async void NavigateToDetailPage(Product product)
        {
            var parameters = new Dictionary<string, object>
            {
                {"Product", product }
            };

            DetailPage detailPage = new DetailPage(parameters);
            await Navigation.PushAsync(detailPage);
        }

        private async void ExecuteNavCartCommand()
        {
            await Navigation.PushAsync(new CartPage());
        }
        #endregion
    }
}
