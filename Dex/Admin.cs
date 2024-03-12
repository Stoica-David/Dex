using Dex;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Dex
{
    public class Admin
    {
        public string Name { get; set; }
        public string Password { get; set; }

        private readonly JsonHandler jsonHandler;

        public Admin(string name, string password)
        {
            Name = name;
            Password = password;
            jsonHandler = new JsonHandler();
        }
    }
}
