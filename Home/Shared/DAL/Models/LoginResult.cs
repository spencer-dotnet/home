using System;
using System.Collections.Generic;
using System.Text;

namespace Home.Shared.DAL.Models
{
    public class LoginResult
    {
        public bool Successful { get; set; }
        public string Error { get; set; }
        public string Token { get; set; }
    }
}
