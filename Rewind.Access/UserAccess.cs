using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using Newtonsoft.Json;
using Rewind.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rewind.Access
{
    public class UserAccess: BaseAccess
    {
        public UserAccess(IConfiguration configuration) : base(configuration)
        {

        }

        public async Task<string> InsertUserOnMongoDBAsync(User newUser)
        {
            var usersCollection = RewindDatabase.GetCollection<User>("users");
            await usersCollection.InsertOneAsync(newUser);
            var insertedUserIdentifier = "";
            using (var cursor = await usersCollection.Find(x => x.UserProfile.Email == newUser.UserProfile.Email).ToCursorAsync())
            {
                while (await cursor.MoveNextAsync())
                {
                    foreach (var user in cursor.Current)
                    {
                        insertedUserIdentifier = user.Id.ToString();
                    }
                }
            }
            return insertedUserIdentifier;
        }

    }
}
