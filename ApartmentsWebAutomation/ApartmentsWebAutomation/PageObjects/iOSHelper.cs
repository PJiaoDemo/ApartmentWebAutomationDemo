using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.iOS;

namespace ApartmentsWebAutomation.PageObjects
{
    class iOSHelper : IHelper
    {
        private readonly IOSDriver<IOSElement> _dr;
        private readonly By _signInButton = By.XPath("//*[@id='headerLoginSection']/a[2]");
        private readonly By _signInUserEmailInputbox = By.Id("loginEmail");
        private readonly By _signInPasswordInputbox = By.Id("loginPassword");
        private readonly By _signInDoneButton = By.Id("signIn");
        private readonly By _signInFailedMessageLabel = By.Id("loginPasswordError");

        public iOSHelper(IOSDriver<IOSElement> driver)
        {
            _dr = driver;
        }

        //TODO: add all properties and methods here for iOS application
    }
}
