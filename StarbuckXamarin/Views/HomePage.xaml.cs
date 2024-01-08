using StarbuckXamarin.Viewmodel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StarbuckXamarin.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
            BindingContext = new HomePageViewmodel(Navigation);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (BindingContext is HomePageViewmodel viewmodel)
            {
                viewmodel.ExecuteLoadFavItems();
            }
            {

            }
        }
    }
}