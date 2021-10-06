using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Rewind.Core;
using Rewind.Objects.TransportObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rewind.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MigrationController : ControllerBase
    {

        private readonly IConfiguration _config;

        public MigrationController(IConfiguration config)
        {
            _config = config;
        }


        [Route("Migrations/DayOne")]
        [HttpPost]
        public IActionResult MigrateFromDayOne(DayOneMigrationRequest dayOneMigrationRequest)
        {
            if(dayOneMigrationRequest != null && dayOneMigrationRequest.DayOneJsonExport?.entries?.Count > 0)
            {
                new MigrationCore(_config).MapFromDayOneJson(dayOneMigrationRequest.DayOneJsonExport, dayOneMigrationRequest.UserId);
            }
            else
            {
                return BadRequest();
            }
            throw new NotImplementedException();
        }




        }
}
