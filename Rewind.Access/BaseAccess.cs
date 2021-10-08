using Microsoft.Extensions.Configuration;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rewind.Access
{
    public class BaseAccess
    {
        private readonly IConfiguration Configuration;
        public static MongoClient RewindMongoDbClient { get; set; }
        public static IMongoDatabase RewindDatabase { get; set; }
        protected string DatabaseConnectionString { get; set; }
        public BaseAccess(IConfiguration configuration)
        {
            Configuration = configuration;
            Configuration = configuration;
            DatabaseConnectionString = Configuration.GetConnectionString("SqlServerConnectionString");

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
    }
}
