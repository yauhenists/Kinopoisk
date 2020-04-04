using System;
using Kinopoisk;
using Kinopoisk.Pages.Kinopoisk;
using NUnit.Framework;
using SeleniumExtras.WaitHelpers;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace SpecflowKinopoisk.Steps
{
    [Binding]
    public class LoginAndLogoutSteps
    {
        private readonly ConciseApi _conciseApi;
        private readonly ScenarioContext _scenarioContext;
        private KinopoiskHomePage _homePage;
        private RegistrationPage _registrationPage;

        public LoginAndLogoutSteps(ConciseApi conciseApi, ScenarioContext scenarioContext)
        {
            _conciseApi = conciseApi;
            _scenarioContext = scenarioContext;
        }

        [Given(@"I have opened home kinopoisk page")]
        public void GivenIHaveOpenedHomeKinopoiskPage()
        {
            _homePage = new KinopoiskHomePage(_conciseApi);
            _scenarioContext.Add(typeof(KinopoiskHomePage).ToString(), _homePage);
        }

        [Given(@"I click login button to go to registration page")]
        public void GivenIClickLoginButtonToGoToRegistrationPage()
        {
            _registrationPage = _homePage.GoToRegistartionPage();
        }

        [When(@"I login with credentials")]
        public void WhenILoginWithCredentials(Table table)
        {
            dynamic credentials = table.CreateDynamicInstance();
            var login = (string)credentials.Login;
            var password = (string)credentials.Password;
            _registrationPage.LoginWithCredentials<RegistrationPage>(login, password);
        }

        [Then(@"I should see invalid password message")]
        public void ThenIShouldSeeInvalidPasswordMessage()
        {
            Assert.AreEqual(_registrationPage.InvalidPasswordMessageText,
                _registrationPage.GetInvalidPasswordMessage());

            Console.WriteLine("Assert step is passed");
        }

        [Then(@"I should see avatar button on reloaded home page")]
        public void ThenIShouldSeeAvatarButtonOnReloadedHomePage()
        {
            Assert.Multiple(() =>
            {
                Assert.IsTrue(_conciseApi.AssertThat(ExpectedConditions.TitleIs(_homePage.Title)));
                Assert.IsTrue(_homePage.IsLoginAvatarButtonDisplayed());
            });
        }

        [When(@"I click logout")]
        public void WhenIClickLogout()
        {
            _homePage = _homePage.Logout();
        }

        [Then(@"I should see login button")]
        public void ThenIShouldSeeLoginButton()
        {
            Assert.AreEqual("Войти", _homePage.GetLoginButtonText());
        }
    }
}
