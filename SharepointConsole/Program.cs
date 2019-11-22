using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using SharepointLib;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;


/***************************************************************
 * Trying to get portable app to work as sharepoint client
 * 
 * You need to download the sharepoint portable DLLs (NuGet does not work for portable!)
 * https://www.microsoft.com/en-us/download/details.aspx?id=42038
 * When the installer runs, the DLLs live here on a windows box:
 * C:\Program Files\Common Files\microsoft shared\Web Server Extensions\16\ISAPI\
 * 
 * 
 * See this article:
 * https://social.msdn.microsoft.com/Forums/office/en-US/3dacdc8f-a819-4451-8b2c-10f8f14e832b/sharepoint-online-client-components-sdk-does-not-work-with-net-core-20
 * *****************************************************************/

namespace SharepointConsole
{
    class Program
    {
        static async Task Main(string[] args)
        {

            IConfigurationRoot configuration = new ConfigurationBuilder()
              .AddJsonFile("appSettings.json")
              .Build();

            await ShowSPLibUsingLibrary(configuration);
            /*
            var clientContext = GetClientContext(configuration);
            Web site = clientContext.Web;

            ListCollection collList = site.Lists;


            clientContext.Load(collList);
            await clientContext.ExecuteQueryAsync();

            Console.WriteLine("Lists on the current site:\n\n");
            foreach (List tList in collList)
                Console.WriteLine(tList.Title);

            const string ListTitle = "Key Staff";
            List targetList = site.Lists.GetByTitle(ListTitle);

            CamlQuery query = new CamlQuery();
            // Filter the list if required 
            //query.ViewXml = "<View><Query><Where><Contains><FieldRef Name='Title'/><Value Type='Text'>Fire Warden</Value></Contains></Where></Query></View>";
            ListItemCollection collListItem = targetList.GetItems(query);

            clientContext.Load(collListItem);
            await clientContext.ExecuteQueryAsync();

            if (collListItem.Count == 0)
            {
                Console.WriteLine($"No items in {ListTitle} found.");
            }
            else
            {
                Console.WriteLine($"\nItems in {ListTitle}:\n");
                foreach (ListItem targetListItem in collListItem)
                {
                    Console.WriteLine(targetListItem["Title"]);
                    var people = targetListItem["Person"];
                    if (people!=null)
                    {
                        foreach (var person in (FieldUserValue[]) people)
                        {
                            Console.WriteLine($"  -> {person.LookupValue}");
                        }
                    }
                }
            }

            const string ListTitle2 = "General Information";
            targetList = site.Lists.GetByTitle(ListTitle2);

            query = new CamlQuery();
            collListItem = targetList.GetItems(query);

            clientContext.Load(collListItem);
            await clientContext.ExecuteQueryAsync();

            if (collListItem.Count == 0)
            {
                Console.WriteLine($"No items in {ListTitle2} found.");
            }
            else
            {
                Console.WriteLine($"Items in {ListTitle2}:\n");
                foreach (ListItem targetListItem in collListItem)
                {
                    Console.WriteLine(targetListItem["Title"]);
                    Console.WriteLine(targetListItem["Details"]);
                }
            }
            */
            Console.ReadKey();
        }

        /*
        private static ClientContext GetClientContext(IConfigurationRoot configuration)
        {
            string targetSiteURL = configuration["SharepointSite"];
            ClientContext clientContext = new ClientContext(targetSiteURL);

            var login = configuration["SharepointUser"];
            var password = configuration["SharepointPassword"];
            SharePointOnlineCredentials onlineCredentials = new SharePointOnlineCredentials(login, password);
            clientContext.Credentials = onlineCredentials;
            return clientContext;
        }
        */
        private static async Task ShowSPLibUsingLibrary(IConfiguration config)
        {
            ISharepointRepository spRepo = new SharepointRepository(config, NullLogger.Instance);
            var items = await spRepo.GetGeneralListItemsAsync();
            Console.WriteLine($"Items in General:\n");
            foreach (GeneralListItem item in items)
            {
                Console.WriteLine(item.Title);
                Console.WriteLine(item.Details);
            }
        }
    }
}
