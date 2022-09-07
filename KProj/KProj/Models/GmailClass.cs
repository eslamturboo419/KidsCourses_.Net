using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KProj.Models
{
    public class GmailClass
    {
        [Required(ErrorMessage = "Required First Name", AllowEmptyStrings = false)]
        public string FName { get; set; }

        [Required(ErrorMessage = "Required last Name", AllowEmptyStrings = false)]
        public string LName { get; set; }

        [Required(ErrorMessage = "Required Email", AllowEmptyStrings = false)]
        [DataType(DataType.EmailAddress, ErrorMessage = "This Is Not Email Address")]
        [RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$", ErrorMessage = "Must Like That  ' joe@aol.com' ")]
        public string YourEmail { get; set; }


        

        [Required(ErrorMessage = "Required Message", AllowEmptyStrings = false)]
        public string Message { get; set; }

    }
}