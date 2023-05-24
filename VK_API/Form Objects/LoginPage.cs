using Aquality.Selenium.Elements.Interfaces;
using Aquality.Selenium.Forms;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VK_API.Form_Objects
{
    public class LoginPage : Form
    {
        private ITextBox LoginInputTextBox = ElementFactory.GetTextBox(By.XPath("//input[@name='login' and @type='text']"), "LoginInputTextBox");
        private IButton SubmitButton = ElementFactory.GetButton(By.XPath("//button[contains(@class,'FlatButton') and @type='submit']"),"SubmitButton");

        public LoginPage() : base(By.XPath("//div[@id='index_login']"), "LoginPage") {}

        public void SendLogin(string login)
        {
            LoginInputTextBox.ClearAndType(login);
        }

        public void ClickSubmitButton()
        {
            SubmitButton.Click();
        }
    }
}
