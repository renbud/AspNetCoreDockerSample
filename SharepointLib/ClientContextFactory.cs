using Microsoft.Extensions.Configuration;
using Microsoft.SharePoint.Client;
using System.Net;
using System.Security;

namespace SharepointLib
{
    public class ClientContextFactory
    {
        IConfiguration _configuration;
        public ClientContextFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public ClientContext GetClientContext()
        {
            string targetSiteURL = _configuration["SharepointSite"];
            ClientContext clientContext = new ClientContext(targetSiteURL);

            var login = _configuration["SharepointUser"];
            var password = _configuration["SharepointPassword"];
            //SecureString pwds = new NetworkCredential("", password).SecurePassword;
            // The following line of code will only work on a Windows platform
            // It seems that to run on Linux we may need to use REST API see comments below
            SharePointOnlineCredentials onlineCredentials = new SharePointOnlineCredentials(login, password);
            clientContext.Credentials = onlineCredentials;
            return clientContext;
        }


        /*********************************************************************************************
         * Potentially using the REST API
         * 
         * Once authenticated the REST API looks not too bad:
         * https://docs.microsoft.com/en-us/sharepoint/dev/sp-add-ins/complete-basic-operations-using-sharepoint-rest-endpoints
         * https://docs.microsoft.com/en-us/sharepoint/dev/sp-add-ins/working-with-lists-and-list-items-with-rest
         * 
         * But authenticating with raw HTTP looks like it will be a massive pain:
         * Looks like there are 2 ways (user authentication  vs app authentication)
         * Neither one looks easy.
         * User authentication requires a very complex series of requests, headers & cookies with tokens
         * https://paulryan.com.au/2014/spo-remote-authentication-rest/  (user authentication)
         * 
         * App authentication requires registering an app with sharepoint online (admin required) and getting a key
         * https://sharepoint.stackexchange.com/questions/236286/sharepoint-online-rest-api-authentication-in-postman (app authentication)
         * ******************************************************************************************/
    }
}
