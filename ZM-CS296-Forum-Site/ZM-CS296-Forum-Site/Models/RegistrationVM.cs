using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ZM_CS296_Forum_Site.Models
{
    public class RegistrationVM
    {
        [Required(ErrorMessage = "Please enter a username.")]
        [StringLength(25, MinimumLength = 5, ErrorMessage = "Enter a Username between 5 and 20 characters")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Please enter a password.")]
        [DataType(DataType.Password)]
        [Compare("ConfirmPassword")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please confirm your password.")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }
    }
}
