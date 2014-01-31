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
using Android.Content.PM;

namespace SportNet
{
	[Activity (Label = "PictureGroupActivity", ScreenOrientation = ScreenOrientation.Portrait)]			
	public class PictureGroupActivity : ActivityGroup
	{
		private List<string> _idList;

		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);

			_idList = new List<string>();

			StartChildActivity("pictures", new Intent(this, typeof(PicturesActivity)));
		}

		public void StartChildActivity(string id, Intent intent)
		{
			intent.AddFlags(ActivityFlags.ClearTop);
			var window = LocalActivityManager.StartActivity(id, intent);
			if (window != null)
			{
				_idList.Add(id);
				SetContentView(window.DecorView);
			}
		}

		public override void FinishActivityFromChild(Activity child, int requestCode)
		{
			var manager = LocalActivityManager;
			var index = _idList.Count - 1;

			if (index < 1)
			{
				Finish();
				return;
			}

			manager.DestroyActivity(_idList[index], true);
			_idList.RemoveAt(index);
			index--;
			var lastId = _idList[index];
			var lastIntent = manager.GetActivity(lastId).Intent;
			var newWindow = manager.StartActivity(lastId, lastIntent);
			SetContentView(newWindow.DecorView);     
		}

		public override bool OnKeyDown(Keycode keyCode, KeyEvent e)
		{
			if (keyCode == Keycode.Back)
			{
				return true;
			}
			return base.OnKeyDown(keyCode, e);
		}

		public override bool OnKeyUp(Keycode keyCode, KeyEvent e)
		{
			if (keyCode == Keycode.Back)
			{
				OnBackPressed();
				return true;
			}
			return base.OnKeyUp(keyCode, e);
		}

		public override void OnBackPressed()
		{
			var length = _idList.Count;
			if (length > 1)
			{

				var current = LocalActivityManager.GetActivity(_idList[length - 1]);
				FinishActivityFromChild(current, 0);
			}
		}
	}
}

