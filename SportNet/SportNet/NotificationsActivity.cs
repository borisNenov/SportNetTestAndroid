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
	[Activity (Label = "NotificationsActivity", ScreenOrientation = ScreenOrientation.Portrait)]			
	public class NotificationsActivity : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.Notifications);
			Console.WriteLine ("NotificationsActivity");

			ImageView logo = FindViewById<ImageView> (Resource.Id.ActionBarLogo);
			logo.Visibility = ViewStates.Invisible;

			ImageView menu = FindViewById<ImageView> (Resource.Id.ActionBarMenu);
			menu.Visibility = ViewStates.Invisible;

			TextView title = FindViewById<TextView> (Resource.Id.ActionBarTitle);
			title.Text = "Notifications";

			Button back = FindViewById<Button> (Resource.Id.ActionBarBack);
			back.Click += delegate {
				Finish();
			};

			Switch getNotifications = FindViewById<Switch>(Resource.Id.onOffSwitchGetNotifications);
			getNotifications.Click += (o, e) => {
				// Perform action on clicks
				if (getNotifications.Checked)
					Toast.MakeText(this, "Checked", ToastLength.Short).Show ();
				else
					Toast.MakeText(this, "Not checked", ToastLength.Short).Show ();
			};

			Switch breakingStories = FindViewById<Switch>(Resource.Id.onOffSwitchBreakingStories);
			breakingStories.Click += (o, e) => {
				// Perform action on clicks
				if (breakingStories.Checked)
					Toast.MakeText(this, "Checked", ToastLength.Short).Show ();
				else
					Toast.MakeText(this, "Not checked", ToastLength.Short).Show ();
			};

			Switch newSources = FindViewById<Switch>(Resource.Id.onOffSwitchNewSources);
			newSources.Click += (o, e) => {
				// Perform action on clicks
				if (newSources.Checked)
					Toast.MakeText(this, "Checked", ToastLength.Short).Show ();
				else
					Toast.MakeText(this, "Not checked", ToastLength.Short).Show ();
			};

			Switch liveScoreEvents = FindViewById<Switch>(Resource.Id.onOffSwitchLiveScoreEvents);
			liveScoreEvents.Click += (o, e) => {
				// Perform action on clicks
				if (liveScoreEvents.Checked)
					Toast.MakeText(this, "Checked", ToastLength.Short).Show ();
				else
					Toast.MakeText(this, "Not checked", ToastLength.Short).Show ();
			};
			// Create your application here
		}
	}
}

