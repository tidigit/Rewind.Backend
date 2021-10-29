using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using Newtonsoft.Json;
using Rewind.Core;
using Rewind.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rewind.Access
{
    public class DiaryAccess : BaseAccess
    {
        public DiaryAccess(IConfiguration configuration) : base(configuration)
        {

        }
        public async Task<List<Diary>> RetrieveMultipleDiariesAsync(string storyId, string type)
        {
            switch (type)
            {
                case "active":
                    break;
                case "archived":
                    break;
                case "hidden":
                    break;
                case "group":
                    break;
            }
            return null;
        }
        public async Task<Diary> RetrieveDiaryAsync(string storyId)
        {
            var storiesCollection = RewindDatabase.GetCollection<Diary>("diaries");
            var diary = new Diary();
            using (var cursor = await storiesCollection.Find(x => ((IResource)x)._id == new ObjectId(storyId)).ToCursorAsync())
            {
                while (await cursor.MoveNextAsync())
                {
                    foreach (var doc in cursor.Current)
                    {
                        Console.WriteLine(JsonConvert.SerializeObject(doc));
                        var serialized = JsonConvert.SerializeObject(doc);
                        diary = doc;
                        // do something with the documents
                    }
                }
            }
            return diary;
        }
        public async Task<bool> CreateDiaryAsync(Diary diaryToBeCreated)
        {
            try
            {
                var diariesCollection = RewindDatabase.GetCollection<Diary>("diaries");
                await diariesCollection.InsertOneAsync(diaryToBeCreated);
                return true;
            }
            catch (Exception e)
            {
                LoggerHelper.LogError(e);
                throw;
            }

        }

        public async Task<bool> PatchDiaryAsync(UpdateDefinition<Diary> diaryUpdateDefinition, string diaryId)
        {
            try
            {
                var diariesCollection = RewindDatabase.GetCollection<Diary>("diaries");
                await diariesCollection.UpdateOneAsync(Builders<Diary>.Filter.Eq(p => ((IResource)p)._id, new ObjectId(diaryId)), diaryUpdateDefinition);
                return true;
            }
            catch (Exception e)
            {
                LoggerHelper.LogError(e);
                throw;
            }

        }

        public async Task<bool> ReplaceDiaryAsync(Diary diaryToBeReplaced)
        {
            try
            {
                var diariesCollection = RewindDatabase.GetCollection<Diary>("diaries");
                await diariesCollection.ReplaceOneAsync(Builders<Diary>.Filter.Eq(p => ((IResource)p)._id, ((IResource)diaryToBeReplaced)._id), diaryToBeReplaced);
                return true;
            }
            catch (Exception e)
            {
                LoggerHelper.LogError(e);
                throw;
            }

        }

        public async Task<bool> DeleteDiaryAsync(string diaryId)
        {
            try
            {
                var diariesCollection = RewindDatabase.GetCollection<Diary>("diaries");
                await diariesCollection.DeleteOneAsync(Builders<Diary>.Filter.Eq(p => ((IResource)p)._id, new ObjectId(diaryId)));
                return true;
            }
            catch (Exception e)
            {
                LoggerHelper.LogError(e);
                throw;
            }

        }


    }
}
