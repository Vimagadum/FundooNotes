namespace BusinessLayer.Service
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using BusinessLayer.Interface;
    using CommonLayer.Model;
    using RepositoryLayer.Entity;
    using RepositoryLayer.Interface;

    /// <summary>
    ///  User BL
    /// </summary>
    public class UserBL : IUserBL
    {
        /// <summary>The user RL</summary>
        private readonly IUserRL userRL;

        /// <summary>Initializes a new instance of the <see cref="UserBL" /> class.</summary>
        /// <param name="userRL">The user RL.</param>
        public UserBL(IUserRL userRL)
        {
            this.userRL = userRL;  
        }

        /// <summary>Forgets the password.</summary>
        /// <param name="email">The email.</param>
        /// <returns>Forget Password</returns>
        public string ForgetPassword(string email)
        {
            try
            {
                return this.userRL.ForgetPassword(email);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>Logins the specified user login.</summary>
        /// <param name="userLogin">The user login.</param>
        /// <returns>login</returns>
        public string login(UserLogin userLogin)
        {
            try
            {
                return this.userRL.login(userLogin);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>Registrations the specified user.</summary>
        /// <param name="User">The user.</param>
        /// <returns>Registration</returns>
        public UserEntity Registration(UserRegistration User)
        {
            try
            {
                return this.userRL.Registration(User);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>Resets the password.</summary>
        /// <param name="email">The email.</param>
        /// <param name="password">The password.</param>
        /// <param name="confirmPassword">The confirm password.</param>
        /// <returns>Reset Password</returns>
        public bool ResetPassword(ResetPasswordModel resetPasswordModel, string email)
        {
            try
            {
                return this.userRL.ResetPassword(resetPasswordModel, email);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
