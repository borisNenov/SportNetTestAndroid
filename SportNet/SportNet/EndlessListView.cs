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
	class EndlessListView : AbsListView.IOnScrollListener
	{
		private ListView list;
		public EndlessListView(ListView list) {
			this.list = list;
		}
		public IntPtr Handle { get; private set; }

		public void OnScroll(AbsListView view, int firstVisibleItem, int visibleItemCount, int totalItemCount) {
			Console.WriteLine ("SCROLL........................................");             
		}
		public void OnScrollStateChanged(AbsListView view, ScrollState state){
			Console.WriteLine ("SCROLL STATE CHANGED........................................");  
		}

		public void Dispose() {}
	}
}

