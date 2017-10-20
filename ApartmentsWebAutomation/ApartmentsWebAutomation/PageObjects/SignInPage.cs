using OpenQA.Selenium;

namespace ApartmentsWebAutomation.PageObjects
{
    class SignInPage
    {
        //Encapsulated fields:
        private readonly IWebDriver _driver;
        private readonly By _signInUserEmailInputbox = By.Id("loginEmail");
        private readonly By _signInPasswordInputbox = By.Id("loginPassword");
        private readonly By _signInDoneButton = By.Id("signIn");
        private readonly By _signInFailedMessageLabel = By.Id("loginPasswordError");

        //Constractor:
        public SignInPage(IWebDriver driver) { _driver = driver; }

        //Public Properties:
        public By SignInUserEmailInputbox { get { return _signInUserEmailInputbox; } }
        public By SignInPasswordInputbox { get { return _signInPasswordInputbox; } }
        public By SignInDoneButton { get { return _signInDoneButton; } }
        public By SignInFailedMessageLabel { get { return _signInFailedMessageLabel; } }
    }
}
