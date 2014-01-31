using System;
using System.Collections.Generic;
using System.Text;

namespace SportNet.Web.Models
{
    public class ProfileUpdateModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EMail { get; set; }
        public bool MakeProfilePublic { get; set; }
        public string PictureUrl { get; set; }
        public int Language { get; set; }
    }
}
