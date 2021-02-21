using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EmployeeWebApi.Models;

namespace EmployeeWebApi.Controllers
{
    public class EmployeesController : ApiController
    {
        public readonly ApplicationDbContext _db;

        public EmployeesController()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            _db = db;
        }

        public IEnumerable<Employee> Get()
        {
            return _db.Employees.ToList();
        }

        public Employee Get(int id)
        {
            return _db.Employees.FirstOrDefault(m => m.Id == id);
        }
    }
}
