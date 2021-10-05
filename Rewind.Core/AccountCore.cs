using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Rewind.Access;
using Rewind.Objects;
using Rewind.Objects.TransportObjects;
using Rewind.Utilities;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Rewind.Core
{
    public class AccountCore
    {
        public static LoginResponse LoginUserByPhoneOrEmail(User returningUser)
        {
            try
            {
                var loginResponse = new LoginResponse();
                bool isUserExists;
                bool isPasswordCorrect;
                User userOnDatabase;
                (userOnDatabase,isUserExists) = AccountAccess.SearchAndRetrieveUser(returningUser);
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
                        loginResponse.AccessToken = new TokenService().BuildToken(returningUser);
                        loginResponse.Code = GeneralCodes.TransactionSuccessful;
                    }
                    else
                    {
                        loginResponse.Code = LoginCodes.WrongPassword;
                    }
                }
                else
                {
                    loginResponse.Code = LoginCodes.UserDoesnotExist;
                }


                return loginResponse;
            }
            catch (Exception exception)
            {
                LoggerHelper.LogError(exception);
                throw;
            }

        }

        public static SignupResponse SignupUser(User newUser)
        {
            var signupResponse = new SignupResponse();
            try
            {
                bool isUserAlreadyExists;
                (_, isUserAlreadyExists) = AccountAccess.SearchAndRetrieveUser(newUser);
                if (isUserAlreadyExists)
                {
                    signupResponse.Code = SignupCodes.UserAlreadyExists;
                }
                else
                {
                    newUser.PasswordHash = BCrypt.Net.BCrypt.HashPassword(newUser.Password);
                    var createdUserId = AccountAccess.CreateNewUser(newUser);
                    if (createdUserId > 0)
                    {
                        signupResponse.AccessToken = new TokenService().BuildToken(newUser);
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


    public class TokenService
    {
        private const double EXPIRY_DURATION_MINUTES = 30;

        public TokenService()
        {

        }
        public string BuildToken(User user)
        {
            string key = "randomKeysyfufkdhdasdoufgasdduyfgaouefsfsjlgsdfjytausdyf";//_config["Jwt:Key"].ToString();
            string issuer = "www.rewind.so";//_config["Jwt:Issuer"].ToString();
            var claims = new[] {
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Email, user.Phone),
            new Claim(ClaimTypes.NameIdentifier,
            Guid.NewGuid().ToString())
        };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            var tokenDescriptor = new JwtSecurityToken(issuer, issuer, claims,
                expires: DateTime.Now.AddMinutes(EXPIRY_DURATION_MINUTES), signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }
        public bool IsTokenValid(string token)
        {
            string key = "randomKeysyfufkdhdasdoufgasdduyfgaouefsfsjlgsdfjytausdyf";//_config["Jwt:Key"].ToString();
            string issuer = "www.rewind.so";//_config["Jwt:Issuer"].ToString();
            var mySecret = Encoding.UTF8.GetBytes(key);
            var mySecurityKey = new SymmetricSecurityKey(mySecret);
            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                tokenHandler.ValidateToken(token,
                new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = issuer,
                    ValidAudience = issuer,
                    IssuerSigningKey = mySecurityKey,
                }, out SecurityToken validatedToken);
            }
            catch
            {
                return false;
            }
            return true;
        }
    }

}
