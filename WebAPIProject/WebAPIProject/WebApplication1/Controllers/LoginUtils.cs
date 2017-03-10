using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using System.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using WebApplication1.Models;


/*
 * 
 * - Prototype!! 
 * - Built with the help of: https://azure.microsoft.com/en-gb/documentation/articles/mobile-services-dotnet-backend-get-started-custom-authentication/
 * 
 */


namespace WebApplication1.Controllers
{

    public struct decryptTokenData
    {
        public string token;
        public string utcDateTime;
    }

    public class LoginUtils
    {
        public static byte[] hash(string plaintext, byte[] salt)
        {
            SHA512Cng hashFunc = new SHA512Cng();
            byte[] plainBytes = System.Text.Encoding.ASCII.GetBytes(plaintext);
            byte[] toHash = new byte[plainBytes.Length + salt.Length];
            plainBytes.CopyTo(toHash, 0);
            salt.CopyTo(toHash, plainBytes.Length);
            return hashFunc.ComputeHash(toHash);
        }

        public static byte[] hashNoSalt(string plaintext)
        {
            SHA512Cng hashFunc = new SHA512Cng();
            byte[] plainBytes = System.Text.Encoding.ASCII.GetBytes(plaintext);
            byte[] toHash = new byte[plainBytes.Length];
            plainBytes.CopyTo(toHash, 0);
            return hashFunc.ComputeHash(toHash);
        }

        public static byte[] generateSalt()
        {
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] salt = new byte[256];
            rng.GetBytes(salt);
            return salt;
        }

        public static bool slowEquals(byte[] a, byte[] b) // deliberately slow
        {
            int diff = a.Length ^ b.Length;
            for (int i = 0; i < a.Length && i < b.Length; i++)
            {
                diff |= a[i] ^ b[i];
            }
            return diff == 0;
        }

        public static bool SafeEquals(byte[] strA, byte[] strB) // Faster Byte[] compare. with tokens if they are different it will fail very fast.
        {
            int length = strA.Length;
            if (length != strB.Length)
            {
                return false;
            }
            for (int i = 0; i < length; i++)
            {
                if (strA[i] != strB[i]) return false;
            }
            return true;
        }

        public static string makeSimpleToken()
        {
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] entropy = new byte[256];
            rng.GetBytes(entropy);

            string tokenKey = System.Text.Encoding.UTF8.GetString(entropy);
            return tokenKey;
        }

        public static string encryptToken(string tokenKey, string timeNow)
        {
            string rawToken = tokenKey + timeNow;
            byte[] rawTokenBytes = System.Text.Encoding.UTF8.GetBytes(rawToken);
            string token = Convert.ToBase64String(rawTokenBytes);

            return token;
        }


        public static decryptTokenData decryptToken(string tokenInput)
        {
            byte[] rawTokenBytes = Convert.FromBase64String(tokenInput);
            string rawToken = System.Text.Encoding.UTF8.GetString(rawTokenBytes);

            int cutPoint = ((rawToken.Length) - 19); // 19 in respect of the number of chars in the date format. ***** DateTime.UtcNow.ToString("dd/MM/yyyy HH:mm:ss"); 

            decryptTokenData data;
            data.token = rawToken.Substring(0, cutPoint - 1);
            data.utcDateTime = rawToken.Substring(cutPoint);
            return data;
        }

        public static bool ValidateToken(string tokenInput, int idInput)
        {
            decryptTokenData data = LoginUtils.decryptToken(tokenInput);
            WebApplication1Context context = new WebApplication1Context();

            byte[] checkHash = LoginUtils.hashNoSalt(data.token);

            TokenModel token = context.TokensModel.Where(a => a.tokenHash == checkHash).FirstOrDefault();

            if(idInput == token.userid)
            {
                bool byteCheck = LoginUtils.SafeEquals(token.tokenHash, checkHash);
                if (byteCheck == true)
                {
                    if (data.utcDateTime == token.tokenDate) // TODO -- Add expiry system!
                    {
                        return true;
                    }
                    else
                    {
                        // TODO - Log the possiblilty of tampering with the user tokens.
                        // This would mean the token had been decrypted and then had the date stamp edited. Suspicious activity!
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                // if the given id is not the same as the one connected to the token fail!
                // saves on doing a byte check too! :)
                return false;
            }
        }

        public static int GetUserOrganization(int userId)
        {
            WebApplication1Context context = new WebApplication1Context();
            AccountsModel account = context.AccountsModel.Where(a => a.primaryKey == userId).FirstOrDefault();

            if (account.primaryKey == userId)
            {
                return account.organizationId;
            }
            else
            {
                return -1; // fail
            }
        }
    }
}