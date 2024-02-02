using Newtonsoft.Json;
using StarbuckXamarin.Models;
using StarbuckXamarin.Services;
using StarbuckXamarin.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
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

        private string _cepTxt;
        public string CepTxt
        {
            get => _cepTxt;
            set => SetProperty(ref _cepTxt, value);
        }

        private double _totalItemValue;
        public double TotalItemValue
        {
            get => _totalItemValue;
            set => SetProperty(ref _totalItemValue, value);
        }

        private TimeSpan _selectedTime;
        public TimeSpan SelectedTime
        {
            get => _selectedTime;
            set => SetProperty(ref _selectedTime, value);
        }


        #endregion

        #region constructor
        public CartPageViewmodel(INavigation navigation)
        {
            _serviceProduct = new ServiceProduct();
            PopulateList();
            BackHomePageCommand = new Command(ExecuteBackHomePageCommand);
            AddMoreItemsCommand = new Command(ExecuteAddMoreItemsCommand);
            FindCepCommand = new Command(ExecuteFindCepCommand);
            ClearCepCommand = new Command(ExecuteClearCepCommand);
            CountLessCommand = new Command<Cart>(ExecuteCountLessCommand);
            CountMoreCommand = new Command<Cart>(ExecuteCountMoreCommand);
            DeleteItemCommand = new Command<Cart>(ExecuteDeleteItemCommand);
            FinishOrderCommand = new Command(ExecuteFinishOrderCommand);
            Navigation = navigation;
        }
        #endregion

        #region commands
        public ICommand BackHomePageCommand { get; set; }
        public ICommand AddMoreItemsCommand { get; set; }
        public ICommand FindCepCommand { get; set; }
        public ICommand ClearCepCommand { get; set; }
        public ICommand CountLessCommand { get; set; }
        public  ICommand CountMoreCommand { get; set; }
        public ICommand DeleteItemCommand { get; set; }
        public ICommand FinishOrderCommand { get; set; }
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
                        Size = item.Size,
                        Quantity = item.Quantity,
                        TotalValue = item.TotalValue,
                        FirebaseKey = item.FirebaseKey,
                    });
                }
            }
        }

        private async void ExecuteFindCepCommand()
        {
            try
            {
                var client = new HttpClient();
                var json = await client.GetStringAsync($"https://viacep.com.br/ws/{CepTxt}/json/");
                var data = JsonConvert.DeserializeObject<Address>(json);

                CepTxt = $"{data.Bairro}, {data.Cidade}";
            }
            catch (Exception)
            {
                await Shell.Current.DisplayAlert("Error", $"O cep não foi encontrado, tente novamente", "Ok");
            }
        }

        private void ExecuteClearCepCommand()
        {
            CepTxt = string.Empty;
        }

        private async void ExecuteCountLessCommand(Cart itemCart)
        {
            try
            {
                if (itemCart?.Quantity > 1)
                {
                    itemCart.Quantity--;
                    itemCart.TotalValue = itemCart.Price * itemCart.Quantity;

                    var index = CoffeeList.IndexOf(itemCart);
                    if (index != -1)
                    {
                        CoffeeList[index].Quantity = itemCart.Quantity;
                        CoffeeList[index].TotalValue = itemCart.TotalValue;
                    }

                    Console.WriteLine($"Executing ExecuteCountLessCommand: Key - {itemCart.FirebaseKey}, Quantity - {itemCart.Quantity}");
                    await _serviceProduct.CountLessItemCart(itemCart.FirebaseKey, itemCart.Quantity);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in ExecuteCountLessCommand: {ex.Message}");
            }
        }

        private async void ExecuteCountMoreCommand(Cart itemCart)
        {
            try
            {
                if (itemCart?.Quantity >= 1)
                {
                    itemCart.Quantity++;
                    itemCart.TotalValue = itemCart.Price * itemCart.Quantity;

                    var index = CoffeeList.IndexOf(itemCart);
                    if (index != +1)
                    {
                        CoffeeList[index].Quantity = itemCart.Quantity;
                        CoffeeList[index].TotalValue = itemCart.Price * itemCart.Quantity;
                    }

                    Console.WriteLine($"Executing ExecuteCountLessCommand: Key - {itemCart.FirebaseKey}, Quantity - {itemCart.Quantity}");
                    await _serviceProduct.CountMoreItemCart(itemCart.FirebaseKey, itemCart.Quantity);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in ExecuteCountLessCommand: {ex.Message}");
            }
        }

        private void RecalculateTotalCartValue()
        {
            TotalItemValue = CoffeeList.Sum(item => item.TotalValue);
        }

        private async void ExecuteDeleteItemCommand(Cart selectedItem)
        {
            try
            {
                if (selectedItem != null)
                {
                    var deleteItem = await Shell.Current.DisplayAlert("Aviso", "Você deseja deletar esse item?", "Sim", "Não");
                    if (deleteItem)
                    {

                        await _serviceProduct.DeleteItemCart(selectedItem.FirebaseKey);
                        CoffeeList.Remove(selectedItem);

                        await Shell.Current.DisplayAlert("Aviso","Item Deletado com sucesso","Ok");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in DeleteItem: {ex.Message}");
            }
        }


        private async void ExecuteFinishOrderCommand()
        {
            try
            {
                var itemCart = await _serviceProduct.GetItemsCart();

                if (itemCart.Count != 0 && !string.IsNullOrEmpty(CepTxt))
                {
                    var finishOrderPage = new FinishOrderPage();
                    finishOrderPage.BindingContext = new FinishOrderViewmodel
                    {
                        SelectedCep = CepTxt,
                        SelectedTime = SelectedTime
                    };
                    await Navigation.PushAsync(finishOrderPage);
                }
                else
                {
                    await Shell.Current.DisplayAlert("Error", "Adicione items ao carrinho ou o Cep para finalizar a compra", "Ok");
                }
            }
            catch (Exception ex)
            {
                
            }
        }

        private async void ExecuteAddMoreItemsCommand()
        {
            await Navigation.PushAsync(new AllItemsPage());
        }

        private async void ExecuteBackHomePageCommand()
        {
            await Navigation.PopAsync();
        }
    }
    #endregion

}
