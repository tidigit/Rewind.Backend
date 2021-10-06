using Microsoft.Extensions.Configuration;
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

        internal async void CreateStories(List<Story> stories, int userId)
        {
            await new StoryAccess(_config).CreateStoriesAsync(stories);
            throw new NotImplementedException();
        }
    }
}
