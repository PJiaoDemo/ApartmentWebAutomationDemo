using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.iOS;
using OpenQA.Selenium.Remote;

//Demo: refer to Java/JUnit annotations
//import org.junit.runners.model.TestClass; 
//import org.junit.BeforeClass;
//import org.junit.Before;
//import org.junit.Test;
//import org.junit.After;
//import org.junit.AfterClass;

namespace ApartmentsWebAutomation
{
    public class BaseTest
    {
        public IWebDriver WebDriver;
        private string _webUrl = "https://www.apartments.com/";

        public AndroidDriver<AndroidElement> AndroidDriver;
        public IOSDriver<IOSElement> iOSDriver;
        public string AppPlatformName = "Web"; //"Android", "iOS", etc

        //Android
        private string _appAndroidAppPath = "@/Users/xxxx/Documents/Automation/apps/Apartments.apk";
        private string _appAndroidAppPackage = "com.apartments.android";
        private string _appAndroidAppActivity = "com.apartments.MainActivity";
        private string _appAndroidPlatformVersion = "5.0.2";
        private string _appAndroidDeviceName = "Nexus 6";
        private string _appAndroidUrl = "http://127.0.0.1:4723/wd/hub/";
        //iOS
        private string _appiOSAppPath = "@/Users/xxxx/Documents/Automation/apps/Apartments.app"; //.api 
        private string _appiOSPlatformVersion = "9.2";
        private string _appiOSDeviceName = "iPhone 6";
        private string _appiOSUrl = "http://127.0.0.1:4723/wd/hub/";

        [OneTimeSetUp] //@BeforeClass
        public void OneTimeSetUp()
        {
            //add any setting here just once for ALL test cases, such as database connection, etc.
        }

        [SetUp] //@Before
        public void Setup()
        {
            if (AppPlatformName == "Web")
            {
                //set up for each test case
                WebDriver = new ChromeDriver();
                WebDriver.Navigate().GoToUrl(_webUrl);
            }
            else
            {
                DesiredCapabilities caps = new DesiredCapabilities();
                caps.SetCapability("platformName", AppPlatformName);
                if (AppPlatformName == "Android")
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
                    AndroidDriver = new AndroidDriver<AndroidElement>(new Uri(_appAndroidUrl), caps);
                    AndroidDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(150);
                }
                else if (AppPlatformName == "iOS")
                {
                    //Set Capabilities for Appium iOS
                    caps.SetCapability("takesScreenshot", true);
                    caps.SetCapability("version", _appiOSPlatformVersion);
                    caps.SetCapability("deviceName", _appiOSDeviceName);
                    caps.SetCapability("app", _appiOSAppPath);
                    caps.SetCapability("autoLaunch", "true");
                    caps.SetCapability("noReset", "true");

                    //Start RemoteWebDriver
                    iOSDriver = new IOSDriver<IOSElement>(new Uri(_appiOSUrl), caps);
                    iOSDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(60);
                }
            }

        }

        [TearDown] //@After
        public void Teardown()
        {
            //tear dowm for each test case
            try
            {
                switch (AppPlatformName)
                {
                    case "Web":
                        WebDriver.Quit();
                        break;
                    case "Android":
                        AndroidDriver.Quit();
                        break;
                    case "iOS":
                        iOSDriver.Quit();
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

    }
}
