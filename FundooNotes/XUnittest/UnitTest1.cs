using BusinessLayer.Interface;
using BusinessLayer.Service;
using CommonLayer.Model;
using FundooNotes.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Context;
using RepositoryLayer.Interface;
using RepositoryLayer.Service;
using System;
using Xunit;

namespace XUnittest
{
    public class UnitTest1
    {
        private readonly IUserBL iuserBL;
        private readonly IUserRL iuserRL;
        public static DbContextOptions<FundooContext> newContext { get; }
        public static string connectionstring = "Data Source=(localdb)\\ProjectsV13;Initial Catalog=FundooDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        static UnitTest1()
        {
            newContext = new DbContextOptionsBuilder<FundooContext>().UseSqlServer(connectionstring).Options;
        }
        //public UnitTest1()
        //{
        //    var context = new FundooContext(newContext);
        //    iuserRL = new UserRL(context);
        //    iuserBL = new UserBL(iuserRL);
        //}
        [Fact]
        public void RegisterApi_AddUser_return_OkResult()
        {
            var contoller = new UserController(iuserBL);
            var data = new UserRegistration {
                FirstName="sattya",
                LastName="hunk",
                Email="sattya_1@gmail.com",
                Password= "Sattya&6789"
            };
            var result = contoller.Registration(data);
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public void RegisterApi_AddUser_return_BadResult()
        {
            var contoller = new UserController(iuserBL);
            var data = new UserRegistration
            {
                FirstName = "sattya",
                LastName = "hunk",
                Email = "sattya_1@gmail.com",
                Password = "Sattya&6789"
            };
            var result = contoller.Registration(data);
            Assert.IsType<BadRequestObjectResult>(result);
        }
        [Fact]
        public void LogInApi_return_OkResult()
        {
            var controller = new UserController(iuserBL);
            var data = new UserLogin
            {
                Email = "sattya_1@gmail.com",
                Password = "Sattya&6789"
            };
            var result = controller.login(data);
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public void ResetPassword_return_OkResult()
        {
            var controller = new UserController(iuserBL);
            var data = new ResetPasswordModel
            {  Password= "Sattya&6789",
               ConfirmPassword= "Sattya&6789"
            };
            var result = controller.ResetPassword(data);
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public void ForgetPassword_return_OkResult()
        {
            string email = "alberto12345sam@gmail.com";
            var controller = new UserController(iuserBL);
            var result = controller.ForgetPassword(email);
            Assert.IsType<OkObjectResult>(result);
        }
    }
}
