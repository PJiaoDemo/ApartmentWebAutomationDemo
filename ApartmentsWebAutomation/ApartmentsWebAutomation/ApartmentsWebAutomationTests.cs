using NUnit.Framework;
using ApartmentsWebAutomation.PageObjects;
using ApartmentsWebAutomation.Utilities;

namespace ApartmentsWebAutomation
{
    [TestFixture(Category = "PJiao Apt Demo")]
    public class ApartmentsWebAutomationTests : BaseTest
    {
        #region MyRegion Test Cases
        /// <summary>
        /// Verify if the log in failure message is matched or not by passing certain username and password.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="userPassword"></param>
        [TestCase("someone@email.com", "TstPass")] //@Test
        public void SignInUnsuccessfulTest(string userName, string userPassword)
        {
            var homePage = new HomePage(WebDriver);
            var signInPage = new SignInPage(WebDriver);

            string expectedFailedMessage = "*Login failed. Please try again";

            //Home page
            Assert.True(Util.IsElementDisplayed(WebDriver, homePage.SignInButton));
            Util.ClickElement(WebDriver, homePage.SignInButton);

            Assert.True(Util.IsElementDisplayed(WebDriver, signInPage.SignInUserEmailInputbox));
            Util.SendKeyToTextBox(WebDriver, signInPage.SignInUserEmailInputbox, userName);

            //Sign in form
            Assert.True(Util.IsElementDisplayed(WebDriver, signInPage.SignInPasswordInputbox));
            Util.SendKeyToTextBox(WebDriver, signInPage.SignInPasswordInputbox, userPassword);

            Assert.True(Util.IsElementDisplayed(WebDriver, signInPage.SignInDoneButton));
            Util.ClickElement(WebDriver, signInPage.SignInDoneButton);

            //Get the failed message
            Assert.True(Util.IsElementDisplayed(WebDriver, signInPage.SignInFailedMessageLabel));
            string actualFailedMessage = Util.GetText(WebDriver, signInPage.SignInFailedMessageLabel);

            //Verify
            Assert.AreEqual(expectedFailedMessage, actualFailedMessage, "Login failed message mismatched");
        } 
        #endregion
    }
}