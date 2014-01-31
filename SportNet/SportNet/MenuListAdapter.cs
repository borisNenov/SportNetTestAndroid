
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
using SportNet.Web.Models.LiveScore;
using Newtonsoft.Json;


namespace SportNet
{
	public class MenuListAdapter: BaseAdapter<string> {
		List<LiveScoreSportModel> sport;
		List<LiveScoreCategoryModel> categories;
		int[] checkedItemPosition;
		Activity context;
		bool root;
		public int checkedPosition = -1;
		public MenuListAdapter(Activity context, List<LiveScoreSportModel> items ,bool root/*, int[] checkedPosition*/) : base() {
			this.context = context;
			this.sport = items;
			this.root = root;
		}
		public MenuListAdapter(Activity context, List<LiveScoreCategoryModel> items, bool root /*, int[] checkedPosition*/) : base() {
			this.context = context;
			this.categories = items;
			this.root = root;
		}
		public MenuListAdapter(Activity context, List<LiveScoreCategoryModel> items, bool root, int pos /*, int[] checkedPosition*/) : base() {
			this.context = context;
			this.categories = items;
			this.root = root;
			this.checkedPosition = pos;
		}

		public int IdAt(int position) {
			return categories [position].Id;
		}

		public override long GetItemId(int position)
		{
			return position;
		}
		public override string this[int position] {  
			get { if (root) {
					return sport [position].Name;
				}
				else {
				return categories [position].Name;
				}
			}
		}
		public override int Count {
			get {  
				if (root) {
					return sport.Count;
				} else {
					return categories.Count;
				}
			}
		}
		public List<LiveScoreCategoryModel> Categories {
			get {
				return categories;
			}
		}

		public override View GetView(int position, View convertView, ViewGroup parent)
		{
			View view = convertView; // re-use an existing view, if one is supplied
			if (view == null) // no view to re-use, create new
				view = context.LayoutInflater.Inflate (Resource.Layout.MenuCustomView, null);
			if (root) {
				view.FindViewById<TextView> (Resource.Id.Text1).Text = sport [position].Name;
				view.FindViewById<ImageView> (Resource.Id.Checkmark).Visibility = ViewStates.Invisible;
				view.FindViewById<TextView> (Resource.Id.Text1).SetTextColor (Android.Graphics.Color.Black);
			} else {
				view.FindViewById<TextView> (Resource.Id.Text1).Text = categories [position].Name;
				view.FindViewById<ImageView> (Resource.Id.Checkmark).Visibility = ViewStates.Invisible;
				view.FindViewById<TextView> (Resource.Id.Text1).SetTextColor (Android.Graphics.Color.Black);
				if (position == checkedPosition) {
					view.FindViewById<ImageView> (Resource.Id.Checkmark).Visibility = ViewStates.Visible;
					view.FindViewById<TextView> (Resource.Id.Text1).SetTextColor (Android.Graphics.Color.Red);
				}
			}

			/*if (position == currentPosition) {
				view.FindViewById<ImageView> (Resource.Id.Checkmark).Visibility = ViewStates.Visible;
				view.FindViewById<TextView> (Resource.Id.Text1).SetTextColor (Android.Graphics.Color.Red);
			} else {
				view.FindViewById<ImageView> (Resource.Id.Checkmark).Visibility = ViewStates.Invisible;
				view.FindViewById<TextView> (Resource.Id.Text1).SetTextColor (Android.Graphics.Color.Black);
			}*/

			return view;
		}

		//public AdapterView.IOnItemClickListener OnItemClickListener { get; set; }


	}
} 
