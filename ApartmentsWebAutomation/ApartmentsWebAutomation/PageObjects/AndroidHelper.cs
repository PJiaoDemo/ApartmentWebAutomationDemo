using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Android;

namespace ApartmentsWebAutomation.PageObjects
{
    public class AndroidHelper : IHelper
    {
        private readonly AndroidDriver<AndroidElement> _dr;
        private readonly By _signInButton = By.XPath("//*[@id='headerLoginSection']/a[2]");
        private readonly By _signInUserEmailInputbox = By.Id("loginEmail");
        private readonly By _signInPasswordInputbox = By.Id("loginPassword");
        private readonly By _signInDoneButton = By.Id("signIn");
        private readonly By _signInFailedMessageLabel = By.Id("loginPasswordError");

        public AndroidHelper(AndroidDriver<AndroidElement> driver)
        {
            _dr = driver;
        }

        //TODO: add all properties and methods here for Android application

    }
}
