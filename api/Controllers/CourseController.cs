using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using api.Models;

namespace api.Controllers
{

    public interface ICourseController
    {
        Task<ActionResult<IEnumerable<Course>>> Getcourses();
        Task<ActionResult<Course>> GetCourse(int id);
        Task<IActionResult> PutCourse(int id, Course course);
        Task<ActionResult<Course>> PostCourse(Course course);
        Task<IActionResult> DeleteCourse(int id);
    }

    [Route("api/[controller]")]
    [Authorize(Roles = "Admin, Contributor")]
    [ApiController]
    public class CourseController : ControllerBase, ICourseController
    {
        private readonly DataContext _context;

        public CourseController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Course
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Course>>> Getcourses()
        {
            return await _context.courses.ToListAsync();
        }

        // GET: api/Course/5
        [HttpGet("{id}")]
        [AllowAnonymous]

        public async Task<ActionResult<Course>> GetCourse(int id)
        {
            var course = await _context.courses.FindAsync(id);

            if (course == null)
            {
                return NotFound();
            }

            return course;
        }

        // PUT: api/Course/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCourse(int id, Course course)
        {
            if (id != course.Id)
            {
                return BadRequest();
            }

            _context.Entry(course).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CourseExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Course
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Course>> PostCourse(Course course)
        {
            _context.courses.Add(course);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCourse", new { id = course.Id }, course);
        }

        // DELETE: api/Course/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            var course = await _context.courses.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }

            _context.courses.Remove(course);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CourseExists(int id)
        {
            return _context.courses.Any(e => e.Id == id);
        }
    }
}
