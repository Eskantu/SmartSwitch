using System;
using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Xamarin.Forms;
using ImageCircle.Forms.Plugin.Droid;

namespace SmartSwitch.UI.Movil.Droid
{
    [Activity(Label = "SmartSwitch", Icon = "@mipmap/Logo", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            Forms.SetFlags("CarouselView_Experimental");
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
            
            Xamarin.Forms.Forms.Init(this, savedInstanceState);
            Window.SetStatusBarColor(Android.Graphics.Color.Transparent);
            Window.Attributes.LayoutInDisplayCutoutMode = LayoutInDisplayCutoutMode.ShortEdges;
            Window.SetNavigationBarColor(Android.Graphics.Color.Black);
            Window.AddFlags(WindowManagerFlags.LayoutInOverscan);
            ImageCircleRenderer.Init();
            XF.Material.Droid.Material.Init(this, savedInstanceState);
            this.LoadApplication(new App());
            
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}