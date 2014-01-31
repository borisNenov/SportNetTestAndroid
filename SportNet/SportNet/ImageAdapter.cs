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
using com.refractored.monodroidtoolkit;
using Android.Animation;
using SportNet.Web.Models;
using Android.Webkit;
using Android.Net;
//using Bitmap = Android.Graphics.Bitmap;
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
	public class ImageAdapter : BaseAdapter
	{
		Context context;

		string[] pictureModel;
		public delegate void ScreenChangedEventHandler(object sender, EventArgs e);
		public ImageAdapter (Context c, string[] model)
		{
			context = c;
			this.pictureModel = model;
		}

		//public override int Count { get { return thumbIds.Length; } }

		public override int Count { get { return pictureModel.Length; } }

		public override Java.Lang.Object GetItem (int position)
		{
			return null;
		}

		public override long GetItemId (int position)
		{
			return 0;
		}

		// create a new ImageView for each item referenced by the Adapter
		public override View GetView (int position, View convertView, ViewGroup parent)
		{

			//TouchImageView image = new TouchImageView (context);
			//image.SetBackgroundResource (thumbIds[position]);
			//image.LayoutParameters = new Gallery.LayoutParams (850, 1000);	doestnt work
			//image.SetScaleType (ImageView.ScaleType.FitXy);
			//return image;

			ImageView i = new ImageView (context);

			i.SetUrlDrawable(pictureModel[position], Resource.Drawable.SportNetNoPicBig);
			i.LayoutParameters = new Gallery.LayoutParams (850, 1000);
			//i.SetScaleType (ImageView.ScaleType.FitXy);
			i.SetScaleType (ImageView.ScaleType.Center);

			return i;
		}


	}
}

