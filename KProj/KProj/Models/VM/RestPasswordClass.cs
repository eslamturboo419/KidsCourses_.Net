using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KProj.Models
{
    public class RestPasswordClass
    {
        [Required(ErrorMessage = "New Password Is Required", AllowEmptyStrings = false)]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "This Password Dosn't Mastch The Same")]
        public string ConfirmPassword { get; set; }


        public string RestCode { get; set; }


    }
}