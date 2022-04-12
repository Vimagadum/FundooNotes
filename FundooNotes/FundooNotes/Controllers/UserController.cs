namespace FundooNotes.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using BusinessLayer.Interface;
    using CommonLayer.Model;
    using Microsoft.AspNetCore.Cors;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using RepositoryLayer.Entity;

    /// <summary>
    /// USER CONTROLLER
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        /// <summary>
        /// The user BL
        /// </summary>
        private readonly IUserBL userBL;

        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger<UserController> _logger;

        string sessionName = "fullName";
        string sessionEmail = "email";

        /// <summary>
        /// Initializes a new instance of the <see cref="UserController"/> class.
        /// </summary>
        /// <param name="userBL">The user bl.</param>
        /// <param name="logger">The logger.</param>
        public UserController(IUserBL userBL)
        {
            this.userBL = userBL;
        }

        /// <summary>
        /// Registrations the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>User Registration</returns>
        [HttpPost("Register")]
        public IActionResult Registration(UserRegistration user)
        {
            try
            {
                //HttpContext.Session.SetString(sessionName, user.FirstName);
                //HttpContext.Session.SetString(sessionEmail, user.Email);
                var result = this.userBL.Registration(user);
                if (result != null)
                {
                    //string sName = HttpContext.Session.GetString(sessionName);
                    //string sEmail = HttpContext.Session.GetString(sessionEmail);
                    // _logger.LogInformation("Register successfull");
                    //return this.Ok(new ExceptionModel<string> { Status = true, Message = "Registration Successfull", Data = "Session || Name : " + sName + "|| Email Id : " + sEmail });
                    return this.Ok(new ExceptionModel<UserEntity> { Status = true, Message = "Registration Successfull", Data = result });

                }
                else
                {
                    //_logger.LogError("Register Unsuccessfull");
                    return this.BadRequest(new ExceptionModel<string> { Status = false, Message = "Registration UnSuccessfull" });
                }
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex.ToString());
                return this.NotFound(new ExceptionModel<string> { Status = false, Message = ex.Message});
            }
        }

        /// <summary>
        /// Logins the specified user login.
        /// </summary>
        /// <param name="userLogin">The user login.</param>
        /// <returns>user login</returns>
        [HttpPost("Login")]
        public IActionResult login(UserLogin userLogin)
        {
            try
            {
                var result = this.userBL.login(userLogin);
                if (result != null)
                {
                    //_logger.LogInformation("Login Unsuccessfull");
                    return this.Ok(new ExceptionModel<string> { Status = true, Message = "Login Successfull", Data=result});
                }
                else
                {
                    //_logger.LogError("Login Unsuccessfull");
                    return this.BadRequest(new ExceptionModel<string> { Status = false, Message = "Enter valid email or password" });
                }
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex.ToString());
                return this.NotFound(new ExceptionModel<string> { Status = false, Message = ex.Message});
            }
        }

        /// <summary>
        /// Forgets the password.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns>Forget Password</returns>
        [HttpPost("ForgetPassword")]
        public IActionResult ForgetPassword(string email)
        {
            
            try
            {
                var result = this.userBL.ForgetPassword(email);
                if (result != null)
                {
                    //_logger.LogInformation("Forgotted the password");
                    return this.Ok(new ExceptionModel<string> { Status = true, Message = "mail sent Successfull"});
                }
                else
                {
                    //_logger.LogError("forgetting password process Unsuccessfull");
                    return this.BadRequest(new ExceptionModel<string> { Status = false, Message = "Enter valid email or password" });
                }
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex.ToString());
                return this.NotFound(new ExceptionModel<string> { Status = false, Message = ex.Message });
            }
        }

        /// <summary>
        /// Resets the password.
        /// </summary>
        /// <param name="password">The password.</param>
        /// <param name="confirmPassword">The confirm password.</param>
        /// <returns>Reset Password</returns>
        [HttpPut("ResetPassword")]
        public IActionResult ResetPassword(ResetPasswordModel resetPasswordModel)
        {
            try
            {
                //var email = this.User.FindFirst(ClaimTypes.Email).Value.ToString();
                string email = "sattya_1@gmail.com";
                var result = this.userBL.ResetPassword(resetPasswordModel, email);
                if (!result)
                {
                    //_logger.LogError("Reset Password Unsuccessfull");
                    return this.BadRequest(new ExceptionModel<string> { Status = false, Message = "Enter valid password" });
                }
                else
                {
                    //_logger.LogInformation("Reset Password Successfull");
                    return this.Ok(new ExceptionModel<string> { Status = true, Message = "Reset Password Successfull" });
                }
            }
            catch (Exception ex)
            {
                //_logger.LogInformation(ex.ToString());
                return this.BadRequest(new ExceptionModel<string> { Status = false, Message = ex.Message });
            }
        }
    }
}
