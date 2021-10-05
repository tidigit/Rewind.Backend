using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Rewind.Core;
using Rewind.Objects;
using Rewind.Objects.AccessObjects;

namespace Rewind.Access
{
    public class AccountAccess
    {
        private static void SetConfig()
        {
            //todo load connection string from settings
            DatabaseConnectionString = "Server=tcp:blanker.database.windows.net,1433;Initial Catalog=dev.rewind;Persist Security Info=False;User ID=blank;Password=hakunaMATATA02@;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        }

        private static string DatabaseConnectionString { get; set; }
        public static Tuple<User, bool> SearchAndRetrieveUser(User user)
        {
            try
            {
                SetConfig();
                var userOnDatabase = new User();
                var ds = new DataSet();
                var isUserExists = false;
                using var connection = new SqlConnection(DatabaseConnectionString);
                using var command = new SqlCommand("searchAndRetrieveUser", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                _ = command.Parameters.AddWithValue("@username", user.Username);
                _ = command.Parameters.AddWithValue("@email", user.Email);
                _ = command.Parameters.AddWithValue("@phone", user.Phone);
                command.CommandTimeout = 5000;
                connection.Open();

                using var sqlAdaptor = new SqlDataAdapter(command);
                _ = sqlAdaptor.Fill(ds);

                var table = JsonConvert.SerializeObject(ds.Tables[0]);
                var usersOnDatabase = JsonConvert.DeserializeObject<List<User>>(table);
                if(usersOnDatabase.Count > 0)
                {
                    userOnDatabase = usersOnDatabase.First(x => x.Phone == user.Phone || x.Email == user.Email);
                    if(userOnDatabase != null)
                    {
                        isUserExists = true;
                    } 
                }
                return new Tuple<User, bool>(userOnDatabase, isUserExists);
            }
            catch(Exception exception)
            {
                LoggerHelper.LogError(exception);
                throw;
            }

            

        }

        public static int CreateNewUser(User user)
        {
            try
            {
                SetConfig();
                int newUserId;
                var ds = new DataSet();
                using var connection = new SqlConnection(DatabaseConnectionString);
                using var command = new SqlCommand("createUser", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                _ = command.Parameters.AddWithValue("@firstname", user.FirstName);
                _ = command.Parameters.AddWithValue("@lastname", user.LastName);
                _ = command.Parameters.AddWithValue("@username", string.IsNullOrWhiteSpace(user.Username) ? DBNull.Value : user.Username);
                _ = command.Parameters.AddWithValue("@email", string.IsNullOrWhiteSpace(user.Email) ? DBNull.Value : user.Email);
                _ = command.Parameters.AddWithValue("@passwordhash", string.IsNullOrWhiteSpace(user.PasswordHash) ? DBNull.Value : user.PasswordHash);
                _ = command.Parameters.AddWithValue("@phone", string.IsNullOrWhiteSpace(user.Phone) ? DBNull.Value : user.Phone);
                command.CommandTimeout = 5000;
                connection.Open();

                using var sqlAdaptor = new SqlDataAdapter(command);
                _ = sqlAdaptor.Fill(ds);

                var table = JsonConvert.SerializeObject(ds.Tables[0]);
                var insertObjects = JsonConvert.DeserializeObject<List<Insertion>>(table);
                newUserId = Convert.ToInt32(insertObjects[0].InsertedId);
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



