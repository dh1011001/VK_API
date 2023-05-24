using NUnit.Framework;
using Aquality.Selenium;
using Aquality.Selenium.Core.Configurations;
using Aquality.Selenium.Core.Utilities;
using Aquality.Selenium.Browsers;
using VK_API.Resources;

namespace VK_API
{
    public class BaseTest
    {
        protected static Browser browser = AqualityServices.Browser;

        [SetUp]
        public void Setup()
        {
            browser.Maximize();
            browser.GoTo(Config.urlVkStartPage);
            browser.WaitForPageToLoad();
        }

        [TearDown]
        public void TearDown()
        {
            browser.Quit();
        }
    }
}
