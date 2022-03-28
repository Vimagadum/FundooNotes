namespace FundooNotes.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using BusinessLayer.Interface;
    using CommonLayer.Model;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
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
        /// Initializes a new instance of the <see cref="UserController"/> class.
        /// </summary>
        /// <param name="userBL">The user BL.</param>
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
                var result = this.userBL.Registration(user);
                if (result != null)
                {
                    return this.Ok(new ExceptionModel<UserEntity> { Status = true, Message = "Registration Successfull", Data = result });
                }
                else
                {
                    return this.BadRequest(new ExceptionModel<string> { Status = false, Message = "Registration UnSuccessfull" });
                }
            }
            catch (Exception)
            {
                return this.NotFound(new ExceptionModel<string> { Status = false, Message = "Registration UnSuccessfull" });
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
                    return this.Ok(new ExceptionModel<string> { Status = true, Message = "Login Successfull", Data=result});
                }
                else
                {
                    return this.BadRequest(new ExceptionModel<string> { Status = false, Message = "Enter valid email or password" });
                }
            }
            catch (Exception)
            {
                return this.NotFound(new ExceptionModel<string> { Status = false, Message = "Enter valid email or password" });

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
                    return this.Ok(new ExceptionModel<string> { Status = true, Message = "mail sent Successfull"});
                }
                else
                {
                    return this.BadRequest(new ExceptionModel<string> { Status = false, Message = "Enter valid email or password" });
                }
            }
            catch (Exception)
            {
                return this.NotFound(new ExceptionModel<string> { Status = false, Message = "Enter valid email or password" });
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
                var email = this.User.FindFirst(ClaimTypes.Email).Value.ToString();
                var result = this.userBL.ResetPassword(resetPasswordModel, email);
                if (!result)
                {
                    return this.BadRequest(new ExceptionModel<string> { Status = false, Message = "Enter valid password" });
                }
                else
                {
                    return this.Ok(new ExceptionModel<string> { Status = true, Message = "Reset Password Successfull" });
                }
            }
            catch (Exception)
            {
                return this.BadRequest(new ExceptionModel<string> { Status = false, Message = "Enter valid password" });
            }
        }
    }
}
