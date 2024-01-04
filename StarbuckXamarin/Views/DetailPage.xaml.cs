using StarbuckXamarin.Viewmodel;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StarbuckXamarin.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DetailPage : ContentPage
	{
		public DetailPage (Dictionary<string, object> parameters)
		{
			InitializeComponent ();
			BindingContext = new DetailPageViewmodel(Navigation, parameters);
		}
	}
}