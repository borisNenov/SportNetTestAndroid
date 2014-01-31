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
using UrlImageViewHelper;

namespace SportNet
{
	public class SelectionAdapter : BaseAdapter
	{
		private readonly Context context;
		AddContentModel model;
		public AddContentModel Model {
			get {
				return model;
			}
			set {
				model = value;
			}
		} 

		public SelectionAdapter(Context c,AddContentModel model)
		{
			context = c;
			this.model = model;
		}

		public override int Count
		{
			get { return model.Categories.Count; }
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
				grid=inflater.Inflate(Resource.Layout.CustomizingSelectionCell, parent, false);
			}else{
				grid = (View)convertView;
			}
			grid.FindViewById<ImageView> (Resource.Id.SportImage).SetUrlDrawable ("http://www.sport.net" + model.Categories[position].Thumb, Resource.Drawable.SportNetNoPic);
			grid.FindViewById<TextView> (Resource.Id.Text).Text =model.Categories[position].Name;
			if (!model.Categories[position].Checked) {
				grid.FindViewById<ImageView> (Resource.Id.black).Visibility = ViewStates.Visible;
				grid.FindViewById<ImageView> (Resource.Id.red).Visibility = ViewStates.Invisible;
				grid.FindViewById<ImageView> (Resource.Id.plus).Visibility = ViewStates.Visible;
				grid.FindViewById<ImageView> (Resource.Id.check).Visibility = ViewStates.Invisible;
			} else {
				grid.FindViewById<ImageView> (Resource.Id.black).Visibility = ViewStates.Invisible;
				grid.FindViewById<ImageView> (Resource.Id.red).Visibility = ViewStates.Visible;
				grid.FindViewById<ImageView> (Resource.Id.plus).Visibility = ViewStates.Invisible;
				grid.FindViewById<ImageView> (Resource.Id.check).Visibility = ViewStates.Visible;
			}
			return grid;
		}
		





	}

}