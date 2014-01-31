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

namespace SportNet
{
	public class TodayLiveScoreAdapter : BaseAdapter<string> {


		Activity context;
		LiveScoreViewModel model;
		List<MatchModel> matches;
		public TodayLiveScoreAdapter(Activity context, LiveScoreViewModel model) : base() {
			this.context = context;
			this.model = model;
			matches = new List<MatchModel> ();
			foreach (var tournament in model.Tournaments) 
				foreach (var match in tournament.Matches) {
					matches.Add (match);
				}
			matches = matches.OrderBy (x => x.MatchTime).ToList ();
		}
		public override long GetItemId(int position)
		{
			return position;
		}
		public override string this[int position] {  
			get { return matches[position].Team1; }
		}
		public override int Count {
			get { 
				int count=0;
				for (int i = 0; i < model.Tournaments.Count; i++) {
					count = count + model.Tournaments [i].Matches.Count;
				}
				return count;
			}
		}
		public override View GetView(int position, View convertView, ViewGroup parent)
		{
			int n;
			View view = convertView; // re-use an existing view, if one is supplied
			if (view == null) // no view to re-use, create new
				view = context.LayoutInflater.Inflate (Resource.Layout.LiveScoreCustomView, null);
			view.FindViewById<TextView> (Resource.Id.teamOne).Text = matches[position].Team1;
			view.FindViewById<TextView> (Resource.Id.teamTwo).Text = matches[position].Team2;
			view.FindViewById<TextView> (Resource.Id.result).Text = matches[position].Result;
			//view.FindViewById<TextView> (Resource.Id.minutes).Text = string.Format("{0}'", currentPoint[position]);
			//view.FindViewById<TextView> (Resource.Id.startTIme).Text = start [position];
			if (matches [position].State == "Finished" || matches [position].State == "Ended") {	 			// FINISHED
				view.FindViewById<TextView> (Resource.Id.finished).Visibility = ViewStates.Visible;
				view.FindViewById<ImageView> (Resource.Id.Loader).Visibility = ViewStates.Invisible;
				view.FindViewById<TextView> (Resource.Id.result).Visibility = ViewStates.Visible;
				view.FindViewById<TextView> (Resource.Id.minutes).Visibility = ViewStates.Invisible;
				view.FindViewById<TextView> (Resource.Id.startTIme).Visibility = ViewStates.Invisible;
			} else if (matches [position].State == "Not started") { 											// HASNT STARTED
				view.FindViewById<TextView> (Resource.Id.finished).Visibility = ViewStates.Invisible;
				view.FindViewById<ImageView> (Resource.Id.Loader).Visibility = ViewStates.Invisible;
				view.FindViewById<TextView> (Resource.Id.result).Visibility = ViewStates.Visible;
				view.FindViewById<TextView> (Resource.Id.minutes).Visibility = ViewStates.Invisible;
				view.FindViewById<TextView> (Resource.Id.startTIme).Text = matches [position].MatchTime;
				view.FindViewById<TextView> (Resource.Id.startTIme).Visibility = ViewStates.Visible;
			} else if (int.TryParse (matches [position].State.Substring (0, 1), out n)) {						// LIVE NOW
				if (matches [position].State.Length >= 3) {
					matches [position].State = matches [position].State.Substring (0, 3);
				}
				view.FindViewById<TextView> (Resource.Id.finished).Visibility = ViewStates.Invisible;
				view.FindViewById<ImageView> (Resource.Id.Loader).Visibility = ViewStates.Visible;
				view.FindViewById<TextView> (Resource.Id.result).Visibility =ViewStates.Visible;
				view.FindViewById<TextView> (Resource.Id.minutes).Text = string.Format("{0}'", matches [position].State);
				view.FindViewById<TextView> (Resource.Id.minutes).Visibility = ViewStates.Visible;
				view.FindViewById<TextView> (Resource.Id.startTIme).Visibility = ViewStates.Invisible;

			} else {
				view.FindViewById<TextView> (Resource.Id.finished).Visibility = ViewStates.Invisible;
				view.FindViewById<ImageView> (Resource.Id.Loader).Visibility = ViewStates.Invisible;
				view.FindViewById<TextView> (Resource.Id.result).Visibility = ViewStates.Visible;
				view.FindViewById<TextView> (Resource.Id.minutes).Visibility = ViewStates.Invisible;
				view.FindViewById<TextView> (Resource.Id.startTIme).Text = matches [position].State;
				view.FindViewById<TextView> (Resource.Id.startTIme).Visibility = ViewStates.Visible;
			}
			return view;
		}
	}
}