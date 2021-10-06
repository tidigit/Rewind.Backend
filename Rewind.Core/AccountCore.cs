using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Rewind.Access;
using Rewind.Objects;
using Rewind.Objects.TransportObjects;
using Rewind.Utilities;
using System;


namespace Rewind.Core
{
    public class AccountCore
    {
        private readonly IConfiguration _config;
        public AccountCore(IConfiguration configuration)
        {
            _config = configuration;
        }
        public LoginResponse LoginUserByPhoneOrEmail(User returningUser)
        {
            try
            {
                var loginResponse = new LoginResponse();
                bool isUserExists;
                bool isPasswordCorrect;
                User userOnDatabase;
                (userOnDatabase,isUserExists) = new AccountAccess(_config).SearchAndRetrieveUser(returningUser);
                if (!string.IsNullOrWhiteSpace(userOnDatabase.PasswordHash))
                {
                    try
                    {
                        isPasswordCorrect = BCrypt.Net.BCrypt.Verify(returningUser.Password, userOnDatabase.PasswordHash);
                    }
                    catch (Exception exception)
                    {
                        LoggerHelper.LogError(exception);
                        isPasswordCorrect = false;
                    }
                    if (isPasswordCorrect)
                    {
                        loginResponse.AccessToken = new JwtHelper(_config).BuildToken(returningUser);
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

        public SignupResponse AddNewUser(User newUser)
        {
            var signupResponse = new SignupResponse();
            try
            {
                bool isUserAlreadyExists;
                var userOnDatabase = new User();
                (userOnDatabase, isUserAlreadyExists) = new AccountAccess(_config).SearchAndRetrieveUser(newUser);
                if (isUserAlreadyExists)
                {
                    if (userOnDatabase != null)
                    {
                        if (userOnDatabase.Email == newUser.Email)
                        {
                            signupResponse.Code = SignupCodes.EmailAlreadyExists;
                        }
                        else if (userOnDatabase.Phone == newUser.Phone)
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
                    newUser.PasswordHash = BCrypt.Net.BCrypt.HashPassword(newUser.Password);
                    var createdUserId = new AccountAccess(_config).CreateNewUser(newUser);
                    if (createdUserId > 0)
                    {
                        signupResponse.AccessToken = new JwtHelper(_config).BuildToken(newUser);
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
