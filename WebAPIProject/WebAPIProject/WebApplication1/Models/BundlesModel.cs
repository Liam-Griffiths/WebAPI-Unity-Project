using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    public class BundleModel
    {
        [Key]
        public int primaryKey { get; set; }
        public string name { get; set; }
        public int projectId { get; set; }
        public int URI { get; set; }
    }
}