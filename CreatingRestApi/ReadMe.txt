
1. Start visual studio 
2. Select new project (language=C# and project type=Web)
3. Select ASP.Net Web application (.NET framework)
4. Select empty project and checkbox webapi 
5. Add Model class "student" with properties such as Id, Name, City 
(Right click on models and add class)
6. Add controller class "StudentsController" 
(Right click on controller folder and select add -> controller)
Select WebApi controller 2 - Empty)

7. Inside Controller class Add support for basic webapi operations on List elements (GET,POST,PUT,DELETE)
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

9. Run application and test application with api address 
e. g http:12345:/api/students
(this will invoke get operation)
http://localhost:58271/api/students/1

Test all four operations using POSTMan software using following argument to add data
Add following in Body 
{
	"Id":4,
	"Name":"Pawar",
	"City":"Thane"
}

10. You Don't need POST man to test you api. You can install swashbuckle from nuget package manager 
and then hit following address
http:12345:/swagger


NOTE 
You need to have IIS enabled on your windows machine

>>Enabling IIS and required IIS components on Windows 10
1. Open Control Panel and click Programs and Features > Turn Windows features on or off.
2. Enable Internet Information Services.
3. Expand the Internet Information Services feature and verify that the web server components listed in the next section are enabled.
Click OK.


// -----------------------
// Hosting the api 
// -----------------------

Start run prompt in windows 
>inetmgr

this will open IIS configuration manager 
Expand Sites -> Default web site 
Right click default web site and add virtual directory 
Give some alias
Then give path of the project where you have created above api 
then Right click on alias and select convert to application 
Now right click again -> manage application and Browe
Enter /api/student 

// -----------------------------------------
// Consuming the api in another application 
// -----------------------------------------

            text1.Content = "hello Girish";
            var client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost/girish1/");
            HttpResponseMessage response = await client.GetAsync("api/student");
            string result = await response.Content.ReadAsStringAsync();
            text1.Content = result;





