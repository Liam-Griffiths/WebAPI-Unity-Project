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
    public class ResourceUtils : ApiController
    {

                /*
        static string GetBlobSasUri(CloudBlobContainer container)
        {
            //Get a reference to a blob within the container.
            CloudBlockBlob blob = container.GetBlockBlobReference("sasblob.txt");

            //Upload text to the blob. If the blob does not yet exist, it will be created. 
            //If the blob does exist, its existing content will be overwritten.
            string blobContent = "This blob will be accessible to clients via a Shared Access Signature.";
            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(blobContent));
            ms.Position = 0;
            using (ms)
            {
                blob.UploadFromStream(ms);
            }

            //Set the expiry time and permissions for the blob.
            //In this case the start time is specified as a few minutes in the past, to mitigate clock skew.
            //The shared access signature will be valid immediately.
            SharedAccessBlobPolicy sasConstraints = new SharedAccessBlobPolicy();
            sasConstraints.SharedAccessStartTime = DateTime.UtcNow.AddMinutes(-5);
            sasConstraints.SharedAccessExpiryTime = DateTime.UtcNow.AddHours(24);
            sasConstraints.Permissions = SharedAccessBlobPermissions.Read | SharedAccessBlobPermissions.Write;

            //Generate the shared access signature on the blob, setting the constraints directly on the signature.
            string sasBlobToken = blob.GetSharedAccessSignature(sasConstraints);

            //Return the URI string for the container, including the SAS token.
            return blob.Uri + sasBlobToken;
        }
        

        static string GetContainerSasUri(CloudBlobContainer container)
        {
            //Set the expiry time and permissions for the container.
            //In this case no start time is specified, so the shared access signature becomes valid immediately.
            SharedAccessBlobPolicy sasConstraints = new SharedAccessBlobPolicy();
            sasConstraints.SharedAccessExpiryTime = DateTime.UtcNow.AddHours(24);
            sasConstraints.Permissions = SharedAccessBlobPermissions.Write | SharedAccessBlobPermissions.List;

            //Generate the shared access signature on the container, setting the constraints directly on the signature.
            string sasContainerToken = container.GetSharedAccessSignature(sasConstraints);

            //Return the URI string for the container, including the SAS token.
            return container.Uri + sasContainerToken;
        }

        public string GenerateSASConnection(string containerStr)
        {

            
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(ConfigurationManager.AppSettings["StorageConnectionString"]);
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference(containerStr);

            //Clear any existing access policies on container.
            BlobContainerPermissions perms = container.GetPermissions();
            perms.SharedAccessPolicies.Clear();
            container.SetPermissions(perms);

            //Create a new access policy on the container, which may be optionally used to provide constraints for 
            //shared access signatures on the container and the blob.
            string sharedAccessPolicyName = "tutorialpolicy";
            CreateSharedAccessPolicy(blobClient, container, sharedAccessPolicyName);
        }
        */


        public async Task<string> UploadPhotoAsync(HttpPostedFileBase uploadData, string containerName)
        {
            if (uploadData == null || uploadData.ContentLength == 0)
            {
                return null;
            }

            string fullPath = null;

            try
            {
                CloudStorageAccount storageAccount = CloudStorageAccount.Parse(ConfigurationManager.AppSettings["StorageConnectionString"]);

                // Create the blob client and reference the container
                CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
                CloudBlobContainer container = blobClient.GetContainerReference(containerName);


                // Upload image to Blob Storage
                CloudBlockBlob blockBlob = container.GetBlockBlobReference(uploadData.FileName);
                blockBlob.Properties.ContentType = uploadData.ContentType;
                await blockBlob.UploadFromStreamAsync(uploadData.InputStream);

                // Convert to be HTTP based URI (default storage path is HTTPS)
                var uriBuilder = new UriBuilder(blockBlob.Uri);
                uriBuilder.Scheme = "https";
                fullPath = uriBuilder.ToString();

            }
            catch (Exception ex)
            {
                //log.Error(ex, "Error upload to storage");
                //FAIL;
            }

            return fullPath;
        }

    }
}
