using BusinessLayer.Interface;
using CommonLayer.Model;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public class UserBL : IUserBL
    {
        private readonly IUserRL userRL;
        public UserBL(IUserRL userRL)
        {
            this.userRL = userRL;  
        }

        public string login(UserLogin userLogin)
        {
            try
            {
                return userRL.login(userLogin);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public UserEntity Registration(UserRegistration User)
        {
            try
            {
                return userRL.Registration(User);
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
