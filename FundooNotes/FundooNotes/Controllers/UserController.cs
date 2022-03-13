using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundooNotes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserBL userBL;
        public UserController(IUserBL userBL)
        {
            this.userBL = userBL;

        }
        [HttpPost("Register")]
        public IActionResult Registration(UserRegistration user)
        {
            try
            {
                var result = userBL.Registration(user);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = "Registration Successfull", data = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Registration UnSuccessfull" });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost("Login")]
        public IActionResult login(UserLogin userLogin)
        {
            try
            {
                var result = userBL.login(userLogin);
                if (result != null)
                {
                    return this.Ok(new { success = true, message ="Login Successfull", data = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Enter Valid Email or Password" });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost("ForgetPassword")]
        public IActionResult ForgetPassword(string email)
        {
            try
            {
                var result = userBL.ForgetPassword(email);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = "Mail Set Successfull"});
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Enter Valid Email" });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
