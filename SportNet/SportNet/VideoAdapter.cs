
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
	public class VideoAdapter : BaseAdapter<string> {
		Activity context;
		List<VideoModelItem> videoModel;

		public List<VideoModelItem> VideoModel {
			get {
				return videoModel;
			}
			set {
				videoModel = value;
			}
		} 
		public VideoAdapter(Activity context, List<VideoModelItem> categoryT) : base() {
			this.context = context;
			this.videoModel = categoryT;
		}
		public override long GetItemId(int position)
		{
			return position;
		}
		public override string this[int position] {  
			get { return videoModel[position].Title; }
		}
		public override int Count {
			get { return videoModel.Count; }
		}
		public override View GetView(int position, View convertView, ViewGroup parent)
		{
			View view = convertView; // re-use an existing view, if one is supplied
			if (view == null) // no view to re-use, create new
				view = context.LayoutInflater.Inflate (Resource.Layout.VideoCustomView, null);
			view.FindViewById<TextView> (Resource.Id.videoHeading).Text = videoModel [position].Title;
			view.FindViewById<TextView> (Resource.Id.Category).Text = videoModel[position].Category;
			view.FindViewById<ImageView> (Resource.Id.videoPic).SetUrlDrawable(videoModel[position].Thumbnail, Resource.Drawable.SportNetNoPic);
			return view;
		}
	}
}

