using StarbuckXamarin.Services;
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
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
