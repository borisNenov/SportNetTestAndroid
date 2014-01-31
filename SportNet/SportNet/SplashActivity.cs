using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Content.PM;

namespace SportNet
{
	using System.Threading;

	using Android.App;
	using Android.OS;
	[Activity (Theme = "@style/Theme.Splash", MainLauncher = true, NoHistory = true, ScreenOrientation = ScreenOrientation.Portrait)]			
	public class SplashActivity : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate(bundle);
			Thread.Sleep(100); // Simulate a long loading process on app startup.
			//if (!Globals.IsLoggedIn ()) {
				//StartActivity (typeof(MainActivity));
				//} else {
			StartActivity (typeof(MainTabActivity));
			//}

		}
	}
}


