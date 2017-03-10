using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    public class ProjectModel
    {

        [Required, NotMapped]
        public string token { get; set; } // input token. This is a protected resource.
        [Required, NotMapped]
        public int userId { get; set; } // input id. This is a protected resource.

        [Key]
        public int primaryKey { get; set; }
        public string projectName { get; set; }
        public string description { get; set; }
        public int ownerId { get; set; }

    }
}