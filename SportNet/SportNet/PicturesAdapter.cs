
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
	class PicturesAdapter : BaseAdapter {
		string[] items;
		string[] news;
		int[] numberOfPictures;
		List<GalleryItemModel> picturesModel;
		Activity context;

		public List<GalleryItemModel> PicturesModel {
			get {
				return picturesModel;
			}
			set {
				picturesModel = value;
			}
		} 

		public PicturesAdapter(Activity context, string[] items, string[] news, int[] number) : base() {
			this.context = context;
			this.items = items;
			this.news = news;
			this.numberOfPictures = number;

		}
		public PicturesAdapter(Activity context,List<GalleryItemModel> categoryT ) : base() {
			this.context = context;
			this.picturesModel = categoryT;

		}
		public override long GetItemId(int position)
		{
			return position;
		}
		public override Java.Lang.Object  GetItem(int position)
		{
			return null;
		}
		//		public override string this[int position] {  
		//			get { return items[position]; }
		//		}
		public override int Count {
			//get { return items.Length; }
			get { return picturesModel.Count; }
		}

		public override int ViewTypeCount {
			get {
				return 2;
			}
		}
		public override int GetItemViewType (int position)
		{
			if (position == 0){
				return 0;
			} else {
				return 1;
			}
		}

		public override View GetView(int position, View convertView, ViewGroup parent)
		{	
			if (position != 0) {
				View view = convertView;
				if (view == null) {
					view = context.LayoutInflater.Inflate (Resource.Layout.PicturesCustomView, parent, false);
				}
				view.FindViewById<TextView> (Resource.Id.Text1).Text = picturesModel[position].Title;
				view.FindViewById<TextView> (Resource.Id.Text2).Text = picturesModel[position].Category;
				view.FindViewById<TextView> (Resource.Id.NumberOfPhotos).Text = string.Format ("{0}", picturesModel[position].PicturesCount);
				view.FindViewById<ImageView> (Resource.Id.Image).SetUrlDrawable(picturesModel[position].Thumbnail, Resource.Drawable.SportNetNoPic);
				return view;
			} else {
				View view = convertView;
				if (view == null) {
					view = context.LayoutInflater.Inflate (Resource.Layout.MainPicturesCustomView, parent, false);
				}
				view.FindViewById<TextView> (Resource.Id.mainHeading).Text = picturesModel[position].Title;
				view.FindViewById<TextView> (Resource.Id.mainCategory).Text = picturesModel[position].Category;
				view.FindViewById<TextView> (Resource.Id.NumberOfPhotos).Text = string.Format ("{0}", picturesModel[position].PicturesCount);
				view.FindViewById<ImageView> (Resource.Id.mainPicture).SetUrlDrawable(picturesModel[position].Thumbnail, Resource.Drawable.SportNetNoPicBig);
				return view;
			}
		}
	}
}

