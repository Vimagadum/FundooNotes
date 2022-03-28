namespace RepositoryLayer.Service
{
    using System;
    using System.Collections.Generic;
    using System.IdentityModel.Tokens;
    using System.IdentityModel.Tokens.Jwt;
    using System.Linq;
    using System.Security.Claims;
    using System.Text;
    using CommonLayer.Model;
    using Microsoft.Extensions.Configuration;
    using Microsoft.IdentityModel.Tokens;
    using RepositoryLayer.Context;
    using RepositoryLayer.Entity;
    using RepositoryLayer.Interface;

    /// <summary>
    /// USER RL Class
    /// </summary>
    /// <seealso cref="RepositoryLayer.Interface.IUserRL" />
    public class UserRL : IUserRL
    {
        /// <summary>
        /// The FUNDOO context
        /// </summary>
        private readonly FundooContext fundooContext;

        /// <summary>
        /// The TOOL SETTINGS
        /// </summary>
        private readonly IConfiguration toolsettings;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserRL"/> class.
        /// </summary>
        /// <param name="fundooContext">The FUNDOO context.</param>
        /// <param name="toolsettings">The TOOL SETTINGS.</param>
        public UserRL(FundooContext fundooContext, IConfiguration toolsettings)
        {
            this.fundooContext = fundooContext;
            this.toolsettings = toolsettings;
        }

        /// <summary>
        /// Registrations the specified user.
        /// </summary>
        /// <param name="User">The user.</param>
        /// <returns>
        /// user Registration
        /// </returns>
        public UserEntity Registration(UserRegistration User)
        {
            try
            {
                var userresult = this.fundooContext.User.FirstOrDefault(u => u.Email == User.Email);
                if (userresult == null)
                {
                    UserEntity userEntity = new UserEntity();
                    userEntity.FirstName = User.FirstName;
                    userEntity.LastName = User.LastName;
                    userEntity.Email = User.Email;
                    userEntity.Password = this.PasswordEncrypt(User.Password);
                    this.fundooContext.Add(userEntity);
                    int result = this.fundooContext.SaveChanges();
                    if (result > 0)
                    {
                        return userEntity;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Logins the specified user login.
        /// </summary>
        /// <param name="userLogin">The user login.</param>
        /// <returns>
        /// user login
        /// </returns>
        public string login(UserLogin userLogin)
        {
            try
            {
                if (string.IsNullOrEmpty(userLogin.Email) || string.IsNullOrEmpty(userLogin.Password))
                {
                    return null;
                }
                var result = this.fundooContext.User.Where(x => x.Email == userLogin.Email).FirstOrDefault();
                string dcryptPass = this.PasswordDecrypt(result.Password);
                if (result != null && dcryptPass == userLogin.Password)
                {
                    string token = this.GenerateSecurityToken(result.Email, result.Id);
                    return token;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Generates the security token.
        /// </summary>
        /// <param name="Email">The email.</param>
        /// <param name="Id">The identifier.</param>
        /// <returns>Generate Security Token</returns>
        private string GenerateSecurityToken(string Email, long Id)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.toolsettings["Jwt:secretkey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.Email, Email),
                new Claim("Id", Id.ToString())
            };
            var token = new JwtSecurityToken(toolsettings["Jwt:Issuer"],
              this.toolsettings["Jwt:Issuer"],
              claims,
              expires: DateTime.Now.AddMinutes(60),
              signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        /// <summary>
        /// Forgets the password.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns>
        /// Forget Password
        /// </returns>
        public string ForgetPassword(string email)
        {
            try
            {
                var user = this.fundooContext.User.Where(x => x.Email == email).FirstOrDefault();
                if(user != null)
                {
                    var token = this.GenerateSecurityToken(user.Email, user.Id);
                    new MsmqModel().Sender(token);
                    return token;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Resets the password.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="password">The password.</param>
        /// <param name="confirmPassword">The confirm password.</param>
        /// <returns>
        /// Reset Password
        /// </returns>
        public bool ResetPassword(ResetPasswordModel resetPasswordModel, string email)
        {
            try
            {
                if (resetPasswordModel.Password.Equals(resetPasswordModel.ConfirmPassword))
                {
                    var user = this.fundooContext.User.Where(x => x.Email == email).FirstOrDefault();
                    user.Password = this.PasswordEncrypt(resetPasswordModel.ConfirmPassword);
                    this.fundooContext.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Passwords the encrypt.
        /// </summary>
        /// <param name="password">The password.</param>
        /// <returns>Password Encrypt</returns>
        public string PasswordEncrypt(string password)
        {
            try
            {
                byte[] encode = new byte[password.Length];
                encode = Encoding.UTF8.GetBytes(password);
                string encryptPass = Convert.ToBase64String(encode);
                return encryptPass;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Passwords the decrypt.
        /// </summary>
        /// <param name="encodedPass">The encoded pass.</param>
        /// <returns>Password Decrypt</returns>
        public string PasswordDecrypt(string encodedPass)
        {
            try
            {
                UTF8Encoding encoder = new UTF8Encoding();
                Decoder utf8Decode = encoder.GetDecoder();
                byte[] toDecodeByte = Convert.FromBase64String(encodedPass);
                int charCount = utf8Decode.GetCharCount(toDecodeByte, 0, toDecodeByte.Length);
                char[] decodedChar = new char[charCount];
                utf8Decode.GetChars(toDecodeByte, 0, toDecodeByte.Length, decodedChar, 0);
                string PassDecrypt = new string(decodedChar);
                return PassDecrypt;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
