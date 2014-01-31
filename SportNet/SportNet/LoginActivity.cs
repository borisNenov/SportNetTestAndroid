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
using Newtonsoft.Json;
using Android.Content.PM;
using SportNet.Web.Models;
using Facebook;
using Android.Gms.Plus;
using Android.Gms.Common;

namespace SportNet
{
	[Activity (Label = "LoginActivity", ScreenOrientation = ScreenOrientation.Portrait)]			
	public class LoginActivity : Activity, IGooglePlayServicesClientConnectionCallbacks, IGooglePlayServicesClientOnConnectionFailedListener
	{
		// facebook
		private const string AppId = "1390701187813059";
		private const string ExtendedPermissions = "user_about_me,email";
		string accessToken;
		FacebookClient fb;
		// google
		PlusClient plusClient;
		ConnectionResult connectionResult;

		Intent main;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.Login);
			Console.WriteLine ("LoginActivity");

			ImageView logo = FindViewById<ImageView> (Resource.Id.ActionBarLogo);
			logo.Visibility = ViewStates.Invisible;

			ImageView menu = FindViewById<ImageView> (Resource.Id.ActionBarMenu);
			menu.Visibility = ViewStates.Invisible;

			TextView title = FindViewById<TextView> (Resource.Id.ActionBarTitle);
			title.Text = "Login";

			Button back = FindViewById<Button> (Resource.Id.ActionBarBack);
			back.Click += delegate {
				Finish ();
			};

			EditText email = FindViewById<EditText> (Resource.Id.Email);
			EditText password = FindViewById<EditText> (Resource.Id.Password);

			email.TextChanged += (object sender, Android.Text.TextChangedEventArgs e) => {

			};
			password.TextChanged += (object sender, Android.Text.TextChangedEventArgs e) => {
			
			};

			ImageButton facebook = FindViewById<ImageButton> (Resource.Id.loginViaFacebook);
			facebook.Click += delegate {
				var webAuth = new Intent(this, typeof(FBWebViewAuthActivity));
				webAuth.PutExtra("AppId", AppId);
				webAuth.PutExtra("ExtendedPermissions", ExtendedPermissions);
				StartActivityForResult(webAuth, 0);
			};

			ImageButton googlePlus = FindViewById<ImageButton> (Resource.Id.loginViaGoogle);
			plusClient = new PlusClient.Builder (this, this, this).Build ();
			plusClient.RegisterConnectionCallbacks (this);
			plusClient.IsConnectionFailedListenerRegistered (this);

			googlePlus.Click += (object sender, EventArgs e) => {
				if(plusClient.IsConnected || plusClient.IsConnecting) {
					return;
				}

				if(connectionResult == null) {
					plusClient.Connect();
				}
				else {
					resolveGoogleLogin(connectionResult);
				}
			};

			main = new Intent (this, typeof(MainTabActivity));
			Button login = FindViewById<Button> (Resource.Id.loginButton);
			login.Click += delegate {
				Globals.DeleteProfileId();
				var request = new RestRequest();
				request.RequestFinished += (object sender, RequestEndedArgs e) => {
					RunOnUiThread(delegate {
						var id = (string)JsonConvert.DeserializeObject(e.Result, typeof(string));
						Globals.SaveProfileId(id);
						StartActivity (main);
					});
				};
				request.Send(RequestConfig.Login, "POST", new LoginModel { Email = email.Text.Trim(), Password = password.Text.Trim(), RememberMe = true });
			};

			Button forgotPass = FindViewById<Button> (Resource.Id.forgotPassButton);
			var forgotPassActivity = new Intent (this, typeof(ForgotPassActivity));
			forgotPass.Click += delegate {
				StartActivity (forgotPassActivity);
			};
		}

		private void resolveGoogleLogin(ConnectionResult result) 
		{
			if (result.HasResolution) {
				try {
					result.StartResolutionForResult(this, 9000);
				}
				catch(Android.Content.IntentSender.SendIntentException) {
					plusClient.Connect ();
				}
			}
		}

		public void OnConnected (Bundle p0)
		{
			Alert ("Google", "Connected", false, (res) => {});
			var request = new RestRequest ();
			var gName = plusClient.CurrentPerson.Name;
			var gImage = plusClient.CurrentPerson.Image;
			request.RequestFinished += (object sender, RequestEndedArgs e) => {
				RunOnUiThread(delegate {
					Globals.DeleteProfileId();
					var id = (string)JsonConvert.DeserializeObject(e.Result, typeof(string));
					Globals.SaveProfileId(id);
					StartActivity (main);
				});
			};
			request.Send (RequestConfig.Google, "POST", new GoogleClient {
				name = plusClient.CurrentPerson.DisplayName,
				family_name = gName.FamilyName,
				given_name = gName.GivenName,
				id = plusClient.CurrentPerson.Id,
				picture = gImage.Url,
				email = plusClient.AccountName
			});
		}

		public void OnDisconnected ()
		{
			Alert ("Google", "Disonnected", false, (res) => {});
		}

		public void OnConnectionFailed (ConnectionResult p0)
		{
			resolveGoogleLogin (p0);
			connectionResult = p0;
		}

		protected override void OnActivityResult (int requestCode, Result resultCode, Intent data)
		{
			base.OnActivityResult (requestCode, resultCode, data);

			// google
			if (requestCode == 9000) {
				if (resultCode == Result.Ok) {
					plusClient.Connect ();
				}
				return;
			}

			// facebook
			switch (resultCode) {
			case Result.Ok:
				accessToken = data.GetStringExtra ("AccessToken");
				string userId = data.GetStringExtra ("UserId");
				string error = data.GetStringExtra ("Exception");

				fb = new FacebookClient (accessToken);
				fb.GetTaskAsync ("me").ContinueWith (t => {
					if(!t.IsFaulted) {
						var result = (IDictionary<string, object>)t.Result;
						var request = new RestRequest ();

						RunOnUiThread(delegate {
							request.RequestFinished += (object sender, RequestEndedArgs e) => {
								RunOnUiThread(delegate {
									Globals.DeleteProfileId();
									var id = (string)JsonConvert.DeserializeObject(e.Result, typeof(string));
									Globals.SaveProfileId(id);
									StartActivity (main);
								});
							};
							request.Send(RequestConfig.Facebook, "POST", new FacebookProfile { 
								Id = long.Parse(result["id"].ToString()),
								Name = (string)result["name"],
								first_name = (string)result["first_name"],
								last_name = (string)result["last_name"],
								Birthday = (string)result["birthday"],
								Email = (string)result["email"],
								UserName = (string)result["username"]
							});
						});
					}
					else {
						Alert("Failed to log in", "Reason: " + error, false, (res) => {});
					}
				});

				/*
				var request = new RestRequest ();
				request.RequestFinished += (object sender, RequestEndedArgs e) => {
					Globals.DeleteProfileId();
					RunOnUiThread(delegate {
						var id = (string)JsonConvert.DeserializeObject(e.Result, typeof(string));
						Globals.SaveProfileId(id);
						StartActivity (main);
					});
				};
				request.Send(RequestConfig.Facebook*/

				break;
			case Result.Canceled:
				Alert ("Failed to log in.", "User Cancelled", false, (res) => {});
				break;
			default:
				Alert ("Something went wrong, please try again in a few minutes.", "Oops!", false, (res) => {});
				break;
			};
		}

		public void Alert (string title, string message, bool CancelButton , Action<Result> callback)
		{
			AlertDialog.Builder builder = new AlertDialog.Builder(this);
			builder.SetTitle(title);
			builder.SetMessage(message);

			builder.SetPositiveButton("Ok", (sender, e) => {
				callback(Result.Ok);
			});

			if (CancelButton) {
				builder.SetNegativeButton("Cancel", (sender, e) => {
					callback(Result.Canceled);
				});
			}

			builder.Show();
		}
	}
}

