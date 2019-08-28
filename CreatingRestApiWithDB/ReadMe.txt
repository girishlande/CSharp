
1. Start visual studio 
2. Select new project (language=C# and project type=Web)
3. Select ASP.Net Web application (.NET framework)
4. Select empty project and checkbox webapi 
5. Add Model class "student" with properties such as Id, Name, City 
(Right click on models and add class)

6. Right click on project and select "Manage Nuget packages"
Search "Entity framework"
Install that package. 

7. Add Database class StudentDbContext in models folder for storing students in corresponding students table. 
Derive it from DbContext which is present in System.Data.Entity
Add DbSet of Students as follows. 

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace RestApi.Models
{
    public class StudentDbContext : DbContext
    {
        public DbSet<Student> Students { get; set; }

    }
}


6. Add controller class "StudentsController" 
(Right click on controller folder and select add -> controller)
Select WebApi controller 2 - Empty)

7. Inside Controller class Add support for basic webapi operations on List elements (GET,POST,PUT,DELETE)
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


9. Run application and test application with api address 
e. g http:12345:/api/students
(this will invoke get operation)

Test all four operations using POSTMan software using following argument to add data
Add following in Body 
{
	"Id":4,
	"Name":"Pawar",
	"City":"Thane"
}
