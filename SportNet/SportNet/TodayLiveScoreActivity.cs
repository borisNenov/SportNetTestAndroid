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
using Android.Provider;
using SportNet.Web.Models;
using SportNet.Web.Models.LiveScore;
using Newtonsoft.Json;
using Android.Content.PM;

namespace SportNet
{
	[Activity (Label = "TodayLiveScoreActivity", ScreenOrientation = ScreenOrientation.Portrait)]			
	public class TodayLiveScoreActivity : Activity
	{

		ListView listView;
		ListView menuList;

		LiveScoreViewModel model;
		List<LiveScoreSportModel> sportModels;

		bool root;

		int currentSport = 0;
		int currentCategory = 0;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.TodayLiveScore);
			Console.WriteLine ("TodayLiveScoreActivity");

			this.root = true;

			var heading = FindViewById<TextView>(Resource.Id.Heading);

			TextView title = FindViewById<TextView> (Resource.Id.ActionBarTitle);
			title.Text = "Livescore";

			ImageView logo = FindViewById<ImageView> (Resource.Id.ActionBarLogo);
			logo.Visibility = ViewStates.Invisible;

			ImageView menu = FindViewById<ImageView> (Resource.Id.ActionBarMenu);
			menu.Visibility = ViewStates.Invisible;

			Button back = FindViewById<Button> (Resource.Id.ActionBarBack);
			back.Visibility = ViewStates.Invisible;

			listView = FindViewById<ListView>(Resource.Id.ListLiveScoreToday); // get reference to the ListView in the layout
			menuList = FindViewById<ListView> (Resource.Id.MenuList);

			//-------------------------------------------------------------------------------------------------------------------------
			var request = new RestRequest ();
			request.RequestFinished += (object sender, RequestEndedArgs e) => {
				var data = (LiveScoreViewModel)JsonConvert.DeserializeObject(e.Result, typeof(LiveScoreViewModel));
				model = data;
				// invoke it on the main thread
				RunOnUiThread(delegate{
					listView.Adapter = new TodayLiveScoreAdapter(this,model);
				});
			};
			request.Send (string.Format(RequestConfig.LiveScore, currentSport, currentCategory), "GET");
			//-------------------------------------------------------------------------------------------------------------------------*/
			request = new RestRequest ();
			request.RequestFinished += (object sender, RequestEndedArgs e) => {
				var data = (List<LiveScoreSportModel>)JsonConvert.DeserializeObject(e.Result, typeof(List<LiveScoreSportModel>));
				sportModels = data;
				// invoke it on the main thread
				RunOnUiThread(delegate{
					//checkedPosition = new int[sportModel];
					menuList.Adapter = new MenuListAdapter(this, sportModels,root);
				});
			};
			request.Send (string.Format(RequestConfig.LiveScoreSports), "GET");
			//-------------------------------------------------------------------------------------------------------------------------*/

			menuList.Visibility = ViewStates.Invisible;

			ImageView showPopMenu = FindViewById<ImageView> (Resource.Id.dropDownContainer);
			bool isMenuVisible = false;
			showPopMenu.Click += (s, arg) => {
				if(!isMenuVisible){
					menuList.Visibility = ViewStates.Visible;
					isMenuVisible = true;
				}
				else{
					menuList.Visibility = ViewStates.Invisible;
					isMenuVisible = false;
				}
				
			};

			listView.ItemClick += (s, arg) => {
				if(isMenuVisible){
					menuList.Visibility = ViewStates.Invisible;
					isMenuVisible = false;
				}
			};

			RelativeLayout listviewContainer = FindViewById<RelativeLayout> (Resource.Id.ListViewContainer);
			listviewContainer.Clickable = true;
			listviewContainer.Click += (object sender, EventArgs e) => {
				if(isMenuVisible){
					menuList.Visibility = ViewStates.Invisible;
					isMenuVisible = false;
				}
			};

			RelativeLayout sportContainer = FindViewById<RelativeLayout> (Resource.Id.SportContainer);
			sportContainer.Click += (object sender, EventArgs e) => {
				if(isMenuVisible){
					menuList.Visibility = ViewStates.Invisible;
					isMenuVisible = false;
				}
			};

			menuList.ItemClick+= (s, arg) => {		//	Root is TRUE by default
				if (arg.Position == 0 && !root) {	// When BACK cell is pressed
					//-------------------------------------------------------------------------------------------------------------------------
					Console.WriteLine("Back");

					request = new RestRequest ();
					request.RequestFinished += (object sender, RequestEndedArgs e) => {
						var data = (List<LiveScoreSportModel>)JsonConvert.DeserializeObject(e.Result, typeof(List<LiveScoreSportModel>));
						sportModels = data;
						// invoke it on the main thread
						RunOnUiThread(delegate{
							//checkedPosition = new int[sportModel];
							menuList.Adapter = new MenuListAdapter(this, sportModels,true);
							root = true;
						});
					};
					request.Send (string.Format(RequestConfig.LiveScoreSports), "GET");
					//-------------------------------------------------------------------------------------------------------------------------
					var request2 = new RestRequest ();
					request2.RequestFinished += (object sender, RequestEndedArgs e) => {
						RunOnUiThread(delegate {
							var data = (LiveScoreViewModel)JsonConvert.DeserializeObject(e.Result, typeof(LiveScoreViewModel));
							listView.Adapter = new TodayLiveScoreAdapter(this,data);
						});
					};
					request2.Send (string.Format (RequestConfig.LiveScore, 0, 0), "GET");
					heading.Text="All Sports";

					//-------------------------------------------------------------------------------------------------------------------------
				} 
				else {																			// When other cell is pressed		

					var request2 = new RestRequest ();

					if (root) {
						Console.WriteLine();
						Console.WriteLine("{0} position in ROOT",arg.Position);
						Console.WriteLine();															// When the other cell is in the root															
						request.RequestFinished += (object sender, RequestEndedArgs e) => {
							RunOnUiThread(delegate {
								var data = (List<LiveScoreCategoryModel>)JsonConvert.DeserializeObject(e.Result, typeof(List<LiveScoreCategoryModel>));
								data.Insert(0, new LiveScoreCategoryModel { Name = "Back" });

								menuList.Adapter = new MenuListAdapter(this,data,false);
								root=false;

							});
						};
						request.Send (string.Format (RequestConfig.LiveScoreCategories, sportModels[arg.Position].Id), "GET");

						request2.RequestFinished += (object sender, RequestEndedArgs e) => {
							RunOnUiThread(delegate {
								var data = (LiveScoreViewModel)JsonConvert.DeserializeObject(e.Result, typeof(LiveScoreViewModel));
								//	this.target.Source = new TodayTableSource(data);
								//this.target.ReloadData();
								currentSport=data.CurrentSport.Id;
								listView.Adapter = new TodayLiveScoreAdapter(this,data);
							});
						};
						request2.Send (string.Format (RequestConfig.LiveScore, sportModels[arg.Position].Id, 0), "GET");		
						heading.Text= sportModels[arg.Position].Name;

					
					} 
					else {
						Console.WriteLine();
						Console.WriteLine("{0} NOT in ROOT",arg.Position);
						Console.WriteLine();
						currentCategory=arg.Position;																	// When the other cell is not in the root
						request2.RequestFinished += (object sender, RequestEndedArgs e) => {
							RunOnUiThread(delegate {
								var data = (LiveScoreViewModel)JsonConvert.DeserializeObject(e.Result, typeof(LiveScoreViewModel));
								//	this.target.Source = new TodayTableSource(data);
								//this.target.ReloadData();
								listView.Adapter = new TodayLiveScoreAdapter(this,data);
								IParcelable state = menuList.OnSaveInstanceState();
								menuList.Adapter = new MenuListAdapter(this, ((MenuListAdapter)menuList.Adapter).Categories, false, arg.Position);
								menuList.OnRestoreInstanceState(state);
							});
						};

						request2.Send (string.Format (RequestConfig.LiveScore, currentSport, ((MenuListAdapter)menuList.Adapter).IdAt(arg.Position)), "GET");	

					}
					
				}






				/*
				currentSport = arg.Position;
				menuList.Adapter = new MenuListAdapter (this, sportModels,currentSport);
				Console.WriteLine("{0}",currentSport);
				menuList.Visibility= ViewStates.Invisible;

				*/
			};




		}
	}
}

