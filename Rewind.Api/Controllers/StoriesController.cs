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
    public class StoriesController : ControllerBase
    {
        private readonly IConfiguration _config;

        public StoriesController(IConfiguration config)
        {
            _config = config;
        }


        [HttpGet("Retrieve/{id}")]
        public string RetrieveStoryById(int id)
        {
            return "value";
        }


        [Route("Retrieve")]
        [HttpPost]
        public IActionResult RetrieveStoriesByView(RetrieveStoriesRequest storiesRequest)
        {

            throw new NotImplementedException();
        }


        [Route("Create")]
        [HttpPost]
        public async Task<IActionResult> CreateStory(CreateStoryRequest createStoryRequest)
        {
            if (createStoryRequest != null)
            {
                await new StoryCore(_config).CreateStories(createStoryRequest.StoriesToCreate, createStoryRequest.UserId);
            }
            //todo validations and operation success result
            return Ok(true);
        }



    }
}
