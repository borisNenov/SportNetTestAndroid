using System;
using Android.Animation;
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
using Android.Webkit;
using Android.Net;
//using Bitmap = Android.Graphics.Bitmap;
using Java.Net;
using Android.Graphics;
using Java.IO;
using Android.Graphics.Drawables;
using Android.Util;
using System.Net;
using System.IO;
using UrlImageViewHelper;

namespace SportNet
{
	class ImageTextAdapter : BaseAdapter {
		Activity context;
		int picturesCount;
		string[] headlines;
		string[] categories;
		string[] images;
	
		public delegate void ScreenChangedEventHandler(object sender, EventArgs e);
		public ImageTextAdapter (Activity c, int numberOfPictures, string[] headings, string[] category, string[] images) : base() {
			this.context = c;
			this.picturesCount = numberOfPictures;
			this.headlines = headings;
			this.categories = category;
			this.images = images;
		}
		public override int Count { get { return picturesCount; } }

		public override Java.Lang.Object GetItem (int position)
		{
			return null;
		}
	
		public override long GetItemId (int position)
		{
			return position;
		}


		// create a new ImageView for each item referenced by the Adapter
		public override View GetView (int position, View convertView, ViewGroup parent)
		{
			View view = convertView;
			view = context.LayoutInflater.Inflate (Resource.Layout.ImageTextView,  parent, false);
			//cell.FindViewById<View> (Resource.Layout.ImageTextView);

			if (images[position]== null) {
				view.FindViewById<ImageView> (Resource.Id.mainPicture).SetImageResource(Resource.Drawable.SportNetNoPicBig);
			} else {
				//var imageBitmap = GetImageBitmapFromUrl (images[position]);
				//view.FindViewById<ImageView> (Resource.Id.mainPicture).SetImageBitmap (imageBitmap);

				view.FindViewById<ImageView> (Resource.Id.mainPicture).SetUrlDrawable (images [position], Resource.Drawable.SportNetNoPicBig);
			}
			view.FindViewById<TextView> (Resource.Id.mainHeading).Text= headlines[position];
			view.FindViewById<TextView> (Resource.Id.mainCategory).Text = categories [position];
			return view;		

		}

	}
}
