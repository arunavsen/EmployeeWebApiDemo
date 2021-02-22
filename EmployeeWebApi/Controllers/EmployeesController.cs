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

        public HttpResponseMessage Get(int id)
        {
            var entity = _db.Employees.FirstOrDefault(m => m.Id == id);
            if (entity!=null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, entity);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Employee with " + id + " is not found");
            }
        }

        public HttpResponseMessage Post([FromBody]Employee employee)
        {
            try
            {
                _db.Employees.Add(employee);
                _db.SaveChanges();

                var messege = Request.CreateResponse(HttpStatusCode.Created, employee);
                messege.Headers.Location = new Uri(Request.RequestUri + employee.Id.ToString());

                return messege;

            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e);
            }
            
        }
    }
}
