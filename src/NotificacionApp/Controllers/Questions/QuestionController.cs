using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NotificacionApp.Common;
using NotificacionApp.Controllers.Common;
using NotificacionApp.Controllers.Students.Models;
using NotificacionApp.Domain;
using NotificacionApp.Services;

namespace NotificacionApp.Controllers.Questions
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class QuestionController : Controller
    {
        private readonly ISessionManager _sessionManager;
        private readonly IQuestionService _questionService;
        private readonly ITeacherService _teacherService;
        private readonly IStudentService _studentService;

        public QuestionController(ISessionManager sessionManager, 
            IQuestionService questionService, 
            ITeacherService teacherService,
            IStudentService studentService)
        {
            _sessionManager = sessionManager ?? throw new ArgumentNullException(nameof(sessionManager));
            _questionService = questionService ?? throw new ArgumentNullException(nameof(questionService));
            _teacherService = teacherService ?? throw new ArgumentNullException(nameof(teacherService));
            _studentService = studentService ?? throw new ArgumentNullException(nameof(studentService));
        }

        [Route("")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetQuestionsAsync()
        {
            var user = _sessionManager.SessionUser;
            if (user != null)
            {
                var data = await _questionService.GetAllAsync(user.Id, user.UserRole);
                return Ok(
                    data.Select(q => new ViewQuestionResponse(
                        q.Id, q.Room, q.Table, q.Topic, 
                        q.Teacher.Id, q.Teacher.Name,
                        q.Student.Id, q.Student.Name, 
                        q.CreateDateTime, q.ResponseDateTime, q.Response?.ToString())));
            }

            return Ok();
        }

        [Route("{id}")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetQuestionAsync(GetQuestionRequest request)
        {
            var user = _sessionManager.SessionUser;
            if (user == null)
            {
                return Unauthorized();
            }

            try
            {
                var data = await _questionService.GetByIdAsync(request.Id);
                if (data.Teacher.Id != user.Id && data.Student.Id != user.Id)
                {
                    return NotFound();
                }

                return Ok(new ViewQuestionResponse(
                        data.Id, data.Room, data.Table, data.Topic,
                        data.Teacher.Id, data.Teacher.Name,
                        data.Student.Id, data.Student.Name,
                        data.CreateDateTime, data.ResponseDateTime, data.Response?.ToString()));
            }
            catch
            {
                return NotFound();
            }
        }

        [Route("")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AddQuestionAsync(AddQuestionRequest request)
        {
            var user = _sessionManager.SessionUser;
            if (user == null)
            {
                return Unauthorized();
            }

            var validation = request.Validate();
            var student = await GetStudent(user.Id);
            var teacher = await GetTeacher(request.TeacherId);

            if (teacher == null)
            {
                validation.NotFound(nameof(Teacher), request.TeacherId);
            }

            if (student == null)
            {
                validation.NotFound(nameof(Student), user.Id);
            }

            if (!validation.IsValid())
            {
                return BadRequest(validation);
            }

            validation = await _questionService.AddAsync(request.ToDomain(teacher!, student!));

            if (validation.IsValid())
            {
                return Ok();
            }
            else
            {
                return BadRequest(validation);
            }
        }

        [Route("{id}")]
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateQuestionAsync(UpdateQuestionRequest request)
        {
            var user = _sessionManager.SessionUser;
            if (user == null)
            {
                return Unauthorized();
            }

            if (user.Id != request.StudentId && user.Id != request.TeacherId)
            {
                return Unauthorized();
            }

            var validation = request.Validate();
            var student = await GetStudent(request.StudentId);
            var teacher = await GetTeacher(request.TeacherId);

            if (teacher == null)
            {
                validation.NotFound(nameof(Teacher), request.TeacherId);
            }

            if (student == null)
            {
                validation.NotFound(nameof(Student), user.Id);
            }

            if (!validation.IsValid())
            {
                return BadRequest(validation);
            }

            validation = await _questionService.UpdateAsync(request.ToDomain(teacher!, student!));

            if (validation.IsValid())
            {
                return Ok();
            }
            else
            {
                return BadRequest(validation);
            }
        }

        [Route("{id}")]
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RemoveQuestionAsync(RemoveQuestionRequest request)
        {
            var user = _sessionManager.SessionUser;
            if (user == null)
            {
                return Unauthorized();
            }


            var validation = request.Validate();
            if (!validation.IsValid())
            {
                return BadRequest(validation);
            }

            validation = await _questionService.RemoveAsync(request.Id);

            if (validation.IsValid())
            {
                return Ok();
            }
            else
            {
                return BadRequest(validation);
            }
        }

        private async Task<Student?> GetStudent(Guid id)
        {
            try
            {
                return await _studentService.GetByIdAsync(id);
            }
            catch
            {
                return null;
            }
        }

        private async Task<Teacher?> GetTeacher(Guid id)
        {
            try
            {
                return await _teacherService.GetByIdAsync(id);
            }
            catch
            {
                return null;
            }
        }
    }
}
