using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace SlackBot.Config
{
    public class WebApiConfig
    {
        public void Configure(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            RequireHttpsOnAllActions(config);
        }

        private void RequireHttpsOnAllActions(HttpConfiguration config)
        {
            string baseUrlsSetting = ConfigurationManager.AppSettings["baseUrls"].ToString();

            string[] baseUrls = baseUrlsSetting.Split(';');

            foreach (string baseUrlEntry in baseUrls)
            {
                string baseUrl = baseUrlEntry.Replace("+", "localhost");
                UriBuilder uri = new UriBuilder(baseUrl);
            }
        }
    }
}
