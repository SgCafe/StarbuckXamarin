using StarbuckXamarin.Services;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StarbuckXamarin
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

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
