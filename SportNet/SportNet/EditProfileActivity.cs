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
	[Activity (Label = "EditProfileActivity", ScreenOrientation = ScreenOrientation.Portrait)]			
	public class EditProfileActivity : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.EditProfile);
			Console.WriteLine ("EditProfileActivity");

			ImageView logo = FindViewById<ImageView> (Resource.Id.ActionBarLogo);
			logo.Visibility = ViewStates.Invisible;

			ImageView menu = FindViewById<ImageView> (Resource.Id.ActionBarMenu);
			menu.Visibility = ViewStates.Invisible;

			TextView title = FindViewById<TextView> (Resource.Id.ActionBarTitle);
			title.Text = "Account Settings";

			Button back = FindViewById<Button> (Resource.Id.ActionBarBack);
			back.Click += delegate {
				Finish();
			};

			Button choseImage = FindViewById<Button> (Resource.Id.Chose);
			EditText firstName = FindViewById<EditText> (Resource.Id.EditFirstName);
			EditText lastName = FindViewById<EditText> (Resource.Id.EditLastName);
			EditText email = FindViewById<EditText> (Resource.Id.EditEmail);
			var customize = new Intent (this, typeof(CustomizingSelectionActivity));
			// Create your application here

			choseImage.Click += delegate {                
				var imageIntent = new Intent ();
				imageIntent.SetType ("image/*");
				imageIntent.SetAction (Intent.ActionGetContent);
				StartActivityForResult (
					Intent.CreateChooser (imageIntent, "Select photo"), 0);
			};

			firstName.KeyPress += (object sender, View.KeyEventArgs e) => {
				e.Handled = false;

				if (e.Event.Action == KeyEventActions.Down && e.KeyCode == Keycode.Enter) {
					Globals.firstName = firstName.Text;
					var inputManager = GetSystemService(InputMethodService) as InputMethodManager;
					inputManager.HideSoftInputFromWindow(firstName.WindowToken, 0);
				}
			};

			lastName.KeyPress += (object sender, View.KeyEventArgs e) => {
				e.Handled = false;

				if (e.Event.Action == KeyEventActions.Down && e.KeyCode == Keycode.Enter) {
					Globals.lastName = lastName.Text;
					var inputManager = GetSystemService(InputMethodService) as InputMethodManager;
					inputManager.HideSoftInputFromWindow(firstName.WindowToken, 0);
				}
			};

			email.KeyPress += (object sender, View.KeyEventArgs e) => {
				e.Handled = false;

				if (e.Event.Action == KeyEventActions.Down && e.KeyCode == Keycode.Enter) {
					Globals.email = email.Text;
					var inputManager = GetSystemService(InputMethodService) as InputMethodManager;
					inputManager.HideSoftInputFromWindow(firstName.WindowToken, 0);
				}
			};

			Switch togglebutton = FindViewById<Switch>(Resource.Id.onOffSwitch);
			togglebutton.Click += (o, e) => {
				// Perform action on clicks
				if (togglebutton.Checked)
					Toast.MakeText(this, "Checked", ToastLength.Short).Show ();
				else
					Toast.MakeText(this, "Not checked", ToastLength.Short).Show ();
			};
			// Create your application here
		}
		protected override void OnActivityResult (int requestCode, Result resultCode, Intent data)
		{
			base.OnActivityResult (requestCode, resultCode, data);

			if (resultCode == Result.Ok) {
				var imageView = 
					FindViewById<ImageView> (Resource.Id.profilePic);
				imageView.SetImageURI (data.Data);
			}
		}
	}
}

