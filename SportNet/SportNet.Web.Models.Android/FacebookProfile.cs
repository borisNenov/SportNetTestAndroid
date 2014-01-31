using System;

namespace SportNet.Web.Models
{
	public class FacebookProfile
	{
		public FacebookProfile ()
		{
		}

		public long Id { get; set; }
		public string Name { get; set; }
		public string first_name { get; set; }
		public string last_name { get; set; }
		public string Birthday { get; set; }
		public string Email { get; set; }
		public string UserName { get; set; }
	}
}

