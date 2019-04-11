using Cobblestone.Models;
using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Cobblestone.Controllers
{
    [RoutePrefix("employee")]
    public class EmployeeController : ApiController
    {
        private readonly EmployeeData _repository;

        public EmployeeController()
        {
            _repository = new EmployeeData();
        }

        [HttpGet, Route("{id:int}")]
        public Employee GetById(int id)
        {
            var dbEmployee = _repository.GetEmployee(id, out string error);
            if (dbEmployee == null) return new Employee();

            return new Employee
            {
                Id = dbEmployee.Id,
                FirstName = dbEmployee.FirstName,
                LastName = dbEmployee.LastName,
                Birthday = dbEmployee.Birthday,
                Age = dbEmployee.Age
            };
        }
    }
}


