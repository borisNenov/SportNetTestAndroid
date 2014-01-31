using System.Net;
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
using Android.Graphics;
using Android.Util;
using Newtonsoft.Json;


namespace SportNet
{
	public class RequestEndedArgs : EventArgs
	{
		public string Result { get; set; }
	}
	public class WebDownload : WebClient
	{
		/// <summary>
		/// Time in milliseconds
		/// </summary>
		public int Timeout { get; set; }

		public WebDownload() : this(60000) { }

		public WebDownload(int timeout)
		{
			this.Timeout = timeout;
		}

		protected override WebRequest GetWebRequest(Uri address)
		{
			var request = base.GetWebRequest(address);
			if (request != null)
			{
				request.Timeout = this.Timeout;
			}
			return request;
		}
	}
	public class RestRequest
	{
		public delegate void RequestFinish(object sender, RequestEndedArgs e);
		public event RequestFinish RequestFinished;

		public delegate void RequestFail (object sender, EventArgs e);
		public event RequestFail RequestFailed;

		public RestRequest ()
		{
		}

		public void Send(string url, string verb, object obj)
		{
			var webClient = new WebDownload ();
			webClient.Headers [HttpRequestHeader.ContentType] = "application/json";
			webClient.Timeout = 3000;
			webClient.UploadStringCompleted += this.uploadStringCompleted;

			var data = JsonConvert.SerializeObject (obj, Formatting.None);
			webClient.UploadStringAsync (new Uri(url), verb, data);
		}

		public void Send(string url, string verb)
		{
			var webClient = new WebDownload ();
			webClient.Timeout = 3000;
			webClient.DownloadStringCompleted += this.downloadStringCompleted;
			webClient.DownloadStringAsync (new Uri (url), verb);
			Console.WriteLine (url);
		}

		void uploadStringCompleted (object sender, UploadStringCompletedEventArgs e)
		{
			try {
				if (RequestFinished != null) {
					RequestFinished (this, new RequestEndedArgs { Result = e.Result });
				}
			}
			catch(Exception ex) {
				if (RequestFailed != null) {
					RequestFailed (this, null);
				}
				//InvokeOnMainThread (delegate {
				//	new UIAlertView ("Sorry", ex.Message, null, "OK", null).Show();
				//});
				//Toast.MakeText(this, "Sorry", ToastLength.Short).Show ();
			}
		}

		void downloadStringCompleted (object sender, DownloadStringCompletedEventArgs e)
		{
			try {
				if (RequestFinished != null) {
					RequestFinished (this, new RequestEndedArgs { Result = e.Result });
				}
			}
			catch(Exception) {
				if (RequestFailed != null) {
					RequestFailed (this, null);
				}
				//InvokeOnMainThread (delegate {
				//	new UIAlertView ("Sorry", ex.Message, null, "OK", null).Show();
				//});
				//Toast.MakeText(this, "Sorry", ToastLength.Short).Show ();
			}
		}
	}
}

