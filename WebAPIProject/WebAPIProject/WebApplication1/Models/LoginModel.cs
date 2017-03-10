using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    public class LoginModel
    {

        /*
         * - Not mapped, i would like EF to not turn this model into a database. 
         * 
         */

        [NotMapped, Required, MinLength(2), MaxLength(30)]
        public string username { get; set; }
        [NotMapped, Required, MinLength(6), MaxLength(30)]
        public string password { get; set; }

        [NotMapped, MinLength(6), MaxLength(30)]
        public string password_validator { get; set; }
        [NotMapped, EmailAddress]
        public string email { get; set; }
        [NotMapped]
        public int organization { get; set; }
    }
}