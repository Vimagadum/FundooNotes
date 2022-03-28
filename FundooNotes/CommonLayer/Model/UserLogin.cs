namespace CommonLayer.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    /// <summary>
    ///  User Login
    /// </summary>
    public class UserLogin
    {
        /// <summary>Gets or sets the email.</summary>
        /// <value>The email.</value>
        [Required]
        public string Email { get; set; }

        /// <summary>Gets or sets the password.</summary>
        /// <value>The password.</value>
        [Required]
        public string Password { get; set; }
    }
}
