﻿using StarbuckXamarin.Viewmodel;
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
	public partial class CategoryItemsPage : ContentPage
	{
		public CategoryItemsPage (Dictionary<string, object> parameters)
		{
			InitializeComponent ();
			BindingContext = new CategoryItemsViewmodel(Navigation, parameters);
		}
	}
}