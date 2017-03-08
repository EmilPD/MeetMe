﻿using System.ComponentModel.DataAnnotations;

namespace MeetMe.Web.ViewModels.Home
{
    public class RegisterViewModel
    {
        public int ShowServerError { get; set; }

        [Required(ErrorMessage = "Please enter email")]
        [RegularExpression("[a-zA-Z][a-zA-Z0-9\\-\\.]*[a-zA-Z]@[a-zA-Z][a-zA-Z0-9\\-\\.]+[a-zA-Z]+\\.[a-zA-Z]{2,4}", ErrorMessage = "Please, enter valid email")]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter password")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please enter your first name")]
        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please enter your last name")]
        [Display(Name = "Last name")]
        public string LastName { get; set; }
    }
}