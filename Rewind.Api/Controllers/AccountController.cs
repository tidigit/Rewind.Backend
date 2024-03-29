﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Rewind.Core;
using Rewind.Objects;
using Rewind.Objects.TransportObjects;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace Rewind.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IConfiguration _config;

        public AccountController(IConfiguration config)
        {

            _config = config;
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            var rng = new Random();
            return new List<string>() { "Test" };
        }

        [Route("Login")]
        [HttpPost]
        public IActionResult Login(LoginRequest loginRequest)
        {
            var loginResponse = new LoginResponse();
            try
            {
                if (loginRequest != null)
                {
                    switch (loginRequest.LoginType)
                    {
                        case LoginType.Email:
                        case LoginType.Phone:
                            if ((!string.IsNullOrWhiteSpace(loginRequest.Email) || !string.IsNullOrWhiteSpace(loginRequest.Phone)) && !string.IsNullOrWhiteSpace(loginRequest.Password))
                            {
                                var returningUser = new Account()
                                {
                                    Email = loginRequest.Email,
                                    Phone = loginRequest.Phone,
                                    Password = loginRequest.Password
                                };
                                loginResponse = new AccountCore(_config).LoginUserByPhoneOrEmail(returningUser);
                                return Ok(loginResponse);
                            }
                            else
                            {
                                return BadRequest();
                            }
                        case LoginType.AppleSignOn:
                            return NotFound();
                        default:
                            return BadRequest();
                    }
                    
                }
                else
                {
                    loginResponse.Code = GeneralCodes.InsufficentData;
                    return BadRequest();
                }
            }
            catch (Exception exception)
            {
                LoggerHelper.LogError(exception);
                throw new ArgumentException($"Internal Server Error - Please try again");
            }
        }

        [Route("Signup")]
        [HttpPost]
        public async Task<IActionResult> SignupAsync(SignupRequest signupRequest)
        {
            var signupResponse = new SignupResponse();
            try
            {
                var newUserAccount = new Account()
                {
                    Email = signupRequest.Email,
                    Phone = signupRequest.Phone,
                    Username = signupRequest.Username,
                    Password = signupRequest.Password,
                    FirstName = signupRequest.FirstName,
                    LastName = signupRequest.LastName
                };
                if ((!string.IsNullOrWhiteSpace(newUserAccount.Email) || !string.IsNullOrWhiteSpace(newUserAccount.Phone)))
                {
                    signupResponse = await new AccountCore(_config).AddNewUserAsync(newUserAccount);
                    return Ok(signupResponse);
                }
                else
                {
                    signupResponse.Code = GeneralCodes.InsufficentData;
                    return BadRequest(signupResponse);
                }
            }
            catch (Exception exception)
            {
                LoggerHelper.LogError(exception);
                throw new ArgumentException($"Internal Server Error - Please try again");
            }

        }
    }
}
