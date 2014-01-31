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
	public class CustomizingSelectionCategoryAdapter : BaseAdapter<string>{

		Activity context;
		string[] category;
		List<CategoriesMenuModelItem> categoryModel;
		List<AddContentItem> model;
		public List<AddContentItem> Model {
			get {
				return model;
			}
			set {
				model = value;
			}
		} 
		bool isCategoriesModel;
		public CustomizingSelectionCategoryAdapter(Activity context, string[] ctg) : base() {
			this.context = context;
			this.category = ctg;
		}
		public CustomizingSelectionCategoryAdapter(Activity context, List<CategoriesMenuModelItem> categoryT, bool isCategoriesModel) : base() {
			this.context = context;
			this.isCategoriesModel = isCategoriesModel;
			this.categoryModel = categoryT;

		}
		public CustomizingSelectionCategoryAdapter(Activity context, List<AddContentItem> categoryT, bool isCategoriesModel) : base() {
			this.context = context;
			this.isCategoriesModel = isCategoriesModel;
			this.model = categoryT;
		}


		public override long GetItemId(int position)
		{
			return position;
		}
		public override string this[int position] {  
			//get { return category[position]; }
			get {
				if (isCategoriesModel) {
					return categoryModel [position].Name;
				} else {
					return model [position].Name;
				}}
		}
		public override int Count {
			//get { return category.Length; }
			get {
				if (isCategoriesModel) {
					return categoryModel.Count;
				}else {
					return model.Count;
				} }
		}
		public override View GetView(int position, View convertView, ViewGroup parent)
		{
			View view = convertView; // re-use an existing view, if one is supplied
			if (view == null) // no view to re-use, create new
				view = context.LayoutInflater.Inflate (Resource.Layout.CustomizingSelectionCategoryCell, null);
			//view.FindViewById<TextView>(Resource.Id.categoryTitle).Text = category[position];
			if (isCategoriesModel) {
				view.FindViewById<TextView> (Resource.Id.categoryTitle).Text = categoryModel [position].Name;
				view.FindViewById<ImageView> (Resource.Id.plusWhite).Visibility = ViewStates.Invisible;
				view.FindViewById<ImageView> (Resource.Id.arrow).Visibility = ViewStates.Visible;
				view.FindViewById<ImageView> (Resource.Id.Checkmark).Visibility = ViewStates.Invisible;
			} else {
				view.FindViewById<TextView> (Resource.Id.categoryTitle).Text = model [position].Name;
				if (model [position].HasChildren) {
					view.FindViewById<ImageView> (Resource.Id.plusWhite).Visibility = ViewStates.Invisible;
					view.FindViewById<ImageView> (Resource.Id.arrow).Visibility = ViewStates.Visible;
					view.FindViewById<ImageView> (Resource.Id.Checkmark).Visibility = ViewStates.Invisible;
					view.FindViewById<TextView> (Resource.Id.categoryTitle).SetTextColor (Android.Graphics.Color.White);
					view.FindViewById<RelativeLayout> (Resource.Id.CellLayout).SetBackgroundColor(Color.ParseColor ("#333333"));
				} else {
					if (model [position].Checked) {
						view.FindViewById<ImageView> (Resource.Id.plusWhite).Visibility = ViewStates.Invisible;
						view.FindViewById<ImageView> (Resource.Id.arrow).Visibility = ViewStates.Invisible;
						view.FindViewById<ImageView> (Resource.Id.Checkmark).Visibility = ViewStates.Visible;
						view.FindViewById<TextView> (Resource.Id.categoryTitle).SetTextColor (Android.Graphics.Color.Red);
						view.FindViewById<RelativeLayout> (Resource.Id.CellLayout).SetBackgroundColor(Color.ParseColor ("#ffffff"));

					} else {
						view.FindViewById<ImageView> (Resource.Id.plusWhite).Visibility = ViewStates.Visible;
						view.FindViewById<ImageView> (Resource.Id.arrow).Visibility = ViewStates.Invisible;
						view.FindViewById<ImageView> (Resource.Id.Checkmark).Visibility = ViewStates.Invisible;
						view.FindViewById<TextView> (Resource.Id.categoryTitle).SetTextColor (Android.Graphics.Color.White);
						view.FindViewById<RelativeLayout> (Resource.Id.CellLayout).SetBackgroundColor(Color.ParseColor ("#333333"));
					}
				}
			}
			return view;
		}
	}
}

