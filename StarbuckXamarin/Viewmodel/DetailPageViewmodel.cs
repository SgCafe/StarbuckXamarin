using StarbuckXamarin.Models;
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
            Navigation = navigation;
			SizeSelect = "Tall";
            BackPageButton = new Command(ExecuteBackPageButtonCommand);
            if (parameters.TryGetValue("Product", out object product) && product is Product)
            {
                ParametersReceived = (Product)product;
            }
            UpdateValueCoffe();
        }
        #endregion

        #region commands
        public ICommand BackPageButton { get; set; }
        #endregion

        #region methods
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
