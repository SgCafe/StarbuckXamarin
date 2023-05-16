using System;
using System.Collections.Generic;
using System.Text;

namespace StarbuckXamarin.Viewmodel
{
    public class DetailPageViewmodel : BaseViewmodel
    {
		private string _sizeSelect;

		public string SizeSelect
        {
			get => _sizeSelect; 
			set => SetProperty(ref _sizeSelect , value); 
		}

		public DetailPageViewmodel()
		{
			SizeSelect = "Tall";

        }

	}
}
