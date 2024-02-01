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
    public partial class FinishOrderPage : ContentPage
    {
        public FinishOrderPage()
        {
            InitializeComponent();
            BindingContext = new FinishOrderViewmodel();
            AnimateBars(); 
        }

        private async void AnimateBars()
        {
            while (true)
            {
                await AnimateBar(Barra1);
                await AnimateBar(Barra2);
                break;
            }
            await AnimateBar(Barra3);

        }

        private async Task AnimateBar(View element)
        {
            await element.FadeTo(0.2, 2000);
            await element.FadeTo(1, 2000);
        }
    }
}