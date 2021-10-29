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
        //todo - validations and operation success result, responseObjects

        [HttpGet]
        public async Task<DiaryBrowserResponse> Get(string userId, string mode)
        {
            var diariesAsInBrowser = await new DiaryCore(_config).RetrieveDiaryBrowserAsync(userId, mode);
            var diaryBrowserResponse = new DiaryBrowserResponse()
            {
                Diaries = diariesAsInBrowser
            };
            return diaryBrowserResponse;
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

            return Ok(true);
        }

        [Route("CreateDiaryCover")]
        [HttpPost]
        public async Task<IActionResult> CreateDiaryCoverAsync(CreateDiaryCoverRequest createDiaryRequest)
        {
            var coverId = "";
            if (createDiaryRequest != null)
            {
                coverId = await new DiaryCore(_config).AddUserDiaryCoverAsync(createDiaryRequest.Image);
            }

            return Ok(coverId);
        }


        [Route("Patch")]
        [HttpPatch]
        public async Task<IActionResult> PatchDiaryAsync(PatchResourceRequest patchDiaryRequest)
        {
            if (patchDiaryRequest != null)
            {
                await new DiaryCore(_config).PatchDiaryAsync(patchDiaryRequest.PatchObjects[0].Patches, patchDiaryRequest.PatchObjects[0].ResourceId.Id, patchDiaryRequest.UserId);
            }
            return Ok(true);
        }

        [Route("PatchMultiple")]
        [HttpPatch]
        public async Task<IActionResult> PatchMultipleDiairesAsync(PatchResourceRequest patchDiaryRequest)
        {
            if (patchDiaryRequest != null)
            {
                await new DiaryCore(_config).PatchMultipleDiariesAsync(patchDiaryRequest.PatchObjects, patchDiaryRequest.UserId);
            }
            return Ok(true);
        }

    }
}
