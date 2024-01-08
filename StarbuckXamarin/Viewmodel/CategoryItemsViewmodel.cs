using StarbuckXamarin.Models;
using StarbuckXamarin.Services;
using StarbuckXamarin.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace StarbuckXamarin.Viewmodel
{
    public class CategoryItemsViewmodel : BaseViewmodel
    {
        #region properties
        public INavigation Navigation;
        private readonly IServiceProduct _serviceProduct;

        private Product _parametersReceived;
        public Product ParametersReceived
        {
            get => _parametersReceived;
            set => SetProperty(ref _parametersReceived, value);
        }

        private ObservableCollection<Product> _itemsList;
        public ObservableCollection<Product> ItemsList
        {
            get => _itemsList;
            set => SetProperty(ref _itemsList, value);
        }

        private Product _selectedProduct;
        public Product SelectedProduct
        {
            get => _selectedProduct;
            set
            {
                if (SetProperty(ref _selectedProduct, value) && value != null)
                {
                    NavigateToDetailPage(value);
                    SelectedProduct = null;
                }
            }
        }

        #endregion

        #region constructor
        public CategoryItemsViewmodel(INavigation navigation, Dictionary<string, object> parameters)
        {
            Navigation = navigation;
            _serviceProduct = new ServiceProduct();
            GetValuesParameters(parameters);
            LoadCategories();
            AddFavouriteCommand = new Command<Product>(ExecuteAddFavouriteCommand);
        }
        #endregion

        #region commands
        public ICommand AddFavouriteCommand { get; set; }
        #endregion

        #region methods
        private void GetValuesParameters(Dictionary<string, object> parameters)
        {
            if (parameters.TryGetValue("Category", out object category) && category is Product)
            {
                ParametersReceived = (Product)category;
            }
        }

        public async void LoadCategories()
        {
            if (ParametersReceived != null)
            {
                var productsByCategory = await _serviceProduct.GetProductAsync();
                var filterProducts = productsByCategory.Where(p => p.CategoryName == ParametersReceived.CategoryName);
                ItemsList = new ObservableCollection<Product>(filterProducts);
            }
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

        private async void NavigateToDetailPage(Product product)
        {
            var parameters = new Dictionary<string, object>
            {
                {"Product", product }
            };

            DetailPage detailPage = new DetailPage(parameters);
            await Navigation.PushAsync(detailPage);
        }
        #endregion
    }
}
