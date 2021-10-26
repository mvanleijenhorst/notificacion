using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NotificacionApp.Common;
using NotificacionApp.Controllers.Common;
using NotificacionApp.Controllers.Students.Models;
using NotificacionApp.Domain;
using NotificacionApp.Services;

namespace NotificacionApp.Controllers.Students
{
    /// <summary>
    /// Student controller.
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly ILogger<StudentController> _logger;
        private readonly IStudentService _service;
        private readonly ISessionManager _sessionManager;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="logger">Logger</param>
        public StudentController(ILogger<StudentController> logger, IStudentService service, ISessionManager sessionManager)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _service = service ?? throw new ArgumentNullException(nameof(service));
            _sessionManager = sessionManager ?? throw new ArgumentNullException(nameof(sessionManager));
        }

        /// <summary>
        /// Add student.
        /// </summary>
        /// <param name="request"><see cref="AddStudentRequest"/></param>
        /// <returns><see cref="AddStudentResponse"/></returns>
        [Route("")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddStudentAsync(AddStudentRequest request)
        {
            var validation = request.Validate();
            if (validation.IsValid())
            {
                validation = await _service
                    .AddAsync(request.ToDomain())
                    .ConfigureAwait(false);
            }

            return validation.IsValid()
                ? Ok()
                : BadRequest(validation);
        }

        /// <summary>
        /// Remove student.
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns><see cref="RemoveStudentRequest"/></returns>
        [Route("{id}")]
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RemoveStudentAsync(RemoveStudentRequest request)
        {
            var validation = request.Validate();
            if (!validation.IsValid())
            {
                return BadRequest(validation);
            }

            validation = await StudentExistsAsync(request.Id);
            if (!validation.IsValid())
            {
                return NotFound();
            }

            validation = await _service
                .RemoveAsync(request.Id)
                .ConfigureAwait(false);

            return validation.IsValid()
                ? Ok()
                : BadRequest(validation);
        }

        /// <summary>
        /// Edit student.
        /// </summary>
        /// <param name="id">Id of the student</param>
        /// <param name="request"><see cref="UpdateQuestionRequest"/></param>
        /// <returns><see cref="EditStudentResponse"/></returns>
        [Route("{id}")]
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateStudentAsync(UpdateStudentRequest request)
        {
            var validation = request.Validate();
            if (!validation.IsValid())
            {
                return BadRequest(validation);
            }

            validation = await StudentExistsAsync(request.Id);
            if (!validation.IsValid())
            {
                return NotFound();
            }

            validation = await _service
                .UpdateAsync(request.ToDomain())
                .ConfigureAwait(false);

            return validation.IsValid()
                ? Ok()
                : BadRequest(validation);
        }

        /// <summary>
        /// Get single student details.
        /// </summary>
        /// <returns><see cref="ViewStudentResponse"/></returns>
        [Route("{id}")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetStudentAsync(GetStudentRequest request)
        {
            var validation = request.Validate();
            if (!validation.IsValid())
            {
                return BadRequest(validation);
            }

            try
            {
                var student = await _service
                    .GetByIdAsync(request.Id)
                    .ConfigureAwait(false);

                return Ok(new ViewStudentResponse(student.Id, student.Name, student.Username));
            }
            catch
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Get list of students.
        /// </summary>
        /// <returns><see cref="ViewStudentResponse"/></returns>
        [Route("")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetStudentsAsync()
        {
            var students = await _service
                .GetAllAsync()
                .ConfigureAwait(false);

            var list = students
                .Select(t => new ViewStudentResponse(t.Id, t.Name, t.Username))
                .ToList();

            return Ok(list);
        }

        /// <summary>
        /// Set password.
        /// </summary>
        /// <param name="request"><see cref="UpdateStudentPasswordRequest"/></param>
        /// <returns></returns>
        [Route("{id}/password")]
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SetPassword(UpdateStudentPasswordRequest request)
        {
            var validation = request.Validate();
            if (!validation.IsValid())
            {
                return BadRequest(validation);
            }

            validation = await _service.SetPasswordAsync(request.Id, request.Password)
                .ConfigureAwait(false);

            return validation.IsValid()
                ? Ok()
                : BadRequest(validation);
        }

        /// <summary>
        /// Check if student exists.
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns><see cref="ValidationResult"/></returns>
        private async Task<ValidationResult> StudentExistsAsync(Guid id)
        {
            var result = new ValidationResult();
            try
            {
                _ = await _service.GetByIdAsync(id).ConfigureAwait(false);
            }
            catch
            {
                result.NotFound(nameof(Student), id);
            }

            return result;
        }
    }
}
