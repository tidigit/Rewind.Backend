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

        public async Task<Diary> RetrieveDiaryAsync(string diaryId)
        {
            var diary = new Diary();
            try
            {
                diary = await new DiaryAccess(_config).RetrieveDiaryAsync(diaryId);
                return diary;
            }
            catch (Exception exception)
            {
                LoggerHelper.LogError(exception);
                throw;
            }
        }

        public async Task<bool> CreateDiaryAsync(CreateDiaryRequest createDiaryRequest)
        {
            var isCreationSuccessful = false;
            try
            {
                var diaryToBeCreated = await ViewToObjectAsync(createDiaryRequest);
                diaryToBeCreated.CreatedTimeStampInUtc = DateTime.UtcNow;
                diaryToBeCreated.LastModifiedTimeStampInUtc = DateTime.UtcNow;
                isCreationSuccessful = await new DiaryAccess(_config).CreateDiaryAsync(diaryToBeCreated);
                return isCreationSuccessful;
            }
            catch (Exception exception)
            {
                LoggerHelper.LogError(exception);
                throw;
            }
        }

        private async Task<Diary> ViewToObjectAsync(CreateDiaryRequest createDiaryRequest)
        {
            var diaryObject = new Diary();
            diaryObject.Name = createDiaryRequest.DiaryName;
            diaryObject.CoverId = ObjectId.GenerateNewId(); //new ObjectId(createDiaryRequest.CoverId.Id);
            diaryObject.ColorId = ObjectId.GenerateNewId(); //new ObjectId(createDiaryRequest.ColorId.Id);
            diaryObject.IsCollectionsEnabled = createDiaryRequest.IsCollectionsEnabled;
            diaryObject.IsCurationsEnabled = createDiaryRequest.IsCurationsEnabled;
            diaryObject.IsGroupDiary = createDiaryRequest.IsGroupDiary;
            return diaryObject;
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
