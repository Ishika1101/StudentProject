using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StudentLibrary;
using StudentLibrary.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly StudentRepository repos;
        private readonly ILogger<StudentController> logger;

        public StudentController(StudentRepository repository,ILogger<StudentController> _logger)
        {
            repos = repository;
            logger = _logger;
        }

        /// <summary>
        /// Add Student
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        /// POST:api/Student
        [HttpPost]
        public ActionResult<Student> RegisterStudent(Student student)
        {
            Student obj=repos.AddStudent(student);
            logger.LogInformation($"Student {obj.StudentId} registered");
            return obj;
        }

        /// <summary>
        /// Get a student
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// GET:api/Student/1
        [HttpGet]
        [Route("{id}")]
        public ActionResult<Student> GetById(int id)
        {
            Student obj = repos.GetStudent(id);
            if (obj != null)
            {
                logger.LogInformation($"Get student{obj.StudentId} detail");
                return obj;
            }
            else
            {
                logger.LogError($"Student with {obj.StudentId} not found");
                return NotFound();
            }
        }

        /// <summary>
        /// Get Students
        /// </summary>
        /// <returns></returns>
        /// GET:api/Student
        [HttpGet]
       
        public ActionResult<List<Student>> GetStudents()
        {
            var students = repos.GetAllStudent().ToList();
            logger.LogInformation($"Get All students");
            return students;
        }

        /// <summary>
        /// Update Student
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        /// PUT:api/Student
        [HttpPut]
        public IActionResult Update(Student student)
        {
            repos.UpdateStudent(student);
            logger.LogInformation($"Student {student.StudentId} updated");
            return Ok();
        }

        /// <summary>
        /// Delete Student
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// DELETE:api/Student/1
        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete(int id)
        {
            repos.DeleteStudent(id);
            logger.LogInformation($"Student with Id {id} is Deleted");
            return Ok();
        }
    }
}
