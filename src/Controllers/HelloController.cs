using System;

using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

using System.IO;

using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;

using Newtonsoft.Json;
using System.Security.Claims;

using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using hello.transaction.core.Models.DTO;

namespace hello.transaction.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiVersionNeutral]
    [AllowAnonymous]
    public class HelloController : ControllerBase
    {

        private readonly IHostEnvironment environment;
        private readonly IConfiguration configuration;

        public HelloController(IConfiguration _configuration, IHostEnvironment _environment)
        {
            configuration = _configuration ?? throw new ArgumentNullException(nameof(_configuration));
            environment = _environment ?? throw new ArgumentNullException(nameof(_environment));
        }

        [EnableCors("AllowCors")]
        [Route("")]
        [HttpGet]
        public ActionResult<string> Index()
        {
            //var user = CvxClaimsPrincipal.Current;
            var user = HttpContext.User;

            return $"Hello World with .NET Framework CORE Web API at { DateTime.UtcNow }.";
        }

        //[AllowAnonymous]
        [EnableCors("AllowCors")]
        [Route("health")]
        [HttpGet]
        public ActionResult<Health> Health()
        {
            return new Health(environment.EnvironmentName);
        }

        //[AllowAnonymous]
        [EnableCors("AllowCors")]
        [Route("throw")]
        [HttpGet]
        public ActionResult<string> Throw()
        {
            throw new IOException();
        }
    }
}