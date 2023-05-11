using Android.Content.Res;
using Google.Android.Material.BottomNavigation;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

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