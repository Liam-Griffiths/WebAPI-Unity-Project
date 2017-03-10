using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class UploadModel
    {
        public int projectID { get; set; }
        public string containerName { get; set; }
        public string PhotoUrl { get; set; }
        public bool IsDone { get; set; } 
    }
}