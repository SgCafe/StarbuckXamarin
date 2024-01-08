using StarbuckXamarin.Models;
using StarbuckXamarin.Services;
using System;
using System.Collections.Generic;
using System.Text;
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
                    UpdateValueCoffe();
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


        #endregion

        #region constructor
        public DetailPageViewmodel(INavigation navigation, Dictionary<string, object> parameters)
		{
            _serviceProduct = new ServiceProduct();
            Navigation = navigation;
			SizeSelect = "Tall";
            BackPageButton = new Command(ExecuteBackPageButtonCommand);
            AddFavouriteCommand = new Command<Product>(ExecuteAddFavouriteCommand);
            GetValuesParameters(parameters);
            UpdateValueCoffe();
        }

        
        #endregion

        #region commands
        public ICommand BackPageButton { get; set; }
        public ICommand AddFavouriteCommand { get; set; }
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

        private void UpdateValueCoffe()
        {
            if (_parametersReceived != null)
            {
                switch (SizeSelect)
                {
                    case "Tall":
                        ValueCoffe = ParametersReceived.ValueTall;
                        break;
                    case "Grande":
                        ValueCoffe = ParametersReceived.Grande;
                        break;
                    case "Venti":
                        ValueCoffe = ParametersReceived.Venti;
                        break;
                    default:
                        break;
                }
            }
        }

        private async void ExecuteBackPageButtonCommand()
        {
            await Navigation.PopAsync();
        }
        #endregion
    }
}
