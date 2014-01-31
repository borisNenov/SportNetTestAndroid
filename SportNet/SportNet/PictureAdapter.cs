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

namespace SportNet
{
	public class PictureAdapter : BaseAdapter
	{
		private readonly Context context;
		private int number;

		public PictureAdapter(Context c, int numberOfPictures)
		{
			context = c;
			number = numberOfPictures;
		}

		public override int Count
		{
			get { return number; }
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
				grid=inflater.Inflate(Resource.Layout.PictureCustomView, parent, false);
			}else{
				grid = (View)convertView;
			}
			return grid;
		}

	}


}