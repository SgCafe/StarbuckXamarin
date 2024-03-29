﻿using StarbuckXamarin.Models;
using StarbuckXamarin.Services;
using StarbuckXamarin.Views;
using System;
using System.Collections.Generic;
using System.Linq;
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
            set
            {
                if (SetProperty(ref _valueCoffe, value))
                {
                    RecalculateTotalValue();
                }
            }
        }

        private List<Cart> _cardItems = new List<Cart>();

        private int _quantityChange;
        public int QuantityChange
        {
            get => _quantityChange;
            set
            {
                if(SetProperty(ref _quantityChange, value))
                {
                    RecalculateTotalValue();
                }
            }
        }

        private double _totalValue;
        public double TotalValue
        {
            get => _totalValue;
            set => SetProperty(ref _totalValue, value);
        }


        #endregion

        #region constructor
        public DetailPageViewmodel(INavigation navigation, Dictionary<string, object> parameters)
		{
            _serviceProduct = new ServiceProduct();
            Navigation = navigation;
            BackPageButton = new Command(ExecuteBackPageButtonCommand);
            AddFavouriteCommand = new Command<Product>(ExecuteAddFavouriteCommand);
            AddtoCartCommand = new Command(ExecuteAddtoCartCommandAsync);
            CountLessCommand = new Command(ExecuteCountLessCommand);
            CountMoreCommand = new Command(ExecuteCountMoreCommand);

            GetValuesParameters(parameters);
            UpdateValueCoffee(SizeSelect);
        }
        #endregion

        #region commands
        public ICommand BackPageButton { get; set; }
        public ICommand AddFavouriteCommand { get; set; }
        public ICommand AddtoCartCommand { get; set; }
        public ICommand CountLessCommand { get; set; }
        public ICommand CountMoreCommand { get; set; }
        #endregion

        #region methods
        private void GetValuesParameters(Dictionary<string, object> parameters)
        {
            if (parameters.TryGetValue("Product", out object category) && category is Product)
            {
                ParametersReceived = (Product)category;
            }
            SizeSelect = "Tall";
            QuantityChange = 1;
            UpdateValueCoffee(SizeSelect);
            RecalculateTotalValue();
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

                var cartItems = await _serviceProduct.GetItemsCart();
                var existingCartItem = cartItems.FirstOrDefault(item => item.Name == ParametersReceived.Name && item.Size == SizeSelect);

                if (existingCartItem != null)
                {
                    existingCartItem.Quantity += QuantityChange;
                    //existingCartItem.TotalValue = existingCartItem.Price * existingCartItem.Quantity;
                }
                else
                {
                    double price = UpdateValueCoffee(SizeSelect);
                    var cartItem = new Cart
                    {
                        Name = ParametersReceived.Name,
                        Image = ParametersReceived.Image,
                        Size = SizeSelect,
                        Price = price,
                        Quantity = QuantityChange,
                        TotalValue = price * QuantityChange
                    };

                    _cardItems.Add(cartItem);
                }



                var firebaseKey = await _serviceProduct.SendCartItems(_cardItems);

                Console.WriteLine($"Item added to cart with Firebase key: {firebaseKey}");

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
                        ValueCoffe = ParametersReceived.ValueTall;
                        break;
                    case "Grande":
                        ValueCoffe = ParametersReceived.Grande;
                        break;
                    case "Venti":
                        ValueCoffe = ParametersReceived.Venti;
                        break;
                    default:
                        ValueCoffe = ParametersReceived.ValueTall; 
                        break;
                }
                return ValueCoffe;
            }
            return 0;
        }

        private void RecalculateTotalValue()
        {
            TotalValue = QuantityChange * UpdateValueCoffee(SizeSelect);
        }

        private void ExecuteCountLessCommand()
        {
            if (QuantityChange != 1)
            {
                QuantityChange--;
                RecalculateTotalValue();
            }
        }

        private void ExecuteCountMoreCommand()
        {
            QuantityChange++;
            RecalculateTotalValue();
        }

        private async void ExecuteBackPageButtonCommand()
        {
            await Navigation.PopAsync();
        }
        #endregion
    }
}
