using Aquality.Selenium.Core.Configurations;
using Aquality.Selenium.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VK_API.Resources
{
    internal class Config
    {
        public static ISettingsFile configArr = new JsonSettingsFile("Config.json");

        public static string urlVkStartPage = configArr.GetValue<string>("urlVkStartPage");
        public static string urlToAPIRequest = configArr.GetValue<string>("urlToAPIRequest");

        public static string access_token = configArr.GetValue<string>("access_token");
        public static string owner_id = configArr.GetValue<string>("owner_id");
        public static string v = configArr.GetValue<string>("v");
        public static string server = configArr.GetValue<string>("server");
        public static string photo = configArr.GetValue<string>("photo");
        public static string hash = configArr.GetValue<string>("hash");
        public static string post_id = configArr.GetValue<string>("post_id");
        public static string message = configArr.GetValue<string>("message");
        public static string attachments = configArr.GetValue<string>("attachments");
        public static string type = configArr.GetValue<string>("type");
        public static string item_id = configArr.GetValue<string>("item_id");
        public static string v_value = configArr.GetValue<string>("v_value");
    }
}
