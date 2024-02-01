using StarbuckXamarin.Models;
using StarbuckXamarin.Services;
using StarbuckXamarin.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace StarbuckXamarin.Viewmodel
{
    public class FavViewmodel : BaseViewmodel
    {
        #region properties
        public INavigation Navigation;
        public readonly IServiceProduct _serviceProduct;

        private ObservableRangeCollection<Product> _coffeeList;
        public ObservableRangeCollection<Product> CoffeeList
        {
            get => _coffeeList;
            set => SetProperty(ref _coffeeList, value);
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

        private bool _isBusy;
        public bool IsBusy
        {
            get => _isBusy;
            set => SetProperty(ref _isBusy, value);
        }
        private bool _isDataLoaded = false;
        #endregion

        #region constructor
        public FavViewmodel(INavigation navigation)
        {
            _serviceProduct = new ServiceProduct();
            Navigation = navigation;
            AddFavouriteCommand = new Command<Product>(ExecuteAddFavouriteCommand);
            PopulateList();
        }
        #endregion

        #region commands
        public ICommand AddFavouriteCommand { get; set; }
        #endregion

        #region methods
        private async void PopulateList()
        {
            var coffee = await _serviceProduct.FilterFavItems();
            if (coffee != null)
            {
                CoffeeList = new ObservableRangeCollection<Product>();
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

        public async void ExecuteLoadFavItems()
        {
            try
            {
                IsBusy = true;

                var updatedCoffeeList = await _serviceProduct.FilterFavItems();

                if (updatedCoffeeList != null)
                {
                    // Remover todos os itens existentes e adicionar os atualizados
                    CoffeeList.Clear();
                    CoffeeList.AddRange(updatedCoffeeList);
                }
            }
            catch (Exception)
            {
                // Tratar exceções conforme necessário
            }
            finally
            {
                IsBusy = false;
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
                    CoffeeList.Remove(prod);
                    CoffeeList.Add(prod);

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
