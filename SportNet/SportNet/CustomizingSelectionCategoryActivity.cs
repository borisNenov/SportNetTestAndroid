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
using Android.Provider;
using Android.Content.PM;
using SportNet.Web.Models;
using Newtonsoft.Json;


namespace SportNet
{
	[Activity (Label = "CustomizingSelectionCategoryActivity", ScreenOrientation = ScreenOrientation.Portrait)]			
	public class CustomizingSelectionCategoryActivity : Activity
	{
		string[] category;
		ListView listView;
		bool isAllFromSportChecked;

		List<AddContentItem> model;
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.CustomizingSelectionCategory);
			Console.WriteLine ("CustomizingSelectionCategoryActivity");

			TextView title = FindViewById<TextView> (Resource.Id.ActionBarTitle);
			//string name = Intent.GetStringExtra ("MyName") ?? "Data not available";
			title.Text = string.Format("{0}",Intent.GetStringExtra ("SportName") ?? "Data not available");
			string id = string.Format("{0}",Intent.GetStringExtra("SportId")?? "Data not available");
			isAllFromSportChecked = Intent.GetBooleanExtra ("IsSportChecked",false);
			Button back = FindViewById<Button> (Resource.Id.ActionBarBack);
			back.Click += delegate {
				Finish ();
			};

			ImageView menu = FindViewById<ImageView> (Resource.Id.ActionBarMenu);
			menu.Visibility = ViewStates.Invisible;

			ImageView logo = FindViewById<ImageView> (Resource.Id.ActionBarLogo);
			logo.Visibility = ViewStates.Invisible;

			listView = FindViewById<ListView>(Resource.Id.CustomizingCategory);
			//listView.Adapter = new CustomizingSelectionCategoryAdapter(this, category);
			var request = new RestRequest ();
			request.RequestFinished += (object sender, RequestEndedArgs e) => {
				var data = (AddContentModel)JsonConvert.DeserializeObject(e.Result, typeof(AddContentModel));
				RunOnUiThread(delegate {
					this.model=data.Categories;
					this.model.Insert(0,new AddContentItem{Name=string.Format("All from {0}",title.Text), HasChildren = false, Checked = this.isAllFromSportChecked, Id = data.ParentCategory.Id});
					listView.Adapter = new CustomizingSelectionCategoryAdapter(this,data.Categories,false);
				});
			};
			request.Send(string.Format(RequestConfig.SubCategories,id),"GET");

			var category = new Intent (this, typeof(CustomizingSelectionCategoryActivity));
			listView.ItemClick += (sender, e) => {
				IParcelable state = listView.OnSaveInstanceState();
				if(e.Position!=0){
				
					if(((CustomizingSelectionCategoryAdapter)this.listView.Adapter).Model[e.Position].HasChildren){
						category.PutExtra ("SportName",((CustomizingSelectionCategoryAdapter)this.listView.Adapter).Model[e.Position].Name);
						category.PutExtra("SportId",((CustomizingSelectionCategoryAdapter)this.listView.Adapter).Model[e.Position].Id.ToString());
						StartActivity(category);
					}else{
						saveChange(e.Position, state);
					}
				}else{
					saveChange(e.Position, state);
				}
			};
		}

		private void saveChange(int position, IParcelable state) {
			var current = ((CustomizingSelectionCategoryAdapter)this.listView.Adapter).Model[position];
			current.Checked = !current.Checked;

			listView.Adapter = new CustomizingSelectionCategoryAdapter(this,((CustomizingSelectionCategoryAdapter)this.listView.Adapter).Model,false);
			listView.OnRestoreInstanceState(state);

			var req = new RestRequest();
			req.Send(RequestConfig.AddContent, "POST", current);
			req.RequestFinished += (object sender, RequestEndedArgs e) => {
				RunOnUiThread(delegate {
					Console.WriteLine(e.Result);
				});
			};
		}
	}
}

