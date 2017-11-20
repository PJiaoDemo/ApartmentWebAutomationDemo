using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using ApartmentsWebAutomation.PageObjects;
using ApartmentsWebAutomation.Utilities;

namespace ApartmentsWebAutomation
{
    [TestFixture(Category = "Ping Apt Demo")]
    public class ApartmentsWebAutomationTests
    {
        private IWebDriver _driver;
        private HomePage _homePage;
        private SignInPage _signInPage;

        [SetUp]
        public void Setup()
        {
            _driver = new ChromeDriver();
            _driver.Navigate().GoToUrl("https://www.apartments.com/");

            _homePage = new HomePage(_driver);
            _signInPage = new SignInPage(_driver);
        }

        [TearDown]
        public void Teardown()
        {
            try
            {
                _driver.Quit();
            }
            catch (Exception)
            {
                throw;
            }
        }

        [TestCase("someone@email.com", "TstPass")]
        public void SignInUnsuccessfulTest(string userName, string userPassword)
        {
            string expectedFailedMessage = "*Login failed. Please try again";

            Assert.True(Util.IsElementDisplayed(_driver, _homePage.SignInButton));
            Util.ClickElement(_driver, _homePage.SignInButton);

            Assert.True(Util.IsElementDisplayed(_driver, _signInPage.SignInUserEmailInputbox));
            Util.SendKeyToTextBox(_driver, _signInPage.SignInUserEmailInputbox, userName);

            Assert.True(Util.IsElementDisplayed(_driver, _signInPage.SignInPasswordInputbox));
            Util.SendKeyToTextBox(_driver, _signInPage.SignInPasswordInputbox, userPassword);

            Assert.True(Util.IsElementDisplayed(_driver, _signInPage.SignInDoneButton));
            Util.ClickElement(_driver, _signInPage.SignInDoneButton);

            Assert.True(Util.IsElementDisplayed(_driver, _signInPage.SignInFailedMessageLabel));
            string actualFailedMessage = Util.GetText(_driver, _signInPage.SignInFailedMessageLabel);

            Assert.AreEqual(expectedFailedMessage, actualFailedMessage, "Login failed message mismatched");
        }
    }
}