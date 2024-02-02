using StarbuckXamarin.Models;
using StarbuckXamarin.Viewmodel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
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

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            try
            {
                Console.WriteLine($"SelectedCep in FinishOrderPage ViewModel: {(BindingContext as FinishOrderViewmodel)?.SelectedCep}");
                var location = await Geolocation.GetLastKnownLocationAsync();
                if (location != null)
                {
                    if (!string.IsNullOrEmpty((BindingContext as FinishOrderViewmodel)?.SelectedCep))
                    {
                        var address = await Geocoding.GetLocationsAsync((BindingContext as FinishOrderViewmodel)?.SelectedCep);
                        if (address.Any())
                        {
                            var firstAddress = address.First();
                            var latitude = firstAddress.Latitude;
                            var longitude = firstAddress.Longitude;

                            var locationWithCoordinates = new Location(latitude, longitude);

                            var mapPosition = new Position(latitude, longitude);
                            var mapSpan = MapSpan.FromCenterAndRadius(mapPosition, Distance.FromMiles(0.5));
                            MyMap.HeightRequest = 350;
                            MyMap.WidthRequest = 300;
                            MyMap.IsShowingUser = true;
                            MyMap.MoveToRegion(mapSpan);
                        }
                    }
                }
                else
                {
                    await DisplayAlert("Erro", "Não foi possivel obter a localização atual", "Ok");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro", $"Ocorreu um erro ao obter a localização: {ex.Message}", "OK");
            }
        }
    }
}