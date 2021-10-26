﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NotificacionApp.Controllers.Common;
using NotificacionApp.Controllers.Teachers.Models;
using NotificacionApp.Domain;
using NotificacionApp.Services;

namespace NotificacionApp.Controllers.Teachers
{
    /// <summary>
    /// Teacher controller.
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class TeacherController : ControllerBase
    {
        private readonly ILogger<TeacherController> _logger;
        private readonly ITeacherService _service;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="logger"></param>
        public TeacherController(ILogger<TeacherController> logger, ITeacherService service)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        /// <summary>
        /// Add teacher.
        /// </summary>
        /// <param name="request"><see cref="AddStudentRequest"/></param>
        /// <returns><see cref="AddTeacherResponse"/></returns>
        [Route("")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddTeacherAsync(AddStudentRequest request)
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
        /// Remove teacher.
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns><see cref="RemoveStudentRequest"/></returns>
        [Route("{id}")]
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RemoveTeacherAsync(RemoveStudentRequest request)
        {
            var validation = request.Validate();
            if (!validation.IsValid())
            {
                return BadRequest(validation);
            }

            validation = await TeacherExistsAsync(request.Id);
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
        /// Edit teacher.
        /// </summary>
        /// <param name="id">Id of the teacher</param>
        /// <param name="request"><see cref="EditStudentRequest"/></param>
        /// <returns><see cref="EditTeacherResponse"/></returns>
        [Route("{id}")]
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> EditTeacherAsync(EditStudentRequest request)
        {
            var validation = request.Validate();
            if (!validation.IsValid())
            {
                return BadRequest(validation);
            }

            validation = await TeacherExistsAsync(request.Id);
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
        /// Get single teacher details.
        /// </summary>
        /// <returns><see cref="ViewStudentResponse"/></returns>
        [Route("{id}")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetTeacherAsync(GetStudentRequest request)
        {
            var validation = request.Validate();
            if (!validation.IsValid())
            {
                return BadRequest(validation);
            }

            try
            {
                var teacher = await _service
                    .GetByIdAsync(request.Id)
                    .ConfigureAwait(false);

                return Ok(new ViewStudentResponse(teacher.Id, teacher.Name, teacher.Username));
            }
            catch
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Get list of teachers.
        /// </summary>
        /// <returns><see cref="ViewStudentResponse"/></returns>
        [Route("")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTeachersAsync()
        {
            var teachers = await _service
                .GetAllAsync()
                .ConfigureAwait(false);

            var list = teachers
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
        /// Check if teacher exists.
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns><see cref="ValidationResult"/></returns>
        private async Task<ValidationResult> TeacherExistsAsync(Guid id)
        {
            var result = new ValidationResult();
            try
            {
                _ = await _service.GetByIdAsync(id).ConfigureAwait(false);
            }
            catch
            {
                result.NotFound(nameof(Teacher), id);
            }

            return result;
        }

    }
}
