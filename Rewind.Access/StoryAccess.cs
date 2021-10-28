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
    public class StoryAccess : BaseAccess
    {
        public StoryAccess(IConfiguration configuration) : base(configuration)
        {

        }

        public async Task<bool> CreateStoryAsync(Story storyToBeCreated)
        {
            var storiesCollection = RewindDatabase.GetCollection<Story>("stories");
            await storiesCollection.InsertOneAsync(storyToBeCreated);
            return true;
        }

        public async Task<bool> RetrieveStoryAsync(string storyId)
        {
            var storiesCollection = RewindDatabase.GetCollection<Story>("stories");

            using (var cursor = await storiesCollection.Find(x => x._id == new ObjectId(storyId)).ToCursorAsync())
            {
                while (await cursor.MoveNextAsync())
                {
                    foreach (var doc in cursor.Current)
                    {
                        Console.WriteLine(JsonConvert.SerializeObject(doc));
                        // do something with the documents
                    }
                }
            }
            return true;
        }



        public async Task<bool> CreateStoriesAsync(List<Story> storiesToBeCreated)
        {
            try
            {
                var storiesCollection = RewindDatabase.GetCollection<Story>("stories");
                await storiesCollection.InsertManyAsync(storiesToBeCreated);
                return true;
            }
            catch (Exception exception)
            {
                LoggerHelper.LogError(exception);
                throw;
            }

        }

    }
}
