using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace StarbuckXamarin.Viewmodel
{
    public class CartPageViewmodel : BaseViewmodel
    {
        #region properties

        #endregion

        #region constructor
        public CartPageViewmodel()
        {
            BackHomePageCommand = new Command(ExecuteBackHomePageCommand);
        }
        #endregion

        #region commands
        public ICommand BackHomePageCommand { get; set; }
        #endregion

        #region methods
        private async void ExecuteBackHomePageCommand()
        {
            await Shell.Current.GoToAsync("///homeShell");
        }
        #endregion

    }
}
