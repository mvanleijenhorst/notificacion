using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace NotificacionApp.Common
{
    public class SessionManager : ISessionManager
    {
        public SessionUser? SessionUser { get; set; }
    }
}
