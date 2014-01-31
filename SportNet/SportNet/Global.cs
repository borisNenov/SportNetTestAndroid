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
using SportNet.Web.Models;
using Newtonsoft.Json;

namespace SportNet
{
	[Application]
	public class Globals: Android.App.Application	{
		
		public static string id { get; set; }
		public static string firstName { get; set; }
		public static string lastName { get; set; }
		public static string email { get; set; }
		//public static ImageView image { get; set; }

		public string InstanceString { get; set; }

		public Globals(IntPtr handle, JniHandleOwnership transfer)
			: base(handle, transfer)
		{
		}

		public static bool IsLoggedIn()
		{
			var sdCardPath = Android.OS.Environment.ExternalStorageDirectory.Path;
			var filePath = System.IO.Path.Combine (sdCardPath, "user.txt");
			var filePath2 = System.IO.Path.GetFullPath(filePath);
			return (System.IO.File.Exists (filePath));
		}

		public static void SaveProfileId(string id)
		{
			var sdCardPath = Android.OS.Environment.ExternalStorageDirectory.Path;
			var filePath = System.IO.Path.Combine (sdCardPath, "user.txt");
			using (System.IO.StreamWriter writer = new System.IO.StreamWriter (filePath, true)) {
				writer.Write (id);
			}
		}

		public static int GetProfileId()
		{
			var sdCardPath = Android.OS.Environment.ExternalStorageDirectory.Path;
			var filePath = System.IO.Path.Combine (sdCardPath, "user.txt");
			int n = 0;
			if (!System.IO.File.Exists (filePath))
				return 0;
			int.TryParse (System.IO.File.ReadAllText(filePath), out n);
			return n;
		}

		public static void DeleteProfileId()
		{
			var sdCardPath = Android.OS.Environment.ExternalStorageDirectory.Path;
			var filePath = System.IO.Path.Combine (sdCardPath, "user.txt");
			System.IO.File.Delete(filePath);
		}

		public static void SetUserInfo(Activity activity)
		{
			var name = activity.FindViewById<TextView> (Resource.Id.MenuName);
			var settings = activity.FindViewById<Button> (Resource.Id.Settings);
			var addContent = activity.FindViewById<Button> (Resource.Id.AddContent);

			if (IsLoggedIn ()) {
				var request = new RestRequest ();
				request.RequestFinished += (object sender, RequestEndedArgs e) => {
					var data = (ProfileUpdateModel)JsonConvert.DeserializeObject(e.Result, typeof(ProfileUpdateModel));
					activity.RunOnUiThread(delegate {
						name.Text = data.FirstName + " " + data.LastName;
						settings.Text = "Logout";
						settings.Click += (object sendr, EventArgs ev) => {
							DeleteProfileId();
							var intent = new Intent(activity, typeof (MainActivity));
							intent.SetFlags(ActivityFlags.NewTask | ActivityFlags.ClearTask);
							activity.StartActivity(intent);
						};
						addContent.Visibility = ViewStates.Visible;
					});
				};
				request.Send (string.Format(RequestConfig.Profile, GetProfileId()), "GET");
			} 
			else {
				name.Text = "Welcome";
				settings.Text = "Login";
				settings.Click += (object sender, EventArgs e) => {
					var intent = new Intent(activity, typeof(MainActivity));
					intent.SetFlags(ActivityFlags.NewTask | ActivityFlags.ClearTask);
					activity.StartActivity(intent);		
				};
				addContent.Visibility = ViewStates.Invisible;
			}
		}
	}
}

