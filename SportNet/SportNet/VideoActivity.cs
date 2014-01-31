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
using Newtonsoft.Json;
using Android.Content.PM;

namespace SportNet
{
	[Activity (Label = "VideoActivity", ScreenOrientation = ScreenOrientation.Portrait)]			
	public class VideoActivity : Activity
	{
		ListView menuList;
		ListView listView;

		//=========================================================================================
		private ImageView _slideLeftBtn;
		private View _leftMenu, _app;										// For the Sliding Menu
		private bool _menuLeftOut;
		private Context _context;
		//=========================================================================================

		CategoryVideoModel model;
		List<CategoriesMenuModelItem> categories;

		RelativeLayout returnMenu;

		bool loader=true;
		int pagingPosition;

		private int category;
		private int page;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.Video);
			Console.WriteLine ("VideoActivity");

			TextView title = FindViewById<TextView> (Resource.Id.ActionBarTitle);
			title.Visibility = ViewStates.Invisible;

			Button back = FindViewById<Button> (Resource.Id.ActionBarBack);
			back.Visibility = ViewStates.Invisible;

			listView = FindViewById<ListView> (Resource.Id.ListVideo);
			menuList = FindViewById<ListView> (Resource.Id.MenuMenu);

			this.returnMenu = FindViewById<RelativeLayout> (Resource.Id.returnMenu);
			this.returnMenu.Visibility = ViewStates.Invisible;

			this.returnMenu.Click += (s, arg) => {
				RestoreMainViewObject();
			};

			//----------------------------------------------------------------------------------------------
			getData (0, 0, true, true);
			this.page = 1;
			this.category = 0;
			//----------------------------------------------------------------------------------------------

			listView.ItemClick += (s, arg) => {
				if (!_menuLeftOut){
					var videoDetail = new Intent (this, typeof(VideoDetailActivity));
					videoDetail.PutExtra ("MyData",string.Format(RequestConfig.Video, model.News[arg.Position].Id));
					videoDetail.PutExtra("MyTitle", model.News[arg.Position].Category);
					StartActivity (videoDetail);
				}else{
					RestoreMainViewObject();
				}
			};

			menuList.ItemClick += (s, arg) => {
				getData(categories[arg.Position].Link, 0, true, true);
				this.category=categories[arg.Position].Link;
				this.page=1;
			};

			listView.Scroll += (object sender, AbsListView.ScrollEventArgs e) => {
				Console.WriteLine("{0}....",listView.FirstVisiblePosition);
				if(loader) return;
				if(listView.FirstVisiblePosition == this.pagingPosition)
				{
					//load new data here
					getPagedData (this.category, this.page);
					this.page++;
					this.pagingPosition += 25;
				}
			};

			_context = ApplicationContext;

			/*TextView menuName = FindViewById<TextView> (Resource.Id.MenuName);
			menuName.Text = string.Format("{0} {1}",Globals.firstName ,Globals.lastName);

			ImageView menuImage = FindViewById<ImageView> (Resource.Id.menuImage);

			Button settings = FindViewById<Button> (Resource.Id.Settings);
			settings.Click += delegate {
				var accSettings = new Intent (this, typeof(AccountSettingsActivity));
				StartActivity (accSettings);
			};


			if (!Globals.IsLoggedIn ()) {
				addContent.Visibility = ViewStates.Visible;

			} else {
				addContent.Visibility = ViewStates.Invisible;
			}*/
			Globals.SetUserInfo (this);

			Button addContent = FindViewById<Button> (Resource.Id.AddContent);
			addContent.Click += delegate {
				var customizingSelection = new Intent (this, typeof(CustomizingSelectionActivity));
				StartActivity (customizingSelection);
			};

			DefineGui ();


		}

		void getData(int category, int page, bool isVideoUpdated, bool isMenuUpdated)
		{
			//----------------------------------------------------------------------------------------------
			var request = new RestRequest ();
			request.RequestFinished += (object sender, RequestEndedArgs e) => {
				var data = (CategoryVideoModel)JsonConvert.DeserializeObject(e.Result, typeof(CategoryVideoModel));
				model = data;
				// invoke it on the main thread
				RunOnUiThread(delegate{
					if(isVideoUpdated) {
						this.listView.Adapter = new VideoAdapter(this,model.News);
						this.pagingPosition = ((VideoAdapter)this.listView.Adapter).VideoModel.Count - 9;
						loader=false;
					}
					if(isMenuUpdated)
					{
						this.menuList.Adapter = new MainMenuListAdapter(this,model.Categories);
						this.categories = model.Categories;
						if(model.CategoryId != 0)
							this.categories.Insert(0, new CategoriesMenuModelItem{ Name="All Sports", Link=model.Parent.Link });
					}
				});
			};
			request.Send (string.Format(RequestConfig.Videos, category, page), "GET");
			//----------------------------------------------------------------------------------------------
		}

		void getPagedData(int category,int page){
			var request = new RestRequest ();
			request.RequestFinished += (object sender, RequestEndedArgs e) => {
				var data = (List<VideoModelItem>)JsonConvert.DeserializeObject(e.Result, typeof(List<VideoModelItem>));
				RunOnUiThread(delegate{
					var adapter = this.listView.Adapter as VideoAdapter;
					adapter.VideoModel.AddRange(data);
					adapter.NotifyDataSetChanged();

				});
			};
			request.Send (string.Format (RequestConfig.VideosPaged, category, page), "GET");
		}




		void DefineGui ()
		{
			_app = FindViewById<LinearLayout> (Resource.Id.app);
			_leftMenu = FindViewById<View> (Resource.Id.left_menu);


			_slideLeftBtn = FindViewById<ImageView> (Resource.Id.ActionBarMenu);
			_slideLeftBtn.Click += delegate {
				LeftMenuAction ();
			};
		}

		void LeftMenuAction ()
		{
			if (!_menuLeftOut)
				ShowMenuObject();
			else 
				RestoreMainViewObject();
		}


		public override void OnBackPressed ()
		{
			if (_menuLeftOut)
				RestoreMainViewObject ();
			else
				base.OnBackPressed ();
		}

		void ShowMenuObject()
		{
			_leftMenu.Visibility = ViewStates.Visible;
			_menuLeftOut = true;

			var animation = ObjectAnimator.OfFloat(_app, "translationX", new[] { 0f, (_app.MeasuredWidth * 0.70f) });
			animation.SetDuration(700);
			animation.Start();
			this.returnMenu.Visibility = ViewStates.Visible;
		}

		private void RestoreMainViewObject()
		{
			_menuLeftOut = false;

			var animation = ObjectAnimator.OfFloat(_app, "translationX", new[] { (_app.MeasuredWidth * 0.70f), 0f });
			animation.SetDuration(700);
			animation.Start();
			animation.AnimationEnd += (sender, args) =>
			{
				_leftMenu.Visibility = ViewStates.Invisible;
			};
			this.returnMenu.Visibility = ViewStates.Invisible;
		}



	}
}

