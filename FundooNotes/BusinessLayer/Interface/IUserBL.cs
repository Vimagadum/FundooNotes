/// <summary>
/// IUSER BL CLASS
/// </summary>
namespace BusinessLayer.Interface
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using CommonLayer.Model;
    using RepositoryLayer.Entity;

    /// <summary>
    /// User Interface class
    /// </summary>
    public interface IUserBL
    {
        /// <summary>
        /// Registrations the specified user.
        /// </summary>
        /// <param name="User">The user.</param>
        /// <returns>User Registration</returns>
        public UserEntity Registration(UserRegistration User);

        /// <summary>
        /// Logins the specified user login.
        /// </summary>
        /// <param name="userLogin">The user login.</param>
        /// <returns>User login</returns>
        public string login(UserLogin userLogin);

        /// <summary>
        /// Forgets the password.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns>Forget Password</returns>
        public string ForgetPassword(string email);

        /// <summary>
        /// Resets the password.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="password">The password.</param>
        /// <param name="confirmPassword">The confirm password.</param>
        /// <returns>Reset Password</returns>
        public bool ResetPassword(ResetPasswordModel resetPasswordModel, string email);
    }
}
