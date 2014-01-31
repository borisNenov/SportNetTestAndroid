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
using SportNet.Web.Models;
using Newtonsoft.Json;
using Android.Content.PM;


namespace SportNet
{
	[Activity (Label = "CustomizingSelectionActivity", ScreenOrientation = ScreenOrientation.Portrait)]			
	public class CustomizingSelectionActivity : Activity
	{
		private int index;
		private int index2;
		private GridView gridview; 

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.CustomizingSelction);
			Console.WriteLine ("CustomizingSelectionActivity");

			TextView title = FindViewById<TextView> (Resource.Id.ActionBarTitle);
			//string name = Intent.GetStringExtra ("MyName") ?? "Data not available";
			title.Text = string.Format("Welcome, {0} {1}",Globals.firstName ,Globals.lastName); 

			Button back = FindViewById<Button> (Resource.Id.ActionBarBack);
			back.Visibility = ViewStates.Invisible;

			ImageView menu = FindViewById<ImageView> (Resource.Id.ActionBarMenu);
			menu.Visibility = ViewStates.Invisible;

			ImageView logo = FindViewById<ImageView> (Resource.Id.ActionBarLogo);
			logo.Visibility = ViewStates.Invisible;

			this.index2 = index;
			var category = new Intent (this, typeof(CustomizingSelectionCategoryActivity));
			this.gridview = FindViewById<GridView> (Resource.Id.gridview);

			var request = new RestRequest ();
			request.RequestFinished += (object sender, RequestEndedArgs e) => {
				var data = (AddContentModel)JsonConvert.DeserializeObject(e.Result,typeof(AddContentModel));
				RunOnUiThread(delegate {
					this.gridview.Adapter =  new SelectionAdapter(this,data);
				});
			};
			request.Send(string.Format(RequestConfig.MainCategories,Globals.GetProfileId()),"GET");


			gridview.ItemClick += (s, arg) => {
				/*int pos = arg.Position;
				Console.WriteLine ("{0}", pos);
				if(this.selected[pos]==0){		//	Not Selected

					this.selected[pos]=1;
					category.PutExtra ("MyData", sports[pos]);
					StartActivity(category);

				} else	{						// Selected

					this.selected[pos]=0;
					this.index = gridview.FirstVisiblePosition;
					this.gridview.Adapter = new SelectionAdapter (this,thums,selected, sports);
					this.gridview.SetSelection (index);
				}*/
				category.PutExtra ("SportName",((SelectionAdapter)this.gridview.Adapter).Model.Categories[arg.Position].Name);
				category.PutExtra("SportId",((SelectionAdapter)this.gridview.Adapter).Model.Categories[arg.Position].Id.ToString());
				category.PutExtra("IsSportChecked",((SelectionAdapter)this.gridview.Adapter).Model.Categories[arg.Position].Checked);

				//((SelectionAdapter)this.gridview.Adapter).Model.Categories[arg.Position].Checked=!((SelectionAdapter)this.gridview.Adapter).Model.Categories[arg.Position].Checked;

				StartActivity(category);
			};
			Button startReading = FindViewById<Button> (Resource.Id.StartReading);
			startReading.Click += delegate {
				var main = new Intent (this, typeof(MainTabActivity));
				StartActivity (main);
			}; 
				
		}
		protected override void OnResume ()
		{
			base.OnResume ();
			Console.WriteLine("OnResume\n");

		}
		protected override void OnPause ()
		{
			base.OnPause ();
			Console.WriteLine("OnPause\n");

		}
		protected override void OnStop ()
		{
			this.index = gridview.FirstVisiblePosition;
			this.index2 = index;
			base.OnStop ();
			Console.WriteLine("OnStop\n");

		}
		protected override void OnRestart ()
		{
			//this.gridview.Adapter = new SelectionAdapter (this,thums,selected, sports);
			//this.gridview.SetSelection (index2);
			base.OnRestart ();
			Console.WriteLine("OnRestart\n");

		}
		protected override void OnStart ()
		{
			base.OnStart ();
			Console.WriteLine("OnStart\n");
			//this.gridview.SmoothScrollToPosition (index2);
			//Console.WriteLine("{0}",index2);
			Console.WriteLine();

		}
	}

}

