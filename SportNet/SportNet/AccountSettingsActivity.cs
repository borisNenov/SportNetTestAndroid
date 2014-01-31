using System;
using Android.Animation;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SportNet.Web.Models;
using Newtonsoft.Json;
using Android.Content.PM;

namespace SportNet
{
	[Activity (Label = "AccountSettingsActivity", ScreenOrientation = ScreenOrientation.Portrait)]			
	public class AccountSettingsActivity : Activity
	{
		ListView menuList;

		//=========================================================================================
		private ImageView _slideLeftBtn;
		private View _leftMenu, _app;										// For the Sliding Menu
		private bool _menuLeftOut;
		private Context _context;
		//=========================================================================================

		CategoryModel model;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.AccountSettings);
			Console.WriteLine ("AccountSettingsActivity");

			ImageView logo = FindViewById<ImageView> (Resource.Id.ActionBarLogo);
			logo.Visibility = ViewStates.Invisible;

			ImageView menu = FindViewById<ImageView> (Resource.Id.ActionBarMenu);
			//menu.Visibility = ViewStates.Invisible;

			TextView title = FindViewById<TextView> (Resource.Id.ActionBarTitle);
			title.Text = "Account Settings";

			Button back = FindViewById<Button> (Resource.Id.ActionBarBack);
			back.Visibility = ViewStates.Invisible;

			Button editProfile = FindViewById<Button> (Resource.Id.EditProfileButton);
			editProfile.Click += delegate {
				var edit = new Intent (this, typeof(EditProfileActivity));
				StartActivity(edit);
			};

			Button changePass = FindViewById<Button> (Resource.Id.ChangePasswordButton);
			changePass.Click += delegate {
				var pass = new Intent (this, typeof(ChangePasswordActivity));
				StartActivity(pass);
			};

			Button notifications = FindViewById<Button> (Resource.Id.NotificationsButton);
			notifications.Click += delegate {
				var notification = new Intent (this, typeof(NotificationsActivity));
				StartActivity(notification);
			};

			Button remove = FindViewById<Button> (Resource.Id.RemoveAccountButton);
			remove.Click += delegate {
				var removeAcc = new Intent (this, typeof(RemoveAccountActivity));
				StartActivity(removeAcc);
			};

			Button aboutApp = FindViewById<Button> (Resource.Id.AboutAppButton);
			aboutApp.Click += delegate {
				var about = new Intent (this, typeof(AboutThisAppActivity));
				StartActivity(about);
			};

			Button signOut = FindViewById<Button> (Resource.Id.SignOutButton);
			signOut.Click += delegate {
				var sdCardPath = Android.OS.Environment.ExternalStorageDirectory.Path;
				var filePath = System.IO.Path.Combine (sdCardPath, "ExampleFile.txt");
				if (System.IO.File.Exists (filePath)) {
					System.IO.File.Delete(filePath);
				}
				StartActivity(typeof(MainActivity));
			};

			Button settings = FindViewById<Button> (Resource.Id.Settings);
			settings.Click += delegate {
				_slideLeftBtn.PerformClick();

			};

			menuList = FindViewById<ListView> (Resource.Id.MenuMenu);

			//----------------------------------------------------------------------------------------------
			var request = new RestRequest ();
			request.RequestFinished += (object sender, RequestEndedArgs e) => {
				var data = (CategoryModel)JsonConvert.DeserializeObject(e.Result, typeof(CategoryModel));
				model = data;
				// invoke it on the main thread
				RunOnUiThread(delegate{
					menuList.Adapter = new MainMenuListAdapter(this,model.Categories);
				});
			};
			request.Send (string.Format (RequestConfig.News(), 0, 0), "GET");
			//----------------------------------------------------------------------------------------------


			_context = ApplicationContext;



			//menuList = FindViewById<ListView> (Resource.Id.MenuMenu);
			//menuList.Adapter = new MainMenuListAdapter (this, sports,checkedPosition);

			TextView menuName = FindViewById<TextView> (Resource.Id.MenuName);
			menuName.Text = string.Format("{0} {1}",Globals.firstName ,Globals.lastName);

			menuList.ItemClick += (s, arg) => {
				var main = new Intent (this, typeof(MainTabActivity));
				StartActivity (main);

			};

			Button addContent = FindViewById<Button> (Resource.Id.AddContent);
			addContent.Click += delegate {
				var customizingSelection = new Intent (this, typeof(CustomizingSelectionActivity));
				StartActivity (customizingSelection);
			};

			TextView firstName = FindViewById<TextView> (Resource.Id.FirstName); 
			firstName.Text = Globals.firstName;
			TextView lastName = FindViewById<TextView> (Resource.Id.LastName);
			lastName.Text = Globals.lastName;
			TextView email = FindViewById<TextView> (Resource.Id.Email);
			email.Text = Globals.email;
		
			DefineGui ();
		}
		void DefineGui ()
		{
			_app = FindViewById<LinearLayout> (Resource.Id.app);
			_leftMenu = FindViewById<View> (Resource.Id.left_menu);


			_slideLeftBtn = FindViewById<ImageView> (Resource.Id.ActionBarMenu);
			_slideLeftBtn.Click += delegate {
				LeftMenuAction ();
			};
		}

		void LeftMenuAction ()
		{
			if (!_menuLeftOut)
				ShowMenuObject();
			else 
				RestoreMainViewObject();
		}


		public override void OnBackPressed ()
		{
			if (_menuLeftOut)
				RestoreMainViewObject ();
			else
				base.OnBackPressed ();
		}

		void ShowMenuObject()
		{
			_leftMenu.Visibility = ViewStates.Visible;
			_menuLeftOut = true;

			var animation = ObjectAnimator.OfFloat(_app, "translationX", new[] { 0f, (_app.MeasuredWidth * 0.70f) });
			animation.SetDuration(700);
			animation.Start();
			//animation.AnimationEnd += (sender, args) => _slideLeftBtn.SetImageResource(Resource.Drawable.menu);
		}

		private void RestoreMainViewObject()
		{
			_menuLeftOut = false;

			var animation = ObjectAnimator.OfFloat(_app, "translationX", new[] { (_app.MeasuredWidth * 0.70f), 0f });
			animation.SetDuration(700);
			animation.Start();
			animation.AnimationEnd += (sender, args) =>
			{
				_leftMenu.Visibility = ViewStates.Invisible;

			};
		}
	}
}

