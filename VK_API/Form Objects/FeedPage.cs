using Aquality.Selenium.Elements.Interfaces;
using Aquality.Selenium.Forms;
using OpenQA.Selenium;



namespace VK_API.Form_Objects
{
    public class FeedPage : Form
    {
        private IButton ProfileButton = ElementFactory.GetButton(By.Id("l_pr"), "ProfileButton");

        public FeedPage() : base(By.XPath("//div[@id='main_feed']"), "FeedPage") {}

        public void ClickProfileButton()
        {
            ProfileButton.Click();
        }
    }
}
