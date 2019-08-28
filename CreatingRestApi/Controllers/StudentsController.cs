using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication10.Models;

namespace WebApplication10.Controllers
{
    public class StudentsController : ApiController
    {
        public static List<Student> _students = new List<Student>
        {
            new Student{Id=1,Name="Girish",City="Pune"},
            new Student{Id=2,Name="Ajit",City="Mumbai"},
            new Student{Id=3,Name="Suhas",City="Noida"}
        };

        public IEnumerable<Student> Get()
        {
            return _students;
        }


        public IHttpActionResult Get(int id)
        {
            var student = _students.FirstOrDefault((p) => p.Id == id);
            if (student == null)
            {
                return NotFound();
            }
            return Ok(student);
        }

        public void Post([FromBody]Student student)
        {
            _students.Add(student);
        }

        public void Put(int id, [FromBody]Student student)
        {
            int index = _students.FindIndex(x => x.Id == id);
            if (index>0)
            _students[index] = student;
        }

        public void Delete(int id)
        {
            int index = _students.FindIndex(x => x.Id == id);
            if (index > 0)
                _students.RemoveAt(index);
        }


    }
}
