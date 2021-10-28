using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using Rewind.Access;
using Rewind.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rewind.Core
{
    public class StoryCore
    {
        private readonly IConfiguration _config;
        public StoryCore(IConfiguration configuration)
        {
            _config = configuration;
        }
        public static bool CreateStory(Story story)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> CreateStories(List<Story> stories, string userId)
        {
            var isCreationSuccess = false;
            try
            {
                _ = stories.Select(x => x._userId == new ObjectId(userId));
                isCreationSuccess = await new StoryAccess(_config).CreateStoriesAsync(stories);
                return isCreationSuccess;
            }
            catch (Exception exception)
            {
                LoggerHelper.LogError(exception);
                throw;
            }
        }
    }
}
