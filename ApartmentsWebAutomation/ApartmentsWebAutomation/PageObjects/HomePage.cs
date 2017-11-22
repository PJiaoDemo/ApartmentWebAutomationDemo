using OpenQA.Selenium;

namespace ApartmentsWebAutomation.PageObjects
{
	/// <summary>
	/// Page Object Model: Home page 
	/// </summary>
	class HomePage
	{
		//Encapsulated fields:
		private readonly IWebDriver _driver;
		private readonly By _signInButton = By.XPath("//*[@id='headerLoginSection']/a[2]");

		//Constractor:
		public HomePage(IWebDriver driver) { _driver = driver; }

		//Public Properties:
		public By SignInButton { get { return _signInButton; } }
	}
}
