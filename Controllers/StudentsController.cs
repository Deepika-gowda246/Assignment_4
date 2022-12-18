using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using StudentWebApi.Models;

namespace StudentWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        
        
            private readonly StudentsDatabaseContext _context;

            public StudentsController(StudentsDatabaseContext context)
            {
                _context = context;
            }


        // GET ALL STUDENT AND SUBJECT
        [HttpGet]
        public ActionResult<IEnumerable<Student>> GetStudent()
        {

            return _context.Students.Include(s => s.Subjects).ToList();

        }


        // GET STUDENT BY ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudent(int id)
        {
            var student = await _context.Students.Include(s => s.Subjects)
                                          .Where(s => s.StudentId == id)
                                          .FirstOrDefaultAsync();

            if (student == null)
            {
                return NotFound("Student ID Does not found");
            }

            return student;
        }



        //ADD STUDENT AND SUBJECT
        [HttpPost]
        public async Task<ActionResult<Student>> PostStudent(Student student)
        {
            _context.Students.Add(student);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStudent", new { id = student.StudentId }, student);
        }



        //UPDATE STUDENT AND SUBJECT

        [HttpPut("{Studentid}")]
        public async Task<IActionResult> PutStudentDetails(int studentid, Student student)
        {
            if (studentid != student.StudentId) { return BadRequest(); }
            
            _context.Students.Update(student);
            await _context.SaveChangesAsync();
            try{ await _context.SaveChangesAsync();}
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentExists(studentid)){  return NotFound();}
                else  { throw;}
            }

           
            return CreatedAtAction("GetStudent", new { studentid = student.StudentId }, student);

        }



        // DELETE BY STUDENT ID
        [HttpDelete("{id}")]
            public async Task<IActionResult> DeleteStudent(int id)
            {
                var student = await _context.Students.FindAsync(id);
                if (student == null) { return NotFound("Id does not found"); }

                _context.Students.Remove(student);
                await _context.SaveChangesAsync();

              return CreatedAtAction("GetStudent", new { id = student.StudentId }, student);
            }


            private bool StudentExists(int id)
            {
                return _context.Students.Any(e => e.StudentId == id);
            }

           
    }
}

