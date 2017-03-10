using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    public class TokenModel
    {
        [Key]
        public int primaryKey { get; set; }
        [Required]
        public byte[] tokenHash { get; set; }
        [Required]
        public int userid { get; set; }
        [Required]
        public string tokenDate {get; set;}
    }
}