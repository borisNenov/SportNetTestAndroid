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

namespace SportNet
{
	[Activity (Label = "LiveScoreTabActivity")]			
	public class LiveScoreTabActivity : TabActivity
	{
	
		public static void setTabColor(TabHost tabhost) {
			for(int i=0;i<tabhost.TabWidget.ChildCount;i++)
			{
				tabhost.TabWidget.GetChildAt(i).SetBackgroundColor(Android.Graphics.Color.Black); //unselected
				if (i==1) {
					tabhost.TabWidget.GetChildAt(i).SetBackgroundColor(Android.Graphics.Color.Red); // selected
				}
			}
		}
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.LiveScoreTab);
			Console.WriteLine ("LiveScoreTabActivity");




			CreateTab(typeof(TodayLiveScoreActivity), "today", "Today", Resource.Drawable.livescore_state);
			TabHost.SetCurrentTabByTag ("today");
			TabChangerListener.setTabColor(TabHost);
			TabHost.TabWidget.GetChildAt(0).Click += (object sender, EventArgs e) => {
				isSelected(0);
				setColor(TabHost);
				TabHost.SetCurrentTabByTag ("yesterday");
			};

			TabHost.TabWidget.GetChildAt(1).Click += (object sender, EventArgs e) => {
				isSelected(1);
				setColor(TabHost);
				TabHost.SetCurrentTabByTag ("today");
			};

			// Create your application here
		}
		private void CreateTab(Type activityType, string tag, string label, int drawableId )
		{
			var intent = new Intent(this, activityType);
			intent.AddFlags(ActivityFlags.NewTask);

			var spec = TabHost.NewTabSpec(tag);
			var drawableIcon = Resources.GetDrawable(drawableId);
			spec.SetIndicator(label, drawableIcon);
			spec.SetContent(intent);

			TabHost.AddTab(spec);
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

