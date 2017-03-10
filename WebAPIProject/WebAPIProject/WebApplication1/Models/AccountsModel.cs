using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    public class AccountsModel
    {
        [Key]
        public int primaryKey { get; set; }
        [Required, MinLength(2), MaxLength(30)]
        public string username { get; set; }
        [Required]
        public byte[] Salt { get; set; }
        [Required]
        public byte[] SaltedAndHashedPassword { get; set; }
        [Required, EmailAddress]
        public string email { get; set; }
        [Required]
        public int organizationId { get; set; } // foreign
    }
}