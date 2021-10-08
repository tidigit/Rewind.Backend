using Microsoft.Extensions.Configuration;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
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

        public async Task<bool> CreateStoriesAsync(List<Story> storiesToBeCreated)
        {
            var storiesCollection = RewindDatabase.GetCollection<Story>("stories");
            await storiesCollection.InsertManyAsync(storiesToBeCreated);
            return true;
        }

    }
}
