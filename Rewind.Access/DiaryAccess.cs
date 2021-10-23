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
    public class DiaryAccess : BaseAccess
    {
        public DiaryAccess(IConfiguration configuration) : base(configuration)
        {

        }

        public async Task<bool> CreateDiaryAsync(Diary diaryToBeCreated)
        {
            var diariesCollection = RewindDatabase.GetCollection<Diary>("diaries");
            await diariesCollection.InsertOneAsync(diaryToBeCreated);
            return true;
        }



    }
}
