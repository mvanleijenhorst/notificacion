using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NotificacionApp.Controllers.Security.Models;
using NotificacionApp.Services;

namespace NotificacionApp.Controllers.Security
{
    /// <summary>
    /// Security controller.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class SecurityController : ControllerBase
    {
        private readonly ISecurityService _securityService;

        /// <summary>
        /// Constuctor.
        /// </summary>
        /// <param name="securityService">Security service</param>
        public SecurityController(ISecurityService securityService)
        {
            _securityService = securityService ?? throw new ArgumentNullException(nameof(securityService));
        }

        /// <summary>
        /// Login of user.
        /// </summary>
        /// <param name="request"><see cref="LoginRequest"/></param>
        /// <returns><see cref="LoginResponse"/></returns>
        [Route("login")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<LoginResponse>> LoginAsync(LoginRequest request)
        {
            var validation = request.Validate();
            if (!validation.IsValid())
            {
                return BadRequest(validation);
            }

            var token = await _securityService.LoginAsync(request.Username, request.Password, request.Role);
            if (token == null)
            {
                return Forbid();
            }

            return Ok(new LoginResponse(token.Hash()));
        }
    }
}
