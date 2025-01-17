﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using University1.DataAccess;
using University1.Models.DataModels;
using University1.Services;

namespace University1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly UniversityDBContext _context;

        private readonly ICourseServices _courseService;

        private readonly ILogger<CoursesController> _logger;

        public CoursesController(UniversityDBContext context, ICourseServices courseService, ILogger<CoursesController> logger)
        {
            _context = context;
            _courseService = courseService;
            _logger = logger;
        }

        // GET: api/Courses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Course>>> GetCourses()
        {
            _logger.LogWarning($"{nameof(CoursesController)} - {nameof(GetCourses)} Warning Level Log");
            _logger.LogError($"{nameof(CoursesController)} - {nameof(GetCourses)} Error Level Log");
            _logger.LogCritical($"{nameof(WeatherForecastController)} - {nameof(GetCourses)} Critical Level Log");

            if (_context.Courses == null)
          {
              return NotFound();
          }
            return await _context.Courses.ToListAsync();
        }

        // GET: api/Courses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Course>> GetCourse(int id)
        {
            _logger.LogWarning($"{nameof(CoursesController)} - {nameof(GetCourse)} Warning Level Log");
            _logger.LogError($"{nameof(CoursesController)} - {nameof(GetCourse)} Error Level Log");
            _logger.LogCritical($"{nameof(CoursesController)} - {nameof(GetCourse)} Critical Level Log");

            if (_context.Courses == null)
          {
              return NotFound();
          }
            var course = await _context.Courses.FindAsync(id);

            _courseService.GetCourseStudents();
            _courseService.GetCourseNoSyllabus();
            _courseService.GetCourseSyllabus();

            if (course == null)
            {
                return NotFound();
            }

            return course;
        }

        // PUT: api/Courses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<IActionResult> PutCourse(int id, Course course)
        {
            _logger.LogWarning($"{nameof(CoursesController)} - {nameof(PutCourse)} Warning Level Log");
            _logger.LogError($"{nameof(CoursesController)} - {nameof(PutCourse)} Error Level Log");
            _logger.LogCritical($"{nameof(CoursesController)} - {nameof(PutCourse)} Critical Level Log");

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

        // POST: api/Courses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<ActionResult<Course>> PostCourse(Course course)
        {
            _logger.LogWarning($"{nameof(CoursesController)} - {nameof(PostCourse)} Warning Level Log");
            _logger.LogError($"{nameof(CoursesController)} - {nameof(PostCourse)} Error Level Log");
            _logger.LogCritical($"{nameof(CoursesController)} - {nameof(PostCourse)} Critical Level Log");
            if (_context.Courses == null)
          {
              return Problem("Entity set 'UniversityDBContext.Courses'  is null.");
          }
            _context.Courses.Add(course);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCourse", new { id = course.Id }, course);
        }

        // DELETE: api/Courses/5
        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            _logger.LogWarning($"{nameof(CoursesController)} - {nameof(DeleteCourse)} Warning Level Log");
            _logger.LogError($"{nameof(CoursesController)} - {nameof(DeleteCourse)} Error Level Log");
            _logger.LogCritical($"{nameof(CoursesController)} - {nameof(DeleteCourse)} Critical Level Log");
            if (_context.Courses == null)
            {
                return NotFound();
            }
            var course = await _context.Courses.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }

            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CourseExists(int id)
        {
            return (_context.Courses?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
