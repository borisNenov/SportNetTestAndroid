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
	[Activity (Label = "GalleryActivity", ScreenOrientation = ScreenOrientation.Portrait)]			
	public class GalleryActivity : Activity
	{
		private GridView gridview; 
		private int pictures;
		private string heading;
		private string id;

		GalleryArticleModel model;


		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.Gallery);
			Console.WriteLine ("GalleryActivity");

			this.id = Intent.GetStringExtra ("MyModel") ?? "Data not available";

			this.pictures = Intent.GetIntExtra ("MyPicturesCount",0);
			TextView headline = FindViewById<TextView> (Resource.Id.GalleryHeading);

			this.heading = Intent.GetStringExtra ("MyHeading") ?? "Data not available";
			headline.Text = heading;

			TextView photoIndex = FindViewById<TextView> (Resource.Id.NumberOfPictures);
			photoIndex.Text = string.Format ("{0}", pictures);

			TextView title = FindViewById<TextView> (Resource.Id.ActionBarTitle);
			title.Text = Intent.GetStringExtra ("MyCategory") ?? "Data not available";

			ImageView logo = FindViewById<ImageView> (Resource.Id.ActionBarLogo);
			logo.Visibility = ViewStates.Invisible;

			ImageView menu = FindViewById<ImageView> (Resource.Id.ActionBarMenu);
			menu.Visibility = ViewStates.Invisible;

			Button back = FindViewById<Button> (Resource.Id.ActionBarBack);
			back.Click += delegate {
				var parent = (PictureGroupActivity)Parent;
				parent.OnBackPressed();
			};
			// Create your application here
			this.gridview = FindViewById<GridView> (Resource.Id.GalleryGridview);

			List<string> pictureModel = new List<string> ();

			//-------------------------------------------------------------------------------------------------------------
			var request = new RestRequest ();
			request.RequestFinished += (object sender, RequestEndedArgs e) => {
				var data = (GalleryArticleModel)JsonConvert.DeserializeObject(e.Result, typeof(GalleryArticleModel));
				model = data;
				pictureModel = model.Images;
				// invoke it on the main thread
				RunOnUiThread(delegate{
					gridview.Adapter = new GalleryAdapter(this,model);
				});
			};
			request.Send (string.Format(RequestConfig.PicturesArticle, this.id), "GET");
			//-------------------------------------------------------------------------------------------------------------

	


			gridview.ItemClick += (s, arg) => {
				gridview.GetChildAt(arg.Position).FindViewById<ImageView>(Resource.Id.Frame).Visibility=ViewStates.Visible;
				Console.WriteLine("Item at {0} position is clicked!",arg.Position);
				var picture = new Intent (this, typeof(PictureActivity));
				picture.PutExtra ("PicturesCount", this.pictures);
				picture.PutExtra("PicturePosition",arg.Position);
				picture.PutStringArrayListExtra("Model",pictureModel);


				StartActivity (picture);
			};
		}
		protected override void OnRestart ()
		{
			this.gridview.Adapter = new GalleryAdapter(this,model);
			base.OnRestart ();
		}
	}
}

