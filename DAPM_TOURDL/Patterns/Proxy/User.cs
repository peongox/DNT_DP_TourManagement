using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DAPM_TOURDL.Patterns.Proxy
{
    public class User
    {
        private string username;
        private string password;

        public string Username { get => username; set => username = value; }
        public string Password { get => password; set => password = value; }

        public User(string username, string password)
        {
            this.username = username;
            this.password = password;
        }
    }
}