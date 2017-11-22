using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace ApartmentsWebAutomation.Utilities
{
	/// <summary>
	/// Arrange all shared or common methods in this utility class
	/// </summary>
	public static class Util
	{
		public static bool IsElementDisplayed(IWebDriver webDriver, By byElement, int optionalWaitSeconds = 30)
		{
			//waiting for current element to be displayed (optional wait time that can be set longer if needed) 
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

		public static string GetText(IWebDriver webDriver, By byElement)
		{
			//get text from an element
			return webDriver.FindElement(byElement).Text;
		}

		public static void ClickElement(IWebDriver webDriver, By byElement)
		{
			//simply click a control
			webDriver.FindElement(byElement).Click();
		}

		public static void SendKeyToTextBox(IWebDriver webDriver, By byElement, string inputValue)
		{
			//send key or input to a Text box
			webDriver.FindElement(byElement).Clear();
			webDriver.FindElement(byElement).SendKeys(inputValue);
		}
	}
}
