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
        private string _sizeSelect;

		public string SizeSelect
        {
			get => _sizeSelect; 
			set => SetProperty(ref _sizeSelect , value); 
		}

        #endregion

        #region constructor
        public DetailPageViewmodel()
		{
			SizeSelect = "Tall";
            BackPageButton = new Command(ExecuteBackPageButtonCommand);

        }
        #endregion

        #region commands
        public ICommand BackPageButton { get; set; }
        #endregion

        #region methods
        private async void ExecuteBackPageButtonCommand()
        {
            await Shell.Current.GoToAsync("///homeShell");
        }
        #endregion
    }
}
