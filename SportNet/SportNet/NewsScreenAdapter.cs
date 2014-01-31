
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
using Newtonsoft.Json;
using Android.Content.PM;


namespace SportNet
{

	class NewsScreenAdapter : BaseAdapter {


		List<NewsModelItem> newsModel;
		string[] mainNews;
		string[] mainCategories;
		string[] mainImages;
		string[] urls;
		string[] items;
		string[] news;
		Activity context;

		public List<NewsModelItem> NewsModel {
			get {
				return newsModel;
			}
			set {
				newsModel = value;
			}
		} 

		public NewsScreenAdapter(Activity context, string[] items, string[] news, int w, string[] links) : base() {
			this.context = context;
			this.items = items;
			this.news = news;
			this.urls = links;
			this.mainNews = new string[]{ this.items [0],
				this.items [1],
				this.items [2],
				this.items [3]};
			this.mainCategories = new string[]{ this.news[0],
				this.news [1],
				this.news [2],
				this.news [3]};
		}	

		public NewsScreenAdapter(Activity context, List<NewsModelItem> categoryT) : base() {
			this.context = context;
			this.newsModel = categoryT;

			//this.urls = links;
			if (newsModel.Count >= 4) {
				this.mainNews = new string[] { 
					newsModel [0].Title,
					newsModel [1].Title,
					newsModel [2].Title,
					newsModel [3].Title
				};
				this.mainCategories = new string[] { 
					newsModel [0].Category,
					newsModel [1].Category,
					newsModel [2].Category,
					newsModel [3].Category
				};
				this.mainImages = new string[] {
					this.newsModel [0].Img, 
					this.newsModel [1].Img,
					this.newsModel [2].Img,
					this.newsModel [3].Img

				};
			}
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
			//get { return news.Length; }
			get { return newsModel.Count-3; }

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
		private Bitmap GetImageBitmapFromUrl(string url)
		{
			Bitmap imageBitmap = null;

			using (var webClient = new WebClient())
			{
				var imageBytes = webClient.DownloadData(url);
				if (imageBytes != null && imageBytes.Length > 0)
				{
					imageBitmap = BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length);
				}
			}

			return imageBitmap;
		}

		public override View GetView(int position, View convertView, ViewGroup parent)
		{	
			if (position !=0) {
				View view = convertView;
				if (view == null) {
					view = context.LayoutInflater.Inflate (Resource.Layout.CustomView, null);
				} 

				view.FindViewById<TextView> (Resource.Id.TextCustom1).Text = newsModel[position+3].Title ;
				view.FindViewById<TextView> (Resource.Id.TextCustom2).Text = newsModel[position+3].Category;

				if (newsModel [position].Img== null) {
					view.FindViewById<ImageView> (Resource.Id.Image).SetImageResource(Resource.Drawable.SportNetNoPic);

				} else {
					 view.FindViewById<ImageView> (Resource.Id.Image).SetUrlDrawable(newsModel[position+3].Img, Resource.Drawable.SportNetNoPic);
					//var imgView = view.FindViewById<ImageView> (Resource.Id.Image);

				}

				return view;
			} else {
				View view;
				view = context.LayoutInflater.Inflate (Resource.Layout.MainCustomView,null);
				//Gallery gallery = (Gallery) FindViewById<Gallery>(Resource.Id.gallery);
				//gallery.Adapter = new ImageAdapter (this,numberOfPictures);
				//view.FindViewById<Gallery>(Resource.Id.mainGallery).Adapter = new ImageAdapter (context , 4);
				view.FindViewById<Gallery>(Resource.Id.mainGallery).Adapter = new ImageTextAdapter (context , 4, mainNews, mainCategories, mainImages);

				view.FindViewById<Gallery>(Resource.Id.mainGallery).ItemSelected+= delegate(object sender, AdapterView.ItemSelectedEventArgs e) {
					if(e.Position==0){
						view.FindViewById<ImageButton>(Resource.Id.firstIndicator).SetBackgroundResource(Resource.Drawable.activeDot);
						view.FindViewById<ImageButton>(Resource.Id.secondIndicator).SetBackgroundResource(Resource.Drawable.normalDot);
						view.FindViewById<ImageButton>(Resource.Id.thirdIndicator).SetBackgroundResource(Resource.Drawable.normalDot);
						view.FindViewById<ImageButton>(Resource.Id.fourthIndicator).SetBackgroundResource(Resource.Drawable.normalDot);
					}
					if(e.Position==1){
						view.FindViewById<ImageButton>(Resource.Id.firstIndicator).SetBackgroundResource(Resource.Drawable.normalDot);
						view.FindViewById<ImageButton>(Resource.Id.secondIndicator).SetBackgroundResource(Resource.Drawable.activeDot);
						view.FindViewById<ImageButton>(Resource.Id.thirdIndicator).SetBackgroundResource(Resource.Drawable.normalDot);
						view.FindViewById<ImageButton>(Resource.Id.fourthIndicator).SetBackgroundResource(Resource.Drawable.normalDot);
					}
					if(e.Position==2){
						view.FindViewById<ImageButton>(Resource.Id.firstIndicator).SetBackgroundResource(Resource.Drawable.normalDot);
						view.FindViewById<ImageButton>(Resource.Id.secondIndicator).SetBackgroundResource(Resource.Drawable.normalDot);
						view.FindViewById<ImageButton>(Resource.Id.thirdIndicator).SetBackgroundResource(Resource.Drawable.activeDot);
						view.FindViewById<ImageButton>(Resource.Id.fourthIndicator).SetBackgroundResource(Resource.Drawable.normalDot);
					}
					if(e.Position==3){
						view.FindViewById<ImageButton>(Resource.Id.firstIndicator).SetBackgroundResource(Resource.Drawable.normalDot);
						view.FindViewById<ImageButton>(Resource.Id.secondIndicator).SetBackgroundResource(Resource.Drawable.normalDot);
						view.FindViewById<ImageButton>(Resource.Id.thirdIndicator).SetBackgroundResource(Resource.Drawable.normalDot);
						view.FindViewById<ImageButton>(Resource.Id.fourthIndicator).SetBackgroundResource(Resource.Drawable.activeDot);
					}
				};

				view.FindViewById<Gallery>(Resource.Id.mainGallery).ItemClick += delegate (object sender, Android.Widget.AdapterView.ItemClickEventArgs args) {
					//Console.WriteLine("Article number {0} was selected",view.FindViewById<Gallery>(Resource.Id.mainGallery).SelectedItemPosition+1);
					var newsDetail = new Intent (context, typeof(NewsDetailActivity));
					string url = string.Format(RequestConfig.Article, newsModel[args.Position].SmallId);
					newsDetail.PutExtra ("MyCategory", newsModel[args.Position].Category);
					newsDetail.PutExtra("MySource", url);
					context.StartActivity (newsDetail);
				};

				return view;
			}	
		}
	}
}

