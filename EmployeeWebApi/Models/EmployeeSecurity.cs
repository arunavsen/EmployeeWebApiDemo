using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeWebApi.Models
{
    public class EmployeeSecurity
    {
        public static ApplicationDbContext _db;

        public EmployeeSecurity(ApplicationDbContext db)
        {
            _db = db;
        }

        //Basic authentication is implemented
        public static bool Login(string username, string password)
        {
            return _db.Users.Any(m =>
                m.UserName.Equals(username, StringComparison.OrdinalIgnoreCase) && m.Password == password);
        }
    }
}