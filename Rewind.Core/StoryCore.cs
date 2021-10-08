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

        public async void CreateStories(List<Story> stories, string userId)
        {
            try
            {
                _ = stories.Select(x => x._userId == new ObjectId(userId));
                await new StoryAccess(_config).CreateStoriesAsync(stories);
            }
            catch (Exception exception)
            {
                LoggerHelper.LogError(exception);
                throw;
            }
        }
    }
}
