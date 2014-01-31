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
using SportNet.Web.Models;
using Newtonsoft.Json; 
using UrlImageViewHelper;

namespace SportNet
{
	public class TabChangerListener : TabHost.IOnTabChangeListener
	{
		//
		// Properties
		//
		public IntPtr Handle
		{
			get;
			set;
		}
		public void Dispose()
		{
		}
		public TabHost TbHost { get; set; }
		public TabChangerListener(TabHost th)
		{
			TbHost = th;
		}
		public void OnTabChanged (string tabId)
		{
			setTabColor (TbHost);
		}  
		public static void setTabColor(TabHost tabhost) {
			for(int i=0;i<tabhost.TabWidget.ChildCount;i++)
			{
				tabhost.TabWidget.GetChildAt(i).SetBackgroundColor(Android.Graphics.Color.Black); //unselected
				if (tabhost.TabWidget.GetChildAt(i).Selected) {
					tabhost.TabWidget.GetChildAt(i).SetBackgroundColor(Android.Graphics.Color.Red); // selected
				}
			}
		}
	}



	[Activity (Label = "MainTabActivity", ScreenOrientation = ScreenOrientation.Portrait)]			
	public class MainTabActivity : TabActivity
	{		
		private ImageView localWebView;
		private AdModel model;
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.MainTab);
			Console.WriteLine ("MainTabActivity");

			if (Globals.IsLoggedIn ()) {
				CreateTab (typeof(NewsGroupActivity), "news", "News", Resource.Drawable.news_state);
			} else {
				CreateTab (typeof(NewsActivity), "news", "News", Resource.Drawable.news_state);
			}
			CreateTab(typeof(PictureGroupActivity), "pictures", "Pictures", Resource.Drawable.pictures_state);
			CreateTab(typeof(VideoActivity), "video", "Video", Resource.Drawable.video_state);
			CreateTab(typeof(TodayLiveScoreActivity), "livescore", "LiveScore", Resource.Drawable.livescore_state);
			this.localWebView = FindViewById<ImageView> (Resource.Id.AdWebView);
			getAdView ();


			TabHost.TabWidget.GetChildAt(0).Click += (object sender, EventArgs e) => {
				getAdView();
				isSelected(0);
				setColor(TabHost);
				TabHost.SetCurrentTabByTag ("news");
			};

			TabHost.TabWidget.GetChildAt(1).Click += (object sender, EventArgs e) => {
				getAdView();
				isSelected(1);
				setColor(TabHost);
				TabHost.SetCurrentTabByTag ("pictures");
			};

			TabHost.TabWidget.GetChildAt(2).Click += (object sender, EventArgs e) => {
				getAdView();
				isSelected(2);
				setColor(TabHost);
				TabHost.SetCurrentTabByTag ("video");
			};

			TabHost.TabWidget.GetChildAt(3).Click += (object sender, EventArgs e) => {
				getAdView();
				isSelected(3);
				setColor(TabHost);
				TabHost.SetCurrentTabByTag ("livescore");
			};	

			localWebView.Click += (s, arg) => {
				var uri = Android.Net.Uri.Parse (this.model.Impression);
				var intent = new Intent (Intent.ActionView, uri); 
				StartActivity (intent);  
			};
		}
		private void getAdView(){
			var request = new RestRequest ();
			request.RequestFinished += (object sender, RequestEndedArgs e) => {
				var data = (AdModel)JsonConvert.DeserializeObject(e.Result, typeof(AdModel));
				this.model=data;
				RunOnUiThread(delegate {
					localWebView.SetUrlDrawable(model.Image,Resource.Drawable.SportNetNoPicBig);
					//var requestAdPressed = new RestRequest ();
					//requestAdPressed.Send (this.model.Impression,"GET");
					//Console.WriteLine("Sent");

				});
			};
			request.Send (RequestConfig.Ads, "GET");

		}


		private void CreateTab(Type activityType, string tag, string label, int drawableId )
		{
			var intent = new Intent(this, activityType);
			//intent.AddFlags(ActivityFlags.NewTask);
			intent.AddFlags(ActivityFlags.ClearTop);

			var spec = TabHost.NewTabSpec(tag);

			var drawableIcon = Resources.GetDrawable(drawableId);
			spec.SetIndicator(label, drawableIcon);
			spec.SetContent(intent);

			TabHost.AddTab(spec);
			TabChangerListener.setTabColor(TabHost);
						
		}
		public static void setColor(TabHost tabhost) {
			for(int i=0;i<tabhost.TabWidget.ChildCount;i++)
			{
				tabhost.TabWidget.GetChildAt(i).SetBackgroundColor(Android.Graphics.Color.Black); //unselected
				if (tabhost.TabWidget.GetChildAt(i).Selected) {
					tabhost.TabWidget.GetChildAt(i).SetBackgroundColor(Android.Graphics.Color.Red); // selected
				}
			}
		}

		public void isSelected(int i){
			for (int j=0; j<TabHost.TabWidget.ChildCount; j++) {
				if (j == i)
					TabHost.TabWidget.GetChildAt (j).Selected = true;
				else
					TabHost.TabWidget.GetChildAt (j).Selected = false;
			}
				
		}



		
	}
}

