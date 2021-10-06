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
    public class StoryAccess
    {
        private readonly IConfiguration Configuration;
        public static MongoClient RewindMongoDbClient { get; set; }
        public static IMongoDatabase RewindDatabase { get; set; }
        public StoryAccess(IConfiguration configuration)
        {
            Configuration = configuration;
            var mongoDbConnectionString = Configuration.GetConnectionString("MongoDbConnectionString");
            var mongoDbSettings = Configuration.GetSection("MongoDbSettings");
            var mongoDbDatabaseName = mongoDbSettings["mongoDbDatabaseName"];
            var settings = MongoClientSettings.FromConnectionString(mongoDbConnectionString);
            RewindMongoDbClient = new MongoClient(settings);
            RewindDatabase = RewindMongoDbClient.GetDatabase(mongoDbDatabaseName);
            SetMongoConventions();
        }

        private static void SetMongoConventions()
        {
            ConventionPack rewindConventionPack = new ConventionPack();
            rewindConventionPack.Add(new CamelCaseElementNameConvention());
            //{
            //new SeperateWordsNamingConvention(),
            //new LowerCaseElementNameConvetion()
            //};
            ConventionRegistry.Register("dev", rewindConventionPack, t => t.FullName.StartsWith("Rewind."));
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
