using Plugin.FirebasePushNotification;
using StarbuckXamarin.Services;
using StarbuckXamarin.Views;
using System;
using System.Globalization;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StarbuckXamarin
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            CultureInfo cultureInfo = new CultureInfo("en-US");
            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

            MainPage = new MainPage();
            DependencyService.Register<IServiceProduct, ServiceProduct>();

            CrossFirebasePushNotification.Current.OnTokenRefresh += Current_OnTokenRefresh;
        }

        private void Current_OnTokenRefresh(object source, FirebasePushNotificationTokenEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine($"Token -> {e.Token}");
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
            base.OnResume();
            if (App.Current.Properties.ContainsKey("FinishOrderPage"))
            {
                MainPage.Navigation.PushAsync(new FinishOrderPage());
                App.Current.Properties.Remove("FinishOrderPage");
            };
        }
    }
}
