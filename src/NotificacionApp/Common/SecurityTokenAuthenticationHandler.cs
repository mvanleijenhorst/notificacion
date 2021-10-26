using System.Collections.Generic;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace NotificacionApp.Common
{
    /// <summary>
    /// Security token authentication handler.
    /// </summary>
    public class SecurityTokenAuthenticationHandler : AuthenticationHandler<SecurityTokenSchemeOptions>
    {
        private readonly ISessionManager _sessionManager;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="options"><see cref="IOptionsMonitor"/></param>
        /// <param name="logger"><see cref="ILoggerFactory"/></param>
        /// <param name="encoder"><see cref="UrlEncoder"/></param>
        /// <param name="clock"><see cref="ISystemClock"/></param>
        /// <param name="sessionManager"><see cref=""/></param>
        public SecurityTokenAuthenticationHandler(
            IOptionsMonitor<SecurityTokenSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            ISessionManager sessionManager)
            : base(options, logger, encoder, clock)
        {
            _sessionManager = sessionManager ?? throw new System.ArgumentNullException(nameof(sessionManager));
        }

        /// <inheritdoc/>
        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey(SecurityConstant.SecurityTokenHeader))
                return AuthenticateResult.Fail(SecurityConstant.AccessDenied);

            string hashValue = Request.Headers[SecurityConstant.SecurityTokenHeader];
            if (string.IsNullOrEmpty(hashValue))
            {
                return AuthenticateResult.NoResult();
            }

            var token = SecurityToken.Validate(hashValue);
            if (token == null)
            {
                return AuthenticateResult.Fail(SecurityConstant.AccessDenied);
            }

            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, hashValue),
                };

            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new System.Security.Principal.GenericPrincipal(identity, null);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);

            await Task.Delay(1);

            _sessionManager.SessionUser = new SessionUser(token.Id, token.Name, token.UserRole);
            return AuthenticateResult.Success(ticket);
        }
    }
}

