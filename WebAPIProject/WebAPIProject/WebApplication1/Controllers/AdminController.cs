using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using System.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class AdminController : ApiController
    {

        // THIS IS ALL TEST CODE -- NOT FOR PRODUCTION!!

        [Route("api/Admin/UploadBundle")]
        [HttpPost]
        public async Task<IHttpActionResult> PlaceholderUpload()
        {
            WebApplication1Context context = new WebApplication1Context();


        }


        [Route("api/Admin/CreateProject")]
        [HttpPut]
        public IHttpActionResult PlaceholderProjectCreate()
        {
            WebApplication1Context context = new WebApplication1Context();
            return Ok();
        }

        [Route("api/Admin/CreateOrg")]
        [HttpPut]
        public IHttpActionResult PlaceholderOrgCreate(OrganizationModel newOrganization)
        {
            WebApplication1Context context = new WebApplication1Context();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(ConfigurationManager.AppSettings["StorageConnectionString"]);
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference(newOrganization.containerName);
            container.CreateIfNotExists(); // Create the container if it doesn't already exist.

            context.OrganizationsModel.Add(
            new OrganizationModel
            {
                name = newOrganization.name,
                description = newOrganization.description,
                containerName = newOrganization.containerName,             
            });

            context.SaveChangesAsync();

            return Ok();
        }

    }
}
