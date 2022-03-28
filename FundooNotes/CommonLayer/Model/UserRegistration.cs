namespace CommonLayer.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    /// <summary>
    ///  User Registration
    /// </summary>
    public class UserRegistration
    {
        /// <summary>Gets or sets the first name.</summary>
        /// <value>The first name.</value>
        [Required]
        [RegularExpression(@"^[A-Z][a-z]{2,}$")]
        public string FirstName { get; set; }

        /// <summary>Gets or sets the last name.</summary>
        /// <value>The last name.</value>
        [Required]
        [RegularExpression(@"^[A-Z][a-z]{2,}$")]
        public string LastName { get; set; }

        /// <summary>Gets or sets the email.</summary>
        /// <value>The email.</value>
        [Required]
        [RegularExpression("^[A-Za-z0-9]{3,}([.][A-Za-z0-9]+)*[@][a-z]+[.][a-z]{3}?$", ErrorMessage = "Enter a valid email.")]
        public string Email { get; set; }

        /// <summary>Gets or sets the password.</summary>
        /// <value>The password.</value>
        [Required]
        [RegularExpression(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$")]
        public string Password { get; set; }        
    }
}
