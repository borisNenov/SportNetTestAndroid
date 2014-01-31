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
	using System.Threading;

	[Activity (Label = "NewsActivity", ScreenOrientation = ScreenOrientation.Portrait)]			
	public class NewsActivity : Activity
	{
		ListView menuList;
		ListView newsList;

		//=========================================================================================
		private ImageView _slideLeftBtn;
		private View _leftMenu, _app;										// For the Sliding Menu
		private bool _menuLeftOut;
		private Context _context;
		//=========================================================================================

		CategoryModel model;
		List<CategoriesMenuModelItem> categories;

		RelativeLayout returnMenu;

		bool loader=true;
		int pagingPosition;
	
		private int category;
		private int page;
	
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.NewsScreen);
			Console.WriteLine ("NewsActivity");

			TextView title = FindViewById<TextView> (Resource.Id.ActionBarTitle);
			title.Visibility = ViewStates.Invisible;
			title.Text=Intent.GetStringExtra("SportName") ?? "SportNet";


			ImageView logo = FindViewById<ImageView> (Resource.Id.ActionBarLogo);
			logo.Visibility = ViewStates.Visible;

			Button back = FindViewById<Button> (Resource.Id.ActionBarBack);
			back.Click += (sender, args) => {
				var parent = (NewsGroupActivity)Parent;
				parent.OnBackPressed();
				//Finish();
			};

			newsList = FindViewById<ListView>(Resource.Id.NewsList);
			menuList = FindViewById<ListView> (Resource.Id.MenuMenu);

			ImageButton menu = FindViewById<ImageButton> (Resource.Id.ActionBarMenu);

			if (Globals.IsLoggedIn ()) {
				menu.Visibility = ViewStates.Invisible;
				back.Visibility = ViewStates.Visible;
				logo.Visibility = ViewStates.Invisible;
				title.Visibility = ViewStates.Visible;
			} else {
				menu.Visibility = ViewStates.Visible;
				back.Visibility = ViewStates.Invisible;
				logo.Visibility = ViewStates.Visible;
				title.Visibility = ViewStates.Invisible;
			}

			this.returnMenu = FindViewById<RelativeLayout> (Resource.Id.returnMenu);
			this.returnMenu.Visibility = ViewStates.Invisible;

			this.returnMenu.Click += (s, arg) => {
				RestoreMainViewObject();
			};


			//----------------------------------------------------------------------------------------------
			this.category = Intent.GetIntExtra ("Category", 0);
			getData (this.category, 0, true, true);
			this.page = 1;
			//----------------------------------------------------------------------------------------------

			newsList.ItemClick += (s, arg) => {
				if (!_menuLeftOut){
					var newsDetail = new Intent (this, typeof(NewsDetailActivity));
					string url = string.Format(RequestConfig.Article,model.News[arg.Position+3].SmallId);
					newsDetail.PutExtra ("MyCategory", model.News[arg.Position+3].Category);
					newsDetail.PutExtra("MySource", url);
					StartActivity (newsDetail);
				}else{
					RestoreMainViewObject();
				}
			};

			menuList.ItemClick += (s, arg) => {
				getData(categories[arg.Position].Link, 0, true, true);
				this.category=categories[arg.Position].Link;
				this.page=1;
			};

			newsList.Scroll += (object sender, AbsListView.ScrollEventArgs e) => {
				Console.WriteLine("{0}....",newsList.FirstVisiblePosition);
				if(loader) return;
				if(newsList.FirstVisiblePosition == this.pagingPosition)
				{
					//load new data here
					getPagedData (this.category, this.page);
					this.page++;
					this.pagingPosition += 25;
				}
			};

			_context = ApplicationContext;

			Globals.SetUserInfo (this);
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

			Button addContent = FindViewById<Button> (Resource.Id.AddContent);
			addContent.Click += delegate {
				var customizingSelection = new Intent (this, typeof(CustomizingSelectionActivity));
				StartActivity (customizingSelection);
			};

			DefineGui ();
		}

		void getData(int category, int page, bool isNewsUpdated, bool isMenuUpdated)
		{
			//----------------------------------------------------------------------------------------------
			var request = new RestRequest ();
			request.RequestFinished += (object sender, RequestEndedArgs e) => {
				var data = (CategoryModel)JsonConvert.DeserializeObject(e.Result, typeof(CategoryModel));
				model = data;
				// invoke it on the main thread
				RunOnUiThread(delegate{
					if(isNewsUpdated) {
						this.newsList.Adapter = new NewsScreenAdapter(this,model.News);
						this.pagingPosition = ((NewsScreenAdapter)this.newsList.Adapter).NewsModel.Count - 9;
						loader=false;
					}
					if(isMenuUpdated)
					{
						this.menuList.Adapter = new MainMenuListAdapter(this,model.Categories);
						this.categories = model.Categories;
						if(model.CategoryId != 0)
							this.categories.Insert(0, new CategoriesMenuModelItem{ Name="Back", Link=model.Parent.Link });
					}

				});
			};
			request.Send (string.Format (RequestConfig.News (), category, page), "GET");

			//----------------------------------------------------------------------------------------------
		}

		void getPagedData(int category,int page){
			var request = new RestRequest ();
			request.RequestFinished += (object sender, RequestEndedArgs e) => {
				var data = (List<NewsModelItem>)JsonConvert.DeserializeObject(e.Result, typeof(List<NewsModelItem>));
				RunOnUiThread(delegate{

					var adapter = this.newsList.Adapter as NewsScreenAdapter;
					adapter.NewsModel.AddRange(data);
					adapter.NotifyDataSetChanged();

				});
			};
			request.Send (string.Format (RequestConfig.NewsPaged, category, page), "GET");
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
			//animation.AnimationEnd += (sender, args) => _slideLeftBtn.SetImageResource(Resource.Drawable.menu);
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
				//_slideLeftBtn.SetImageResource(Resource.Drawable.menu);
			};
			this.returnMenu.Visibility = ViewStates.Invisible;
		}





	}

}

