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
using SportNet.Web.Models;
using Newtonsoft.Json;

namespace SportNet
{
	[Activity (Label = "SignUpActivity", ScreenOrientation = ScreenOrientation.Portrait)]			
	public class SignUpActivity : Activity
	{
		public string fName;
		public string lName;
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.SignUp);
			Console.WriteLine ("SignUpActivity");

			ImageView logo = FindViewById<ImageView> (Resource.Id.ActionBarLogo);
			logo.Visibility = ViewStates.Invisible;

			ImageView menu = FindViewById<ImageView> (Resource.Id.ActionBarMenu);
			menu.Visibility = ViewStates.Invisible;

			TextView title = FindViewById<TextView> (Resource.Id.ActionBarTitle);
			title.Text = "Sign Up";

			Button back = FindViewById<Button> (Resource.Id.ActionBarBack);
			back.Click += delegate {
				Finish ();
			};
			// Create your application here

			Button createAcc = FindViewById<Button> (Resource.Id.createAccButton);
			Button choseImage = FindViewById<Button> (Resource.Id.Chose);
			ImageButton facebook = FindViewById<ImageButton> (Resource.Id.signUpViaFacebook);
			ImageButton twitter = FindViewById<ImageButton> (Resource.Id.signUpViaTwitter);
			ImageButton googlePlus = FindViewById<ImageButton> (Resource.Id.signUpViaGoogle);
			EditText firstName = FindViewById<EditText> (Resource.Id.FirstName);
			EditText lastName = FindViewById<EditText> (Resource.Id.LastName);
			EditText email = FindViewById<EditText> (Resource.Id.Email);
			EditText password = FindViewById<EditText> (Resource.Id.Password);
			var customize = new Intent (this, typeof(CustomizingSelectionActivity));
			// Create your application here

			choseImage.Click += delegate {                
				var imageIntent = new Intent ();
				imageIntent.SetType ("image/*");
				imageIntent.SetAction (Intent.ActionGetContent);
				StartActivityForResult (
					Intent.CreateChooser (imageIntent, "Select photo"), 0);
			} ;
     

			/*firstName.KeyPress += (object sender, View.KeyEventArgs e) => {
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
					inputManager.HideSoftInputFromWindow(email.WindowToken, 0);
				}
			};

			password.KeyPress += (object sender, View.KeyEventArgs e) => {
				e.Handled = false;
				if (e.Event.Action == KeyEventActions.Down && e.KeyCode == Keycode.Enter) {

					var inputManager = GetSystemService(InputMethodService) as InputMethodManager;
					inputManager.HideSoftInputFromWindow(password.WindowToken, 0);
				}
			};*/
			firstName.TextChanged += (object sender, Android.Text.TextChangedEventArgs e) => {

			};
			lastName.TextChanged += (object sender, Android.Text.TextChangedEventArgs e) => {

			};
			email.TextChanged += (object sender, Android.Text.TextChangedEventArgs e) => {

			};
			password.TextChanged += (object sender, Android.Text.TextChangedEventArgs e) => {

			};

			this.fName = Globals.firstName;
			this.lName = Globals.lastName;


			createAcc.Click += delegate {
				var model = new RegisterModel { Email = email.Text, FirstName = firstName.Text, LastName = lastName.Text, Password = password.Text.Trim() };
				var request = new RestRequest();
				request.RequestFinished += (object sender, RequestEndedArgs e) => {
					var id = (string)JsonConvert.DeserializeObject(e.Result, typeof(string));
					Globals.SaveProfileId(id);
					StartActivity (customize);
				};
				request.Send(RequestConfig.Register, "POST", model);
			};

			facebook.Click += delegate {

				Console.WriteLine("Just clicked signup via Facebook");

			};

			twitter.Click += delegate {

				Console.WriteLine("Just clicked signup via Twitter");

			};

			googlePlus.Click += delegate {

				Console.WriteLine("Just clicked login via Google Plus");

			};
			
		}
		protected override void OnActivityResult (int requestCode, Result resultCode, Intent data)
		{
			base.OnActivityResult (requestCode, resultCode, data);

			if (resultCode == Result.Ok) {
				var imageView = FindViewById<ImageView> (Resource.Id.profilePic);
				imageView.SetImageURI (data.Data);
			}
		}
	}
}

