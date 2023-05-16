using StarbuckXamarin.Viewmodel;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StarbuckXamarin.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DetailPage : ContentPage
	{
		public DetailPage ()
		{
			InitializeComponent ();
			BindingContext = new DetailPageViewmodel();
		}
	}
}