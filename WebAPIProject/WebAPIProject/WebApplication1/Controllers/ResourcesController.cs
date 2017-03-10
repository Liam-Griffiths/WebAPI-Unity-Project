using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using System.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class ResourcesController : ApiController
    {

        [Route("api/Resources/Projects")]
        [HttpGet]
        public IHttpActionResult GetProjects(ProjectModel projectModel)
        {
            bool loggedIn = false;
            string inputRequest = projectModel.token;

            loggedIn = LoginUtils.ValidateToken(projectModel.token, projectModel.userId);

            if(loggedIn == true)
            {

                int orgId = LoginUtils.GetUserOrganization(projectModel.userId);

                if(orgId == -1)
                {
                    return NotFound(); // organisation not found!
                }
                else
                {
                    WebApplication1Context context = new WebApplication1Context();
                    IQueryable<ProjectModel> projects = context.ProjectsModel.Where(a => a.ownerId == orgId);

                    return Ok(projects); // Hopefully this will return a content negotiated list of projects. TODO

                    /*foreach(ProjectModel rowData in projects)
                    {

                    }*/

                }

            }
            else
            {
                return NotFound(); // token not found!
            }
        }
    }
}
