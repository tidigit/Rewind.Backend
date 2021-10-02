using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Rewind.Core;
using Rewind.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rewind.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<AccountController> _logger;
        private readonly IConfiguration _config;

        public AccountController(IConfiguration config, ILogger<AccountController> logger)
        {
            _logger = logger;
            _config = config;
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            var rng = new Random();
            return new List<string>() {"Test" };
        }

        [Route("Login")]
        [HttpPost]
        public string Login(LoginRequest loginRequest)
        {
            var token = AccountCore.LoginUser(loginRequest);
            return token;
        }

        [Route("Signup")]
        [HttpPost]
        public string Signup(SignupRequest signupRequest)
        {
            var newUser = signupRequest.User;
            if((!string.IsNullOrWhiteSpace(newUser.Email) || !string.IsNullOrWhiteSpace(newUser.Phone)) && !string.IsNullOrWhiteSpace(newUser.Password))
            {
                var token = AccountCore.SignupUser(newUser);
                return token;
            }
            else {
                return null;
            }
            
        }
    }
}
