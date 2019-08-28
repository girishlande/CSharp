using RestApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RestApi.Controllers
{

    public class StudentsController : ApiController
    {
        private readonly StudentDbContext _context = new StudentDbContext();

        public IEnumerable<Student> Get()
        {
            return _context.Students;
        }


        public IHttpActionResult Get(int id)
        {
            var student = _context.Students.FirstOrDefault((p) => p.Id == id);
            if (student == null)
            {
                return NotFound();
            }
            return Ok(student);
        }

        public void Post([FromBody]Student student)
        {
            var s = _context.Students.FirstOrDefault((p) => p.Id == student.Id);
            if (s == null)
            {
                _context.Students.Add(student);
                _context.SaveChanges();
            }
        }

        public void Put(int id, [FromBody]Student student)
        {
            var dbStudent = _context.Students.FirstOrDefault((p) => p.Id == id);
            if (dbStudent != null)
            {
                dbStudent.City = student.City;
                dbStudent.Name = student.Name;
                _context.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            var dbStudent = _context.Students.FirstOrDefault((p) => p.Id == id);
            if (dbStudent != null)
            {
                _context.Students.Remove(dbStudent);
                _context.SaveChanges();
            }
        }
    }
}
