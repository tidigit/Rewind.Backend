using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using Newtonsoft.Json;
using Rewind.Core;
using Rewind.Objects;
using Rewind.Objects.AccessObjects;

namespace Rewind.Access
{
    public class AccountAccess : BaseAccess
    {
        private readonly IConfiguration _config;
        public AccountAccess(IConfiguration configuration): base(configuration)
        {

        }
        
        public Tuple<Account, bool> SearchAndRetrieveUserAccount(Account userAccount)
        {
            try
            {
                var userOnDatabase = new Account();
                var ds = new DataSet();
                var isUserExists = false;
                using var connection = new SqlConnection(DatabaseConnectionString);
                using var command = new SqlCommand("searchAndRetrieveUser", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                _ = command.Parameters.AddWithValue("@username", userAccount.Username);
                _ = command.Parameters.AddWithValue("@email", userAccount.Email);
                _ = command.Parameters.AddWithValue("@phone", userAccount.Phone);
                command.CommandTimeout = 5000;
                connection.Open();

                using var sqlAdaptor = new SqlDataAdapter(command);
                _ = sqlAdaptor.Fill(ds);

                var table = JsonConvert.SerializeObject(ds.Tables[0]);
                var usersOnDatabase = JsonConvert.DeserializeObject<List<Account>>(table);
                if(usersOnDatabase.Count > 0)
                {
                    userOnDatabase = usersOnDatabase.First(x => x.Phone == userAccount.Phone || x.Email == userAccount.Email || x.Username == userAccount.Username);
                    if(userOnDatabase != null)
                    {
                        isUserExists = true;
                    } 
                }
                return new Tuple<Account, bool>(userOnDatabase, isUserExists);
            }
            catch(Exception exception)
            {
                LoggerHelper.LogError(exception);
                throw;
            }

            

        }

        public string CreateNewUserAccountAsync(Account userAccount)
        {
            try
            {
                var newUserId = "";
                var ds = new DataSet();
                using var connection = new SqlConnection(DatabaseConnectionString);
                using var command = new SqlCommand("createUser", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                _ = command.Parameters.AddWithValue("@firstname", userAccount.FirstName);
                _ = command.Parameters.AddWithValue("@lastname", userAccount.LastName);
                _ = command.Parameters.AddWithValue("@username", string.IsNullOrWhiteSpace(userAccount.Username) ? DBNull.Value : userAccount.Username);
                _ = command.Parameters.AddWithValue("@email", string.IsNullOrWhiteSpace(userAccount.Email) ? DBNull.Value : userAccount.Email);
                _ = command.Parameters.AddWithValue("@passwordhash", string.IsNullOrWhiteSpace(userAccount.PasswordHash) ? DBNull.Value : userAccount.PasswordHash);
                _ = command.Parameters.AddWithValue("@phone", string.IsNullOrWhiteSpace(userAccount.Phone) ? DBNull.Value : userAccount.Phone);
                //todo - add useridentifier
                command.CommandTimeout = 5000;
                connection.Open();

                using var sqlAdaptor = new SqlDataAdapter(command);
                _ = sqlAdaptor.Fill(ds);

                var table = JsonConvert.SerializeObject(ds.Tables[0]);
                var insertObjects = JsonConvert.DeserializeObject<List<Insertion>>(table);
                if(insertObjects.Count > 0)
                {
                    newUserId = Convert.ToString(insertObjects[0].InsertedId);
                }
                return newUserId;
            }
            catch (Exception exception)
            {
                LoggerHelper.LogError(exception);
                throw;
            }

        }


    }
}



