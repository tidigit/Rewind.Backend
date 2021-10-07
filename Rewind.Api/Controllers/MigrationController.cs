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


        [Route("Migration")]
        [HttpPost]
        public IActionResult MigrateFromThirdPartyApplication(MigrationRequest migrationRequest)
        {
            if(migrationRequest != null && migrationRequest.MigrationExport != null)
            {
                new MigrationCore(_config).MigrateFromThirdPartyApplication(migrationRequest.MigrationExport, migrationRequest.ThirdPartyApplication, migrationRequest.UserId);
            }
            else
            {
                return BadRequest();
            }
            throw new NotImplementedException();
        }




        }
}
