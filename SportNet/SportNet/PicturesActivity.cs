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
	[Activity (Label = "PicturesActivity", ScreenOrientation = ScreenOrientation.Portrait)]			
	public class PicturesActivity : Activity
	{
		ListView menuList;
		ListView picturesList;

		//=========================================================================================
		private ImageView _slideLeftBtn;
		private View _leftMenu, _app;										// For the Sliding Menu
		private bool _menuLeftOut;
		private Context _context;
		//=========================================================================================

		CategoryPicturesModel model;
		List<CategoriesMenuModelItem> categories;

		RelativeLayout returnMenu;

		bool loader=true;
		int pagingPosition;

		private int category;
		private int page;
		

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.Pictures);
			Console.WriteLine ("PicturesActivity");

			TextView title = FindViewById<TextView> (Resource.Id.ActionBarTitle);
			title.Visibility = ViewStates.Invisible;

			Button back = FindViewById<Button> (Resource.Id.ActionBarBack);
			back.Visibility = ViewStates.Invisible;

			picturesList = FindViewById<ListView>(Resource.Id.PicturesList);
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

			picturesList.ItemClick += (s, arg) => {
				if (!_menuLeftOut){
					var gallery = new Intent (this, typeof(GalleryActivity));
					gallery.PutExtra ("MyHeading", model.News[arg.Position].Title);
					gallery.PutExtra ("MyPicturesCount", model.News[arg.Position].PicturesCount);
					gallery.PutExtra ("MyCategory", model.News[arg.Position].Category);
					gallery.PutExtra ("MyModel",model.News[arg.Position].Id);
					gallery.AddFlags(ActivityFlags.ClearTop);

					var parent = (PictureGroupActivity)Parent;
					parent.StartChildActivity("picturesActivity", gallery);
				}else{
					RestoreMainViewObject();
				}
			};

			menuList.ItemClick += (s, arg) => {
				getData(categories[arg.Position].Link, 0, true, true);
				this.category=categories[arg.Position].Link;
				this.page=1;
			};

			picturesList.Scroll += (object sender, AbsListView.ScrollEventArgs e) => {
				Console.WriteLine("{0}....",picturesList.FirstVisiblePosition);
				if(loader) return;
				if(picturesList.FirstVisiblePosition == this.pagingPosition)
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
		void getData(int category, int page, bool isPicturesUpdated, bool isMenuUpdated)
		{
			//----------------------------------------------------------------------------------------------
			var request = new RestRequest ();
			request.RequestFinished += (object sender, RequestEndedArgs e) => {
				var data = (CategoryPicturesModel)JsonConvert.DeserializeObject(e.Result, typeof(CategoryPicturesModel));
				model = data;
				// invoke it on the main thread
				RunOnUiThread(delegate{
					if(isPicturesUpdated) {
						picturesList.Adapter = new PicturesAdapter(this,model.News);
						this.pagingPosition = ((PicturesAdapter)this.picturesList.Adapter).PicturesModel.Count - 9;
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
			request.Send (string.Format(RequestConfig.Pictures, category, page), "GET");
			//----------------------------------------------------------------------------------------------
		}

		void getPagedData(int category,int page){
			var request = new RestRequest ();
			request.RequestFinished += (object sender, RequestEndedArgs e) => {
				var data = (List<GalleryItemModel>)JsonConvert.DeserializeObject(e.Result, typeof(List<GalleryItemModel>));
				RunOnUiThread(delegate{

					var adapter = this.picturesList.Adapter as PicturesAdapter;
					adapter.PicturesModel.AddRange(data);
					adapter.NotifyDataSetChanged();

				});
			};
			request.Send (string.Format (RequestConfig.PicturesPaged, category, page), "GET");
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
				//_slideLeftBtn.SetImageResource(Resource.Drawable.menu);
			};
		}

	}
}

