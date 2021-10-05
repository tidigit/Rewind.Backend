using Microsoft.AspNetCore.Mvc;
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
    public class StoriesController : ControllerBase
    {

        [Route("Stories")]
        [HttpGet("{id}")]
        public string RetrieveStoryById(int id)
        {
            return "value";
        }


        [Route("Stories")]
        [HttpPost]
        public IActionResult RetrieveStoriesByView(StoriesRequest storiesRequest)
        {

            throw new NotImplementedException();
        }



    }
}
