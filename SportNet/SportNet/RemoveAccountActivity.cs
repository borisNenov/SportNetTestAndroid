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
	[Activity (Label = "RemoveAccountActivity", ScreenOrientation = ScreenOrientation.Portrait)]			
	public class RemoveAccountActivity : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.RemoveAccount);
			Console.WriteLine ("RemoveAccountActivity");

			ImageView logo = FindViewById<ImageView> (Resource.Id.ActionBarLogo);
			logo.Visibility = ViewStates.Invisible;

			ImageView menu = FindViewById<ImageView> (Resource.Id.ActionBarMenu);
			menu.Visibility = ViewStates.Invisible;

			TextView title = FindViewById<TextView> (Resource.Id.ActionBarTitle);
			title.Text = "Remove Account";

			Button back = FindViewById<Button> (Resource.Id.ActionBarBack);
			back.Click += delegate {
				Finish();
			};

			EditText password = FindViewById<EditText> (Resource.Id.Password);
			password.TextChanged += (object sender, Android.Text.TextChangedEventArgs e) => {

			};

			Button submit = FindViewById<Button> (Resource.Id.Submit);
			submit.Click += delegate {
				Console.WriteLine("Submitting!");
			};


			// Create your application here
		}
	}
}

