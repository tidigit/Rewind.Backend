using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
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
                diary = (Diary)await new ResourceAccess<Diary>(_config).RetrieveResourceAsync(diaryId);
                return diary;
            }
            catch (Exception exception)
            {
                LoggerHelper.LogError(exception);
                throw;
            }
        }

        public async Task<List<DiaryViewInDiaryBrowser>> RetrieveDiaryBrowserAsync(string userId, string mode = "VIEW")
        {
            var diaries = new List<Diary>();
            try
            {
                diaries = await new DiaryAccess(_config).RetrieveMultipleDiariesAsync(userId, "active");
                var diariesAsInDiaryBrowser = new List<DiaryViewInDiaryBrowser>();

                return diariesAsInDiaryBrowser;
            }
            catch (Exception exception)
            {
                LoggerHelper.LogError(exception);
                throw;
            }
        }


        public async Task<List<DiaryViewInDiaryBrowser>> RetrieveDiaryCoverBrowserAsync(string userId, string viewport = "S1")
        {
            var diaryCovers = new List<DiaryCover>();
            try
            {
                //set limit of covers according to viewport size
                //retrieve pre defined covers
                //retrieve user defined covers
                //diaryCovers = await new DiaryAccess(_config).RetrieveMultipleDiariesAsync(userId, "active");
                var diariesAsInDiaryBrowser = new List<DiaryViewInDiaryBrowser>();

                return diariesAsInDiaryBrowser;
            }
            catch (Exception exception)
            {
                LoggerHelper.LogError(exception);
                throw;
            }
        }

        public async Task<bool> DeleteDiaryAsync(string diaryId)
        {
            var isDeleteSuccessful = false;
            try
            {
                isDeleteSuccessful = await new DiaryAccess(_config).DeleteDiaryAsync(diaryId);
                return isDeleteSuccessful;
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
                var diaryToBeCreated = await ViewToModelAsync(createDiaryRequest);
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

        public Task<string> AddUserDiaryCoverAsync(string image)
        {
            throw new NotImplementedException();
        }

        private async Task<Diary> ViewToModelAsync(CreateDiaryRequest createDiaryRequest)
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

        public async Task<bool> PatchDiaryAsync(List<Patch> patches, string dairyId, string userId)
        {
            var isPatchSuccess = false;
            try
            {
                var diaryOnDatabase = await RetrieveDiaryAsync(dairyId);
                var updatedDiary = UpdateDiaryFieldsFromPatches(patches, diaryOnDatabase);
                await new DiaryAccess(_config).ReplaceDiaryAsync(updatedDiary);
                //await new DiaryAccess(_config).PatchDiaryAsync(updateDefinition);
                return isPatchSuccess;
            }
            catch (Exception exception)
            {
                LoggerHelper.LogError(exception);
                throw;
            }
        }

        public async Task<bool> PatchMultipleDiariesAsync(List<PatchObject> patchObjects, string userId)
        {
            var isPatchSuccess = false;
            try
            {
                var diariesToBeUpdated = new List<Diary>();
                foreach (var patchObject in patchObjects)
                {
                    var diaryOnDatabase = await RetrieveDiaryAsync(patchObject.ResourceId.Id);
                    var updatedDiary = UpdateDiaryFieldsFromPatches(patchObject.Patches, diaryOnDatabase);
                    diariesToBeUpdated.Add(updatedDiary);
                }
                await new ResourceAccess<Diary>(_config).ReplaceMultipleResourcesAsync(diariesToBeUpdated);
                //await new DiaryAccess(_config).PatchDiaryAsync(updateDefinition);
                return isPatchSuccess;
            }
            catch (Exception exception)
            {
                LoggerHelper.LogError(exception);
                throw;
            }
        }

        public Diary UpdateDiaryFieldsFromPatches(List<Patch> patches, Diary diaryOnDatabase)
        {
            if (patches.Count > 0)
            {
                foreach (var currentPatch in patches)
                {
                    switch (currentPatch.Field)
                    {
                        case "name":
                            diaryOnDatabase.Name = currentPatch.Value;
                            break;
                        case "cover":
                            diaryOnDatabase.CoverId = new ObjectId(currentPatch.Value);
                            break;
                        case "position":
                            diaryOnDatabase.Position = Convert.ToInt32(currentPatch.Value);
                            break;
                        case "isHidden":
                            diaryOnDatabase.IsHidden = Convert.ToBoolean(currentPatch.Value);
                            break;
                        case "isArchived":
                            diaryOnDatabase.IsArchived = Convert.ToBoolean(currentPatch.Value);
                            break;
                        case "coWriting" when currentPatch.Operation == "Remove":
                            var initialContributors = diaryOnDatabase.PartnerSettings.Contributors;
                            initialContributors.Remove(new ObjectId(currentPatch.Value));
                            break;
                        case "coWriting" when currentPatch.Operation == "Add":
                            diaryOnDatabase.PartnerSettings.Contributors.Add(new ObjectId(currentPatch.Value));
                            break;
                    }
                }
            }
            return diaryOnDatabase;
        }

    }
}
