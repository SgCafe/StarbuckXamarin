using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Google.Android.Material.BottomNavigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using Android.Content.Res;

namespace StarbuckXamarin.Droid.Effects
{
    class MyShellBottomNavViewAppearanceTracker : IShellBottomNavViewAppearanceTracker
    {
        public void Dispose()
        {
        }

        public void ResetAppearance(BottomNavigationView bottomView)
        {
        }

        public void SetAppearance(BottomNavigationView bottomView, IShellAppearanceElement appearance)
        {
            bottomView.SetBackgroundResource(Resource.Drawable.bottombackground);
            bottomView.ItemIconTintList = ColorStateList.ValueOf(Android.Graphics.Color.White);

            bottomView.SetPadding(0, 30, 0, 0);


        }
    }
}