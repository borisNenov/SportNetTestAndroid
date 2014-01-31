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
	[Activity (Label = "NewsDetailActivity", ScreenOrientation = ScreenOrientation.Portrait)]			
	public class NewsDetailActivity : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.NewsDetail);
			Console.WriteLine ("NewsDetailActivity");

			ImageView logo = FindViewById<ImageView> (Resource.Id.ActionBarLogo);
			logo.Visibility = ViewStates.Invisible;

			ImageView menu = FindViewById<ImageView> (Resource.Id.ActionBarMenu);
			menu.Visibility = ViewStates.Invisible;

			TextView title = FindViewById<TextView> (Resource.Id.ActionBarTitle);
			title.Text =  Intent.GetStringExtra ("MyCategory") ?? "Sport.net";

			Button back = FindViewById<Button> (Resource.Id.ActionBarBack);
			back.Click += delegate {
				Finish();	
			};

			string url = Intent.GetStringExtra ("MySource") ?? "Data not available";
			WebView localWebView = FindViewById<WebView> (Resource.Id.NewsDetailWebView);
			localWebView.LoadUrl (url);
			localWebView.Settings.LoadWithOverviewMode = true;
			localWebView.Settings.UseWideViewPort = true;

			// Create your application here
		}
	}
}

