using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StarbuckXamarin.Views.Templates
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ButtonAddItem : Grid
    {
        public int quantity = 1;

        public ButtonAddItem ()
		{
			InitializeComponent ();
            quantity = 1;
            

        }

        private void count_less(object sender, EventArgs e)
        {
            if(quantity != 0)
            {
                quantity--;
                txtButton.Text = quantity.ToString();
            }
			
        }
        private void count_more(object sender, EventArgs e)
        {
            quantity++;
            txtButton.Text = quantity.ToString();

        }
    }
}