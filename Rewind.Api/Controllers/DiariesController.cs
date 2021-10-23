using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Rewind.Core;
using Rewind.Objects.TransportObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Rewind.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiariesController : ControllerBase
    {
        private readonly IConfiguration _config;
        public DiariesController(IConfiguration configuration)
        {
            _config = configuration;
        }
        // GET: api/<DiariesController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<DiariesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<DiariesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<DiariesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<DiariesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }


        
        [Route("Create")]
        [HttpPost]
        public IActionResult CreateDiary(CreateDiaryRequest createDiaryRequest)
        {
            if(createDiaryRequest != null)
            {
                new DiaryCore(_config).CreateDiary(createDiaryRequest.DiaryToCreate, createDiaryRequest.UserId);
            }
            //todo validations and operation success result
            return Ok(true);
        }
    }
}
