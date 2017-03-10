using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace WebApplication1.Models
{
    public class OrganizationModel
    {
        [Key]
        public int primaryKey { get; set; }
        public string name { get; set; }
        public string description { get; set; } // optional. for use in the ui system.
        public string containerName { get; set; } 
    }
}