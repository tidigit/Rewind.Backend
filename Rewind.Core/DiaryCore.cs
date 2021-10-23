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
    public class DiaryCore
    {
        private readonly IConfiguration _config;
        public DiaryCore(IConfiguration configuration)
        {
            _config = configuration;
        }

        public async void CreateDiary(Diary diaryToBeCreated, string userId)
        {
            try
            {
                await new DiaryAccess(_config).CreateDiaryAsync(diaryToBeCreated);
            }
            catch (Exception exception)
            {
                LoggerHelper.LogError(exception);
                throw;
            }
        }
    }
}
