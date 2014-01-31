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
using Android.Webkit;
using Android.Content.PM;

namespace SportNet
{
	[Activity (Label = "VideoDetailActivity", ScreenOrientation = ScreenOrientation.Portrait)]			
	public class VideoDetailActivity : Activity
	{
		public string passedUrl;
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.VideoDetail);
			Console.WriteLine ("VideoDetailActivity");

			TextView title = FindViewById<TextView> (Resource.Id.ActionBarTitle);
			title.Text = Intent.GetStringExtra("MyTitle") ?? "Data not available";

			ImageView logo = FindViewById<ImageView> (Resource.Id.ActionBarLogo);
			logo.Visibility = ViewStates.Invisible;

			Button back = FindViewById<Button> (Resource.Id.ActionBarBack);
			back.Click += delegate {
				Finish ();
			};

			ImageView menu = FindViewById<ImageView> (Resource.Id.ActionBarMenu);
			menu.Visibility = ViewStates.Invisible;

			this.passedUrl = Intent.GetStringExtra ("MyData") ?? "Data not available";
			WebView localWebView = FindViewById<WebView> (Resource.Id.LocalWebView);
			localWebView.LoadUrl (passedUrl);
			localWebView.Settings.LoadWithOverviewMode = true;
			localWebView.Settings.UseWideViewPort = true;

		}
		public virtual void onBackPressed()
		{			Finish();		}
	}
}

