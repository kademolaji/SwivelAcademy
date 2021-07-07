using Microsoft.AspNetCore.Mvc;
using Swivel.Domain.Models;
using Swivel.Repository;
using Swivel.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Swivel.API.Controllers
{

    [Route("api/teacher")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly IRepositoryWrapper _repository;

        public TeacherController(ILoggerManager logger, IRepositoryWrapper repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult GetAllTeachers()
        {
            try
            {
                var teachers = _repository.Teacher.GetAllTeachers();

                _logger.LogInfo($"Returned all teachers from database.");

                return Ok(teachers);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllTeachers action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}", Name = "TeacherById")]
        public IActionResult GetTeacherById(int id)
        {
            try
            {
                var teacher = _repository.Teacher.GetTeacherById(id);

                if (teacher == null)
                {
                    _logger.LogError($"Teacher with id: {id}, was not found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned teacher with id: {id}");
                    return Ok(teacher);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetTeacherById action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public IActionResult CreateTeacher([FromBody] Teacher teacher)
        {
            try
            {
                if (teacher == null)
                {
                    _logger.LogError("Teacher object sent from client is null.");
                    return BadRequest("Teacher object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid Teacher object sent from client.");
                    return BadRequest("Invalid model object");
                }

                _repository.Teacher.CreateTeacher(teacher);

                return CreatedAtRoute("TeacherById", new { id = teacher.Id }, teacher);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateTeacher action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateTeacher(int id, [FromBody] Teacher teacher)
        {
            try
            {
                if (teacher == null)
                {
                    _logger.LogError("Teacher object sent from client is null.");
                    return BadRequest("Teacher object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid teacher object sent from client.");
                    return BadRequest("Invalid model object");
                }

                var dbTeacher = _repository.Teacher.GetTeacherById(id);
                if (dbTeacher == null)
                {
                    _logger.LogError($"Teacher with id: {id}, was not found in db.");
                    return NotFound();
                }

                _repository.Teacher.UpdateTeacher(dbTeacher, teacher);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateTeacher action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTeacher(int id)
        {
            try
            {
                var teacher = _repository.Teacher.GetTeacherById(id);
                if (teacher == null)
                {
                    _logger.LogError($"Teacher with id: {id}, was not found in db.");
                    return NotFound();
                }

                _repository.Teacher.DeleteTeacher(teacher);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeleteTeacher action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
