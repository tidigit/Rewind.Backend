using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Rewind.Core;
using Rewind.Objects;
using Rewind.Objects.TransportObjects;
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
        public LoginResponse Login(LoginRequest loginRequest)
        {
            var loginResponse = new LoginResponse();
            if(loginRequest != null && loginRequest.User != null)
            {
                var user = loginRequest.User;
                switch (loginRequest.LoginType)
                {
                    case LoginType.Email: 
                    case LoginType.Phone:
                        if(!string.IsNullOrWhiteSpace(user.Email) || !string.IsNullOrWhiteSpace(user.Phone))
                        {
                            loginResponse = AccountCore.LoginUserByPhoneOrEmail(user);
                        }
                        break;
                    case LoginType.AppleSignOn:
                        break;
                    default:
                        break;

                }
            }
            else
            {
                loginResponse.Code = LoginCodes.InsufficentData;
            }
            return loginResponse;
        }

        [Route("Signup")]
        [HttpPost]
        public SignupResponse Signup(SignupRequest signupRequest)
        {
            var newUser = signupRequest.User;
            var signupResponse = new SignupResponse();
            if((!string.IsNullOrWhiteSpace(newUser.Email) || !string.IsNullOrWhiteSpace(newUser.Phone)) && !string.IsNullOrWhiteSpace(newUser.Password))
            {
                signupResponse = AccountCore.SignupUser(newUser);
            }
            else 
            {
                signupResponse.Code = GeneralCodes.InsufficentData;
                return null;
            }
            return signupResponse;
        }
    }
}
