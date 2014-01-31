using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Views.TextService;
using Android.Content.PM;

namespace SportNet
{
	[Activity (Label = "SportNet", ScreenOrientation = ScreenOrientation.Portrait)]
	public class MainActivity : Activity
	{

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.Main);
			Console.WriteLine ("MainActivity");

			LinearLayout layout = FindViewById<LinearLayout> (Resource.Id.MainLayout);
			layout.SetBackgroundResource (Resource.Drawable.wall);
			// Get our button from the layout resource,
			// and attach an event to it
			Button createAcc = FindViewById<Button> (Resource.Id.CreateAnAccount);
			Button login = FindViewById<Button> (Resource.Id.Login);
			Button maybeLater = FindViewById<Button> (Resource.Id.MaybeLater);
			//var tv = FindViewById<TextView> (Resource.Id.systemUiFlagTextView);
			


			createAcc.Click += delegate {
				var signUp = new Intent (this, typeof(SignUpActivity));
				StartActivity (signUp);
				//tv.SystemUiVisibility = (StatusBarVisibility)View.SystemUiFlagLowProfile;
			};

			var loginActivity = new Intent (this, typeof(LoginActivity));			
			login.Click += delegate {
				StartActivity (loginActivity);
			};

			var mainActivity = new Intent (this, typeof(MainTabActivity));			// no user
			maybeLater.Click += delegate {
				StartActivity(mainActivity);
			};
		}
	}
}


