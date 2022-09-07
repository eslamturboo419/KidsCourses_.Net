using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KProj.Models
{
    public class RegisterVM
    {
        [Display(Name = "First Name :")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name :")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter Last Name")]
        public string LastName { get; set; }


        [Display(Name = "Email :")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter Email")]
        [RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$", ErrorMessage = "Must Like That  ' joe@aol.com' ")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }


 


        [Display(Name = "Password :")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter Password")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Minum Is 6 ")]
        public string Password { get; set; }


        [Display(Name = " Confirm Password :")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password Is Not The Same")]
        public string ConfirmPassword { get; set; }
    }
}