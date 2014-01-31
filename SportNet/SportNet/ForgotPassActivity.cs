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
using Android.Views.InputMethods;
using Android.Content.PM;

namespace SportNet
{
	[Activity (Label = "ForgotPassActivity", ScreenOrientation = ScreenOrientation.Portrait)]			
	public class ForgotPassActivity : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.ForgotPass);
			Console.WriteLine ("ForgotPassActivity");

			ImageView logo = FindViewById<ImageView> (Resource.Id.ActionBarLogo);
			logo.Visibility = ViewStates.Invisible;

			ImageView menu = FindViewById<ImageView> (Resource.Id.ActionBarMenu);
			menu.Visibility = ViewStates.Invisible;

			TextView title = FindViewById<TextView> (Resource.Id.ActionBarTitle);
			title.Text= "Forgot Your Password";

			Button back = FindViewById<Button> (Resource.Id.ActionBarBack);
			back.Click += delegate {
				Finish();	
			};

			EditText email = FindViewById<EditText> (Resource.Id.Email);

			email.TextChanged += (object sender, Android.Text.TextChangedEventArgs e) => {

			};

			Button send = FindViewById<Button> (Resource.Id.Send);
			send.Click += delegate {

			};
			// Create your application here
		}
	}
}

