using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using ApartmentsWebAutomation.PageObjects;
using ApartmentsWebAutomation.Utilities;
//for Java annotations
//import org.junit.runners.model.TestClass; 
//import org.junit.BeforeClass;
//import org.junit.Before;
//import org.junit.Test;
//import org.junit.After;
//import org.junit.AfterClass;

namespace ApartmentsWebAutomation
{
    [TestFixture(Category = "PJiao Apt Demo")] //history similar to @TestClass
    public class ApartmentsWebAutomationTests
    {
        private IWebDriver _driver;
        private HomePage _homePage;
        private SignInPage _signInPage;
        private string _webUrl = "https://www.apartments.com/";

        [OneTimeSetUp] //@BeforeClass
        public void OneTimeSetUp()
        {
            //add any setting here just once for ALL test cases, such as database connection, etc.
        }

        [SetUp] //@Before
        public void Setup()
        {
            //set up for each test case
            _driver = new ChromeDriver();
            _driver.Navigate().GoToUrl(_webUrl);

            _homePage = new HomePage(_driver);
            _signInPage = new SignInPage(_driver);
        }

        [TearDown] //@After
        public void Teardown()
        {
            //tear dowm for each test case
            try
            {
                _driver.Quit();
            }
            catch (Exception)
            {
                throw;
            }
        }

        [OneTimeTearDown] //@AfterClass
        public void OneTimeTearDown()
        {
            //quit ALL test case just one time while disconnecting the database, etc.
        }

        /// <summary>
        /// Verify if the log in failure message is matched or not by passing certain username and password.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="userPassword"></param>
        [TestCase("someone@email.com", "TstPass")] //@Test
        public void SignInUnsuccessfulTest(string userName, string userPassword)
        {
            string expectedFailedMessage = "*Login failed. Please try again";

            //Home page
            Assert.True(Util.IsElementDisplayed(_driver, _homePage.SignInButton));
            Util.ClickElement(_driver, _homePage.SignInButton);

            Assert.True(Util.IsElementDisplayed(_driver, _signInPage.SignInUserEmailInputbox));
            Util.SendKeyToTextBox(_driver, _signInPage.SignInUserEmailInputbox, userName);

            //Sign in form
            Assert.True(Util.IsElementDisplayed(_driver, _signInPage.SignInPasswordInputbox));
            Util.SendKeyToTextBox(_driver, _signInPage.SignInPasswordInputbox, userPassword);

            Assert.True(Util.IsElementDisplayed(_driver, _signInPage.SignInDoneButton));
            Util.ClickElement(_driver, _signInPage.SignInDoneButton);

            //Get the failed message
            Assert.True(Util.IsElementDisplayed(_driver, _signInPage.SignInFailedMessageLabel));
            string actualFailedMessage = Util.GetText(_driver, _signInPage.SignInFailedMessageLabel);

            //Verify
            Assert.AreEqual(expectedFailedMessage, actualFailedMessage, "Login failed message mismatched");
        }
    }
}