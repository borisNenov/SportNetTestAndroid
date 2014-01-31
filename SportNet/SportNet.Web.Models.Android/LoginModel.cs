using System;
using System.Collections.Generic;
using System.Text;

namespace SportNet.Web.Models
{
    public class LoginModel
    {        
        public string Email { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
