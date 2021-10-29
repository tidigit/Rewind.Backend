using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Rewind.Core;
using Rewind.Objects;
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

        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet("{id}")]
        public async Task<Diary> Get(string id)
        {
            var diary = await new DiaryCore(_config).RetrieveDiaryAsync(id);
            return diary;
        }

        [HttpDelete("{id}")]
        public async Task<bool> Delete(string id)
        {
            var diary = await new DiaryCore(_config).DeleteDiaryAsync(id);
            return diary;
        }

        [Route("Create")]
        [HttpPost]
        public async Task<IActionResult> CreateDiaryAsync(CreateDiaryRequest createDiaryRequest)
        {
            if (createDiaryRequest != null)
            {
                await new DiaryCore(_config).CreateDiaryAsync(createDiaryRequest);
            }
            //todo validations and operation success result
            return Ok(true);
        }

        [Route("Patch")]
        [HttpPatch]
        public async Task<IActionResult> PatchDiaryAsync(PatchDiaryRequest patchDiaryRequest)
        {
            if (patchDiaryRequest != null)
            {
                await new DiaryCore(_config).PatchDiaryAsync(patchDiaryRequest.Patches, patchDiaryRequest.DiaryId.Id, patchDiaryRequest.UserId);
            }
            //todo validations and operation success result
            return Ok(true);
        }
    }
}
