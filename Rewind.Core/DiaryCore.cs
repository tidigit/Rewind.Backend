using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using Rewind.Access;
using Rewind.Objects;
using Rewind.Objects.TransportObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rewind.Core
{
    public class DiaryCore
    {
        private readonly IConfiguration _config;
        public DiaryCore(IConfiguration configuration)
        {
            _config = configuration;
        }

        public async void CreateDiary(Diary diaryToBeCreated, string userId)
        {
            try
            {
                await new DiaryAccess(_config).CreateDiaryAsync(diaryToBeCreated);
            }
            catch (Exception exception)
            {
                LoggerHelper.LogError(exception);
                throw;
            }
        }

        public async void PatchDiary(PatchObject patchObject, string dairyId, string userId)
        {
            try
            {
                var currentDairy = RetrieveDairy(dairyId);
                if (patchObject.Patches.Count > 0)
                {
                    foreach (var currentPatch in patchObject.Patches)
                    {
                        switch (currentPatch.Field)
                        {
                            case "name":
                                currentDairy.Name = currentPatch.Value;
                                break;
                            case "cover":
                                break;

                        }

                    }

                }
                //await new DiaryAccess(_config).CreateDiaryAsync(diaryToBeCreated);
            }
            catch (Exception exception)
            {
                LoggerHelper.LogError(exception);
                throw;
            }
        }

        private Diary RetrieveDairy(string dairyId)
        {
            throw new NotImplementedException();
        }
    }
}
