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
	class MainMenuListAdapter: BaseAdapter<string> {
		Activity context;

		List<CategoriesMenuModelItem> menuModel;
		public MainMenuListAdapter(Activity context,List<CategoriesMenuModelItem> model ) : base() {
			this.context = context;
			this.menuModel = model;
		}
		public override long GetItemId(int position)
		{
			return position;
		}
		public override string this[int position] {  
			get { return menuModel[position].Name; }
		}
		public override int Count {
			get { return menuModel.Count; }
		}

		public override View GetView(int position, View convertView, ViewGroup parent)
		{
			View view = convertView; // re-use an existing view, if one is supplied
			if (view == null) // no view to re-use, create new
				view = context.LayoutInflater.Inflate (Resource.Layout.MainMenuCustomView, null);
			view.FindViewById<TextView> (Resource.Id.MenuElement).Text = menuModel[position].Name;

			return view;
		}

	}
} 