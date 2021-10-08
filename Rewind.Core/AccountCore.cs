using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Rewind.Access;
using Rewind.Objects;
using Rewind.Objects.TransportObjects;
using Rewind.Utilities;
using System;
using System.Threading.Tasks;

namespace Rewind.Core
{
    public class AccountCore
    {
        private readonly IConfiguration _config;
        public AccountCore(IConfiguration configuration)
        {
            _config = configuration;
        }
        public LoginResponse LoginUserByPhoneOrEmail(Account returningUserAccount)
        {
            try
            {
                var loginResponse = new LoginResponse();
                bool isUserExists;
                bool isPasswordCorrect;
                Account userOnDatabase;
                (userOnDatabase, isUserExists) = new AccountAccess(_config).SearchAndRetrieveUserAccount(returningUserAccount);
                if (!string.IsNullOrWhiteSpace(userOnDatabase.PasswordHash))
                {
                    try
                    {
                        isPasswordCorrect = BCrypt.Net.BCrypt.Verify(returningUserAccount.Password, userOnDatabase.PasswordHash);
                    }
                    catch (Exception exception)
                    {
                        LoggerHelper.LogError(exception);
                        isPasswordCorrect = false;
                    }
                    if (isPasswordCorrect)
                    {
                        loginResponse.AccessToken = new JwtHelper(_config).BuildToken(returningUserAccount);
                        loginResponse.Code = GeneralCodes.TransactionSuccessful;
                    }
                    else
                    {
                        loginResponse.Code = LoginCodes.WrongPassword;
                        loginResponse.Description = LoginCodes.WrongPasswordDescription;
                    }
                }
                else
                {
                    loginResponse.Code = LoginCodes.UserDoesnotExist;
                    loginResponse.Description = LoginCodes.UserDoesnotExistDescription;
                }


                return loginResponse;
            }
            catch (Exception exception)
            {
                LoggerHelper.LogError(exception);
                throw;
            }

        }

        public async Task<SignupResponse> AddNewUserAsync(Account newUserAccount)
        {
            var signupResponse = new SignupResponse();
            try
            {
                bool isUserAlreadyExists;
                var userAccountOnDatabase = new Account();
                (userAccountOnDatabase, isUserAlreadyExists) = new AccountAccess(_config).SearchAndRetrieveUserAccount(newUserAccount);
                if (isUserAlreadyExists)
                {
                    if (userAccountOnDatabase != null)
                    {
                        if (userAccountOnDatabase.Email == newUserAccount.Email)
                        {
                            signupResponse.Code = SignupCodes.EmailAlreadyExists;
                        }
                        else if (userAccountOnDatabase.Phone == newUserAccount.Phone)
                        {
                            signupResponse.Code = SignupCodes.PhoneAlreadyExists;
                        }
                        else
                        {
                            signupResponse.Code = SignupCodes.UserAlreadyExists;
                        }
                        signupResponse.Description = SignupCodes.UserAlreadyExistDescription;
                    }
                    else
                    {
                        signupResponse.Code = GeneralCodes.SomethingWentWrong;
                    }
                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(newUserAccount.Password))
                    {
                        newUserAccount.PasswordHash = BCrypt.Net.BCrypt.HashPassword(newUserAccount.Password);
                    }
                    var newUser = AccountHelper.MapUserFromAccount(newUserAccount);
                    var userIdentifier = await new UserAccess(_config).InsertUserOnMongoDBAsync(newUser);
                    newUserAccount.UserIdentifier = userIdentifier;
                    var createdUserId = new AccountAccess(_config).CreateNewUserAccountAsync(newUserAccount);
                    if (!string.IsNullOrWhiteSpace(createdUserId))
                    {
                        signupResponse.AccessToken = new JwtHelper(_config).BuildToken(newUserAccount);
                        signupResponse.Code = GeneralCodes.TransactionSuccessful;
                    }
                    else
                    { 
                        signupResponse.Code = GeneralCodes.SomethingWentWrong;
                    }
                }
                return signupResponse;
            }
            catch (Exception exception)
            {
                LoggerHelper.LogError(exception);
                throw;
            }

        }
    }


}
