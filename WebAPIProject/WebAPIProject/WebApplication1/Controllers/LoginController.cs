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
using WebApplication1.Models;

/*
 * 
 *  TOHDOH!
 *  Cross-site request forgery protection required! https://azure.microsoft.com/en-gb/documentation/articles/web-sites-dotnet-rest-service-aspnet-api-sql-database/
 * 
 */

namespace WebApplication1.Controllers
{
    public class LoginController : ApiController
    {

        [Route("api/Login")]
        [HttpPost]
        public IHttpActionResult PostLogin(LoginModel loginModel)
        {
            WebApplication1Context context = new WebApplication1Context();

            string error = "Invalid Username or Password";

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            AccountsModel account = context.AccountsModel.Where(a => a.username == loginModel.username).FirstOrDefault();

            if(account.username == loginModel.username)
            {
                byte[] saltInput = LoginUtils.hash(loginModel.password, account.Salt);
                bool slowHashCheck = LoginUtils.slowEquals(saltInput, account.SaltedAndHashedPassword);

                if (slowHashCheck == true)
                {

                    // Success!
                    string rawToken = LoginUtils.makeSimpleToken();
                    string timeStamp = DateTime.UtcNow.ToString("dd/MM/yyyy HH:mm:ss");
                    string obfuscatedToken = LoginUtils.encryptToken(rawToken, timeStamp);

                    byte[] hashedToken = LoginUtils.hashNoSalt(rawToken);

                    context.TokensModel.Add(
                        new TokenModel
                        {
                            tokenHash = hashedToken,
                            tokenDate = timeStamp,
                            userid = account.primaryKey
                        });

                    context.SaveChangesAsync();

                    //return Ok(obfuscatedToken); // return the obfuscated token!
                    return Ok(new
                    {
                        token = obfuscatedToken,
                        userId = account.primaryKey,
                    });
                }
                else
                {
                    //return BadRequest("i failed here!");
                    return BadRequest(error);
                }

            }
            else
            {
                //return BadRequest("i failed there!");
                return BadRequest(error);
            }

        }

        [Route("api/Register")]
        [HttpPost]
        public IHttpActionResult PostRegister(LoginModel loginModel)
        {
            WebApplication1Context context = new WebApplication1Context();
   
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (loginModel.password != loginModel.password_validator)
            {
                string error = "Uhhhhh. I can't believe you've done this.";
                return BadRequest(error);
            }

            byte[] salt = LoginUtils.generateSalt();
            byte[] saltPass = LoginUtils.hash(loginModel.password, salt);

            // Add validations!


            //WebApplication1Context context = new WebApplication1Context();

            context.AccountsModel.Add(
                new AccountsModel
                {
                    username = loginModel.username,
                    email = loginModel.email,
                    organizationId = loginModel.organization,
                    Salt = salt,
                    SaltedAndHashedPassword = saltPass,
                });

            context.SaveChangesAsync();

            return Ok();
        }

    }
}
