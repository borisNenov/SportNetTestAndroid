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
using Android.Animation;
using SportNet.Web.Models;
using Newtonsoft.Json;
using Android.Content.PM;


namespace SportNet
{
	[Activity (Label = "UserCategoryActivity", ScreenOrientation = ScreenOrientation.Portrait, MainLauncher = true)]			
	public class UserCategoryActivity : Activity
	{

		ListView menuList;
		string[] sports;
		int[] checkedPosition;
		
		string[] links;
		ListView newsList;

		string[] selectionCategory;
		ListView listView;

		//=========================================================================================
		private ImageView _slideLeftBtn;
		private View _leftMenu, _app;										// For the Sliding Menu
		private bool _menuLeftOut;
		private Context _context;
		//=========================================================================================

		CategoryModel model;
		List<NewsModelItem> news;

		RelativeLayout returnMenu;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.UserCategory);
			Console.WriteLine ("UserCategoryActivity");

			TextView title = FindViewById<TextView> (Resource.Id.ActionBarTitle);
			title.Visibility = ViewStates.Invisible;

			Button back = FindViewById<Button> (Resource.Id.ActionBarBack);
			back.Visibility = ViewStates.Invisible;

			Display display = WindowManager.DefaultDisplay;
			int width = display.Width;

			newsList = FindViewById<ListView>(Resource.Id.UserNewsList);
			listView = FindViewById<ListView>(Resource.Id.UserCustomizingCategory);
			menuList = FindViewById<ListView> (Resource.Id.MenuMenu);

			this.returnMenu = FindViewById<RelativeLayout> (Resource.Id.returnMenu);
			this.returnMenu.Visibility = ViewStates.Invisible;

			this.returnMenu.Click += (s, arg) => {
				RestoreMainViewObject();
			};

			var request = new RestRequest ();
			request.RequestFinished += (object sender, RequestEndedArgs e) => {

				Console.WriteLine("KOR v newsActivity 1");
				var data = (CategoryModel)JsonConvert.DeserializeObject(e.Result, typeof(CategoryModel));
				model = data;
				// invoke it on the main thread
				this.news = new List<NewsModelItem> {
					model.News[0],
					model.News[1],
					model.News[2],
					model.News[3]

				};
				RunOnUiThread(delegate{
					newsList.Adapter = new NewsScreenAdapter(this,news);
					listView.Adapter = new CustomizingSelectionCategoryAdapter (this, model.Categories,true);
					menuList.Adapter = new MainMenuListAdapter(this,model.Categories);
				});
			};
			request.Send (string.Format (RequestConfig.News(), 0, 0), "GET");


			newsList.ItemClick += (s, arg) => {
				var newsDetail = new Intent (this, typeof(NewsDetailActivity));
				string url = string.Format("{0}{1}",RequestConfig.Article ,news[arg.Position].SmallId);
				Console.WriteLine(url);
				newsDetail.PutExtra("MyCategory",url);
				StartActivity (newsDetail);
			};



			listView.ItemClick += (sender, e) => {
				var intent = new Intent(this, typeof (NewsActivity));
				intent.PutExtra("Category", model.Categories[e.Position].Link);
				intent.PutExtra("SportName", model.Categories[e.Position].Name);
				intent.AddFlags(ActivityFlags.ClearTop);
				var parent = (NewsGroupActivity)Parent;
				parent.StartChildActivity("newsActivity", intent);

			};

			menuList.ItemClick += (sender, e) => {
				var news = new Intent(this, typeof (NewsActivity));
				news.PutExtra("Category", model.Categories[e.Position].Link);
				news.PutExtra("SportName", model.Categories[e.Position].Name);
				news.AddFlags(ActivityFlags.ClearTop);
				var parent = (NewsGroupActivity)Parent;
				parent.StartChildActivity("newsActivity", news);

			};

			_context = ApplicationContext;

			/*TextView menuName = FindViewById<TextView> (Resource.Id.MenuName);
			menuName.Text = string.Format("{0} {1}",Globals.firstName ,Globals.lastName);

			ImageView menuImage = FindViewById<ImageView> (Resource.Id.menuImage);
		
			Button settings = FindViewById<Button> (Resource.Id.Settings);
			settings.Click += delegate {
				var accSettings = new Intent (this, typeof(AccountSettingsActivity));
				StartActivity (accSettings);
			};*/

			Globals.SetUserInfo (this);

			Button addContent = FindViewById<Button> (Resource.Id.AddContent);
			addContent.Click += (sender, args) => {
				var customizingSelection = new Intent (this, typeof(CustomizingSelectionActivity));
				StartActivity (customizingSelection);

			};

			DefineGui ();


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
