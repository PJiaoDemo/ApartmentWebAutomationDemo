using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.iOS;
using ApartmentsWebAutomation.PageObjects;
using ApartmentsWebAutomation.Utilities;
using OpenQA.Selenium.Remote;

//refer to Java/JUnit annotations
//import org.junit.runners.model.TestClass; 
//import org.junit.BeforeClass;
//import org.junit.Before;
//import org.junit.Test;
//import org.junit.After;
//import org.junit.AfterClass;

namespace ApartmentsWebAutomation
{
    [TestFixture(Category = "PJiao Apt Demo")]
    public class ApartmentsWebAutomationTests
    {
        #region Definitions
        private IWebDriver _driver;
        private HomePage _homePage;
        private SignInPage _signInPage;
        private string _webUrl = "https://www.apartments.com/";

        private AndroidDriver<AndroidElement> _androidDriver;
        private IOSDriver<IOSElement> _iOSDriver;

        private string _appPlatformName = "Web"; //"Android", "iOS", etc
        //You can alternatively store the following values in app.config file and then retrieve them by Configuration Manager
        //Android
        private string _appAndroidAppPath = "@/Users/xxxx/Documents/Automation/apps/Apartments.apk";
        private string _appAndroidAppPackage = "com.apartments.android";
        private string _appAndroidAppActivity = "com.apartments.MainActivity";
        private string _appAndroidPlatformVersion = "5.0.2";
        private string _appAndroidDeviceName = "Nexus 6";
        private string _appAndroidUrl = "http://10.10.10.10:4723/wd/hub/";
        //iOS
        private string _appiOSAppPath = "@/Users/xxxx/Documents/Automation/apps/Apartments.app"; //.api 
        private string _appiOSPlatformVersion = "9.2";
        private string _appiOSDeviceName = "iPhone 6";
        private string _appiOSUrl = "http://10.10.70.10:4723/wd/hub/"; 
        #endregion

        #region Environment Settings
        [OneTimeSetUp] //@BeforeClass
        public void OneTimeSetUp()
        {
            //add any setting here just once for ALL test cases, such as database connection, etc.
        }

        [SetUp] //@Before
        public void Setup()
        {
            if (_appPlatformName == "Web")
            {
                //set up for each test case
                _driver = new ChromeDriver();
                _driver.Navigate().GoToUrl(_webUrl);

                _homePage = new HomePage(_driver);
                _signInPage = new SignInPage(_driver);
            }
            else
            {
                DesiredCapabilities caps = new DesiredCapabilities();
                caps.SetCapability("platformName", _appPlatformName);
                if (_appPlatformName == "Android")
                {
                    //Set Capabilities for Appium Android
                    caps.SetCapability("appPackage", _appAndroidAppPackage);
                    caps.SetCapability("deviceName", _appAndroidDeviceName);
                    caps.SetCapability("app-activity", _appAndroidAppActivity);
                    caps.SetCapability("takesScreenshot", true);
                    caps.SetCapability("version", _appAndroidPlatformVersion);
                    caps.SetCapability("appPath", _appAndroidAppPath);
                    caps.SetCapability("app", _appAndroidAppPath);
                    caps.SetCapability("autoLaunch", "true");
                    caps.SetCapability("noReset", "true");

                    //Start RemoteWebDriver
                    _androidDriver = new AndroidDriver<AndroidElement>(new Uri(_appAndroidUrl), caps);
                    _androidDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(150);
                }
                else if (_appPlatformName == "iOS")
                {
                    //Set Capabilities for Appium iOS
                    caps.SetCapability("takesScreenshot", true);
                    caps.SetCapability("PlatformVersion", _appiOSPlatformVersion);
                    caps.SetCapability("deviceName", _appiOSDeviceName);
                    caps.SetCapability("app", _appiOSAppPath);
                    caps.SetCapability("autoLaunch", "true");
                    caps.SetCapability("noReset", "true");

                    //Start RemoteWebDriver
                    _iOSDriver = new IOSDriver<IOSElement>(new Uri(_appiOSUrl), caps);
                    _iOSDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(60);
                }
            }

        }

        [TearDown] //@After
        public void Teardown()
        {
            //tear dowm for each test case
            try
            {
                switch (_appPlatformName)
                {
                    case "Web":
                        _driver.Quit();
                        break;
                    case "Android":
                        _androidDriver.Quit();
                        break;
                    case "iOS":
                        _iOSDriver.Quit();
                        break;
                    default:
                        //TODO for any other drivers
                        break;
                }
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
        #endregion

        #region MyRegion Test Cases
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
        #endregion
    }
}