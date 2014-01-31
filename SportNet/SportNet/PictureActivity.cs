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
using Android.Graphics;
using Android.Animation;
using com.refractored.monodroidtoolkit;
using SportNet.Web.Models;
using Newtonsoft.Json;
using Android.Content.PM;


namespace SportNet
{
	[Activity (Label = "PictureActivity")]	
			
	public class PictureActivity : Activity
	{
		List<string> model;
		//public Android.Support.V4.View.ViewPager vPager;
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.PicturesMainCustomView);
			Console.WriteLine ("PictureActivity");


			this.model = Intent.GetStringArrayListExtra ("Model").ToList();
			ImageView logo = FindViewById<ImageView> (Resource.Id.ActionBarLogo);
			logo.Visibility = ViewStates.Invisible;

			ImageView menu = FindViewById<ImageView> (Resource.Id.ActionBarMenu);
			menu.Visibility = ViewStates.Invisible;

			Button back = FindViewById<Button> (Resource.Id.ActionBarBack);
			back.Click += delegate {
				Finish();	
			};

			Gallery gallery = (Gallery) FindViewById<Gallery>(Resource.Id.gallery);
			int numberOfPictures = Intent.GetIntExtra ("PicturesCount",0);
			gallery.Adapter = new ImageAdapter (this,model.ToArray());
			int position = Intent.GetIntExtra ("PicturePosition", 0);
			gallery.SetSelection (position);


			TextView title = FindViewById<TextView> (Resource.Id.ActionBarTitle);
			title.Text=string.Format("{0}/{1}", gallery.SelectedItemPosition+1,numberOfPictures);

			/*			g.setOnItemSelectedListener( new OnItemSelectedListener() {
				public void onItemSelected(AdapterView<?> arg0, View arg1, int pos, long arg3) {
					Log.v("Changed----->", ""+pos);
					change_position=pos;
				}

				public void onNothingSelected(AdapterView<?> arg0) {
					// TODO Auto-generated method stub
				}
			});	*/



			gallery.ItemSelected+= delegate(object sender, AdapterView.ItemSelectedEventArgs e) {
				title.Text=string.Format("{0}/{1}", gallery.SelectedItemPosition+1,numberOfPictures);
			};

			gallery.ItemClick += delegate (object sender, Android.Widget.AdapterView.ItemClickEventArgs args) {
				Console.WriteLine("The image has been clicked!!!");
			};

		} 
		
	}

}

