using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Sharepoint_online_connection
{
    class Program
    {
        static void Main(string[] args)
        {
            string tenant = "https://nileshpwc.sharepoint.com/sites/markets";
            string userName = "nilesh.a.shinde@nileshpwc.onmicrosoft.com";
            string passwordString = "AAyyee99";
            TestConnect(tenant, userName, passwordString);
        }

        private static void TestConnect(string tenant, string userName, string passwordString)
        {
            // Get access to source site
            using (var ctx = new ClientContext(tenant))
            {
                //Provide count and pwd for connecting to the source
                var passWord = new SecureString();
                foreach (char c in passwordString.ToCharArray()) passWord.AppendChar(c);
                ctx.Credentials = new SharePointOnlineCredentials(userName, passWord);
                // Actual code for operations
                Web web = ctx.Web;
                ctx.Load(web);
                ctx.ExecuteQuery();
                Console.WriteLine(string.Format("Connected to site with title of {0}", web.Title));
                Console.ReadLine();
                //Get my list
                CamlQuery query = new CamlQuery();
                ListCollection collList = web.Lists;

                ctx.Load(collList);

                ctx.ExecuteQuery();

                foreach (var oList in collList)
                {
                    Console.WriteLine("Title: {0} Created: {1}", oList.Title, oList.Created.ToString());
                }

                List myList = web.Lists.GetByTitle("users");
                ListItemCollection kundeItems = myList.GetItems(query);
                ctx.Load<List>(myList);
                ctx.Load<ListItemCollection>(kundeItems);
                ctx.ExecuteQuery();
                Console.WriteLine("Getting list items");
                foreach (var spItem in kundeItems)
                {
                    string title = (string)spItem["Title"];
                    Console.WriteLine(title);
                }
                Console.ReadLine();
            }
        }
    }
}
