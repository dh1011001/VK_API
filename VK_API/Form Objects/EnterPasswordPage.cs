using Aquality.Selenium.Elements.Interfaces;
using Aquality.Selenium.Forms;
using OpenQA.Selenium;

namespace VK_API.Form_Objects
{
    public class EnterPasswordPage : Form
    {
        private ITextBox PasswordInputTextBox = ElementFactory.GetTextBox(By.XPath("//input[@name='password']"), "PasswordInputTextBox");
        private IButton ContinueButton = ElementFactory.GetButton(By.XPath("//button[contains(@class,'vkuiButton') and @type='submit']"), "ContinueButton");
        public EnterPasswordPage() : base(By.ClassName("vkc__EnterPasswordNoUserInfo__content"), "EnterPasswordPage") {}

        public void SendPassword(string password)
        {
            PasswordInputTextBox.ClearAndType(password);
        }

        public void ClickContinueButton()
        {
            ContinueButton.Click();
        }

    }
}
