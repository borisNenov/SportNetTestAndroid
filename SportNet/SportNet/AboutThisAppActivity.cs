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
	[Activity (Label = "AboutThisAppActivity", ScreenOrientation = ScreenOrientation.Portrait)]			
	public class AboutThisAppActivity : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.AboutThisApp);
			Console.WriteLine ("AboutThisAppActivity");


			LinearLayout layout = FindViewById<LinearLayout> (Resource.Id.Background);
			layout.SetBackgroundResource (Resource.Drawable.bgr);

			ImageView logo = FindViewById<ImageView> (Resource.Id.ActionBarLogo);
			logo.Visibility = ViewStates.Invisible;

			ImageView menu = FindViewById<ImageView> (Resource.Id.ActionBarMenu);
			menu.Visibility = ViewStates.Invisible;

			TextView title = FindViewById<TextView> (Resource.Id.ActionBarTitle);
			title.Text = "About This App";

			Button back = FindViewById<Button> (Resource.Id.ActionBarBack);
			back.Click += delegate {
				Finish();
			};

			ImageButton facebook = FindViewById<ImageButton> (Resource.Id.followFacebook);
			facebook.Click += delegate {

				Console.WriteLine("Just clicked login via Facebook");

			};

			ImageButton twitter = FindViewById<ImageButton> (Resource.Id.followTwitter);
			twitter.Click += delegate {

				Console.WriteLine("Just clicked login via Twitter");

			};

			ImageButton googlePlus = FindViewById<ImageButton> (Resource.Id.followGoogle);
			googlePlus.Click += delegate {

				Console.WriteLine("Just clicked login via GooglePlus");

			};

			Button termsOfUse = FindViewById<Button> (Resource.Id.TermsOfUse);
			termsOfUse.Click += delegate {

				Console.WriteLine("TERMS OF USE");

			};

			Button privacyPolicy = FindViewById<Button> (Resource.Id.PrivasyPolicy);
			privacyPolicy.Click += delegate {

				Console.WriteLine("PRIVACY POLICY");

			};

			Button writeAppReview = FindViewById<Button> (Resource.Id.WriteAppReview);
			writeAppReview.Click += delegate {

				Console.WriteLine("APP REVIEW");

			};


			// Create your application here
		}
	}
}

