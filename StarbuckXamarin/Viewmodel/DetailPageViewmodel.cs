using StarbuckXamarin.Models;
using StarbuckXamarin.Services;
using StarbuckXamarin.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace StarbuckXamarin.Viewmodel
{
    public class DetailPageViewmodel : BaseViewmodel
    {

        #region properties
        public INavigation Navigation;

        private readonly IServiceProduct _serviceProduct;

        private string _sizeSelect;
		public string SizeSelect
        {
			get => _sizeSelect;
            set
            {
                if (SetProperty(ref _sizeSelect, value) && value != null)
                {
                    UpdateValueCoffee(SizeSelect);
                }
            }
        }

        private Product _parametersReceived;
        public Product ParametersReceived
        {
            get => _parametersReceived;
            set => SetProperty(ref _parametersReceived, value);
        }

        private double _valueCoffe;

        public double ValueCoffe
        {
            get => _valueCoffe;
            set => SetProperty(ref _valueCoffe, value);
        }

        private List<Cart> _cardItems = new List<Cart>();
        #endregion

        #region constructor
        public DetailPageViewmodel(INavigation navigation, Dictionary<string, object> parameters)
		{
            _serviceProduct = new ServiceProduct();
            Navigation = navigation;
			SizeSelect = "Tall";
            BackPageButton = new Command(ExecuteBackPageButtonCommand);
            AddFavouriteCommand = new Command<Product>(ExecuteAddFavouriteCommand);
            AddtoCartCommand = new Command(ExecuteAddtoCartCommandAsync);
            GetValuesParameters(parameters);
            UpdateValueCoffee(SizeSelect);
        }
        #endregion

        #region commands
        public ICommand BackPageButton { get; set; }
        public ICommand AddFavouriteCommand { get; set; }
        public ICommand AddtoCartCommand { get; set; }
        #endregion

        #region methods
        private void GetValuesParameters(Dictionary<string, object> parameters)
        {
            if (parameters.TryGetValue("Product", out object category) && category is Product)
            {
                ParametersReceived = (Product)category;
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

        private async void ExecuteAddtoCartCommandAsync()
        {
            try
            {
                _cardItems.Clear();

                double price = UpdateValueCoffee(SizeSelect);
                var cartItem = new Cart
                {
                    Name = ParametersReceived.Name,
                    Image = ParametersReceived.Image,
                    Size = SizeSelect,
                    Price = price

                };

                _cardItems.Add(cartItem);
                await _serviceProduct.SendCartItems(_cardItems);

                await Navigation.PushAsync(new CartPage());
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error IsSaving", $"Erro: {ex.Message}", "Ok");
            }
        }

        private double UpdateValueCoffee(string size)
        {
            if (_parametersReceived != null)
            {
                switch (size)
                {
                    case "Tall":
                        return ValueCoffe = ParametersReceived.ValueTall;
                    case "Grande":
                        return ValueCoffe = ParametersReceived.Grande;
                    case "Venti":
                        return ValueCoffe = ParametersReceived.Venti;
                    default:
                        return 0;
                }
            }
            return 0;
        }

        private async void ExecuteBackPageButtonCommand()
        {
            await Navigation.PopAsync();
        }
        #endregion
    }
}
