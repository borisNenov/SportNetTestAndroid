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
	[Activity (Label = "ChangePasswordActivity", ScreenOrientation = ScreenOrientation.Portrait)]			
	public class ChangePasswordActivity : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.ChangePassword);
			Console.WriteLine ("ChangePasswordActivity");

			ImageView logo = FindViewById<ImageView> (Resource.Id.ActionBarLogo);
			logo.Visibility = ViewStates.Invisible;

			ImageView menu = FindViewById<ImageView> (Resource.Id.ActionBarMenu);
			menu.Visibility = ViewStates.Invisible;

			TextView title = FindViewById<TextView> (Resource.Id.ActionBarTitle);
			title.Text = "Change Password";

			Button back = FindViewById<Button> (Resource.Id.ActionBarBack);
			back.Click += delegate {
				Finish();
			};

			EditText currentPass = FindViewById<EditText> (Resource.Id.CurrentPassword);
			EditText newPass = FindViewById<EditText> (Resource.Id.NewPassword);
			EditText confirmPass = FindViewById<EditText> (Resource.Id.ConfirmPassword);

			currentPass.TextChanged += (object sender, Android.Text.TextChangedEventArgs e) => {

			};

			newPass.TextChanged += (object sender, Android.Text.TextChangedEventArgs e) => {

			};

			confirmPass.TextChanged += (object sender, Android.Text.TextChangedEventArgs e) => {


			};

			Button change = FindViewById<Button> (Resource.Id.Change);
			change.Click += delegate {
				
			};




		}
	}
}

