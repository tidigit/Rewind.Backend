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
    public class ResourceAccess<T> : BaseAccess where T : IResource
    {
        public IMongoCollection<T> ResourceCollection { get; set; }
        public ResourceAccess(IConfiguration configuration) : base(configuration)
        {
            ResourceCollection = RewindDatabase.GetCollection<T>("diaries");
        }

        public async Task<IResource> RetrieveResourceAsync(string resourceId)
        {
            IResource resource = null;
            using (var cursor = await ResourceCollection.Find(x => x._id == new ObjectId(resourceId)).ToCursorAsync())
            {
                while (await cursor.MoveNextAsync())
                {
                    foreach (var doc in cursor.Current)
                    {
                        Console.WriteLine(JsonConvert.SerializeObject(doc));
                        var serialized = JsonConvert.SerializeObject(doc);
                        resource = doc;
                        // do something with the documents
                    }
                }
            }
            return resource;
        }
        public async Task<bool> CreateResourceAsync(T ResourceToBeCreated)
        {
            try
            {
                await ResourceCollection.InsertOneAsync(ResourceToBeCreated);
                return true;
            }
            catch (Exception e)
            {
                LoggerHelper.LogError(e);
                throw;
            }

        }

        public async Task<bool> PatchResourceAsync(UpdateDefinition<T> ResourceUpdateDefinition, string ResourceId)
        {
            try
            {
                await ResourceCollection.UpdateOneAsync(Builders<T>.Filter.Eq(p => p._id, new ObjectId(ResourceId)), ResourceUpdateDefinition);
                return true;
            }
            catch (Exception e)
            {
                LoggerHelper.LogError(e);
                throw;
            }

        }

        public async Task<bool> ReplaceResourceAsync(T ResourceToBeReplaced)
        {
            try
            {
                await ResourceCollection.ReplaceOneAsync(Builders<T>.Filter.Eq(p => p._id, ResourceToBeReplaced._id), ResourceToBeReplaced);
                return true;
            }
            catch (Exception e)
            {
                LoggerHelper.LogError(e);
                throw;
            }

        }
        public async Task<bool> ReplaceMultipleResourcesAsync(List<T> resourcesToBeReplaced)
        {
            try
            {
                foreach (var resourceToBeReplaced in resourcesToBeReplaced)
                {
                    await ResourceCollection.ReplaceOneAsync(Builders<T>.Filter.Eq(p => p._id, resourceToBeReplaced._id), resourceToBeReplaced);
                }
                return true;
            }
            catch (Exception e)
            {
                LoggerHelper.LogError(e);
                throw;
            }

        }

        public async Task<bool> DeleteResourceAsync(string ResourceId)
        {
            try
            {
                await ResourceCollection.DeleteOneAsync(Builders<T>.Filter.Eq(p => p._id, new ObjectId(ResourceId)));
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
