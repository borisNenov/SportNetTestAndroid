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
	public class GalleryAdapter : BaseAdapter
	{
		private readonly Context context;
		//private int number;
		GalleryArticleModel galleryModel;

		public GalleryAdapter(Context c, GalleryArticleModel model)
		{
			context = c;
			//number = numberOfPictures;
			this.galleryModel = model;
		}

		public override int Count
		{
			//get { return number; }
			get { return galleryModel.Images.Count; }
		}

		public override Java.Lang.Object  GetItem(int position)
		{
			return null;
		}			

		public AdapterView.IOnItemClickListener OnItemClickListener { get; set; }

		public override long GetItemId(int position)
		{
			return 0;
		}
		public override View GetView(int position, View convertView, ViewGroup parent)
		{																
			View grid;

			if(convertView==null){
				grid = new View(context);
				LayoutInflater inflater= LayoutInflater.From(context);
				grid=inflater.Inflate(Resource.Layout.GalleryCustomView, parent, false);
			}else{
				grid = (View)convertView;
			}
			grid.FindViewById<ImageView> (Resource.Id.Frame).Visibility = ViewStates.Invisible;
			grid.FindViewById<ImageView> (Resource.Id.SportImage).SetUrlDrawable(galleryModel.Images[position], Resource.Drawable.SportNetNoPicBig);
			return grid;
		}

	}


}

