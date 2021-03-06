using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeWebApi.Models
{
    public class EmployeeSecurity
    {
        //Basic authentication is implemented
        public static bool Login(string username, string password)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                //var t = context.Users.Find(username);
                var x = context.Users.Any(m =>
                    m.UserName.Equals(username, StringComparison.OrdinalIgnoreCase) && m.Password == password);
                return x;
            }
        }
    }
}