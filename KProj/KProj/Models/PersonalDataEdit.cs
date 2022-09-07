using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KProj.Models
{
    public class PersonalDataEdit
    {
        public int id { get; set; }

        public string F_Name { get; set; }
        public string L_Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }

        
        public Nullable<System.DateTime> Birthday { get; set; }



        // reset Password

        public string HiddenString { get; set; }

        //[Required(ErrorMessage = "Old Password Is Required", AllowEmptyStrings = false)]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }

        //[Required(ErrorMessage = "New Password Is Required", AllowEmptyStrings = false)]
        [DataType(DataType.Password)]
      
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "This Password Dosn't Mastch The Same")]
        public string ConfirmPassword { get; set; }



    }
}