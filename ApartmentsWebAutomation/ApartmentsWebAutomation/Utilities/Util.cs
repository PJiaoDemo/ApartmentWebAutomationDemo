using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace ApartmentsWebAutomation.Utilities
{
    public static class Util
    {
        public static bool IsElementDisplayed(IWebDriver webDriver, By byElement, int optionalWaitSeconds = 30)
        {
            //Synchronize waiting for current element to be displayed (optional wait time that can be set longer if needed) 
            try
            {
                var explicitWait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(optionalWaitSeconds));
                var targetElementDisplayed = explicitWait.Until(d => d.FindElement(byElement).Displayed);
                return targetElementDisplayed;
            }
            catch 
            {
                return false;
            }
        }

        public static bool IsElementEnabled(IWebDriver webDriver, By byElement, int optionalWaitSeconds = 10)
        {
            //Synchronize waiting for current element to be enabled (optional wait time that can be set longer if needed) 
            try
            {
                var explicitWait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(optionalWaitSeconds));
                var targetElementDisplayed = explicitWait.Until(d => d.FindElement(byElement).Enabled);
                return targetElementDisplayed;
            }
            catch
            {
                return false;
            }
        }

        public static bool IsElementSelected(IWebDriver webDriver, By byElement, int optionalWaitSeconds = 10)
        {
            //check if current element is selected or not.
            try
            {
                var explicitWait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(optionalWaitSeconds));
                var targetElementSelected = explicitWait.Until(d => d.FindElement(byElement).Selected);
                return targetElementSelected;
            }
            catch
            {
                return false;
            }
        }

        public static string GetText(IWebDriver webDriver, By byElement)
        {
            return webDriver.FindElement(byElement).Text;
        }

        public static string GetAttribute(IWebDriver webDriver, By byElement, string attributeString)
        {
            if (null != webDriver.FindElement(byElement).GetAttribute(attributeString))
            {
                return webDriver.FindElement(byElement).GetAttribute(attributeString).Trim();
            }
            return webDriver.FindElement(byElement).GetAttribute(attributeString);
        }

        public static string GetAttributeValue(IWebDriver webDriver, By byElement)
        {
            return webDriver.FindElement(byElement).GetAttribute("value");
        }

        public static string GetAttributeText(IWebDriver webDriver, By byElement)
        {
            return webDriver.FindElement(byElement).GetAttribute("Text");
        }

        public static void ClickElement(IWebDriver webDriver, By byElement)
        {
            //simply click a control
            webDriver.FindElement(byElement).Click();
        }

        public static void SendKeyToTextBox(IWebDriver webDriver, By byElement, string inputValue)
        {
            //send key to a Text box
            webDriver.FindElement(byElement).Clear();
            webDriver.FindElement(byElement).SendKeys(inputValue);
        }
    }
}
