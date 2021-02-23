using System;
using System.Collections.Generic;
using System.Data.Entity;
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

        [HttpGet]
        public HttpResponseMessage LoadAllEmployees(string gender="All")
        {
            if (gender == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest,
                    "Value for gender must be All, Male or Female. " + gender + " is invalid");
            }
            else
            {
                switch (gender.ToLower())
                {
                    case "all":
                        return Request.CreateResponse(HttpStatusCode.OK, _db.Employees.ToList());
                    case "male":
                        return Request.CreateResponse(HttpStatusCode.OK,
                            _db.Employees.Where(m => m.Gender.ToLower() == "male").ToList());
                    case "female":
                        return Request.CreateResponse(HttpStatusCode.OK,
                            _db.Employees.Where(m => m.Gender.ToLower() == "female").ToList());
                    default:
                        return Request.CreateErrorResponse(HttpStatusCode.BadRequest,
                            "Value for gender must be All, Male or Female. " + gender + " is invalid");
                }
            }
            
        }

        [HttpGet]
        public HttpResponseMessage LoadEmployeeById(int id)
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

        [HttpPost]
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

        [HttpDelete]
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                var employee = _db.Employees.FirstOrDefault(m => m.Id == id);

                if (employee == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Employee with Id= " + id.ToString(
                                                                                ) + " not found");
                }
                else
                {
                    _db.Employees.Remove(employee);
                    _db.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e);

            }
        }

        [HttpPut]
        public HttpResponseMessage Put(int id, [FromBody] Employee employee)
        {
            try
            {
                var entity = _db.Employees.FirstOrDefault(m => m.Id == id);

                if (entity == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                        "Employee with Id = " + id.ToString() + " not found to update");
                }
                else
                {
                    entity.FirstName = employee.FirstName;
                    entity.LastName = employee.LastName;
                    entity.Gender = employee.Gender;
                    entity.Salary = employee.Salary;

                    _db.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK, entity);
                }
                
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e);
            }
            
        }
    }
}
