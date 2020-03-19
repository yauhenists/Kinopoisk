using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using AventStack.ExtentReports.Reporter;
using BoDi;
using Kinopoisk;
using Kinopoisk.Pages.Kinopoisk;
using Kinopoisk.Tests;
using NUnit.Framework;
using SeleniumExtras.WaitHelpers;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace SpecflowKinopoisk.Steps
{
    [Binding]
    public class LoginAndLogoutSteps : TechTalk.SpecFlow.Steps
    {
        //public static KinopoiskTests Tests = new KinopoiskTests();
        //public ConciseApi ConciseApi { get; set; } = Tests.ConciseApi;

        private Hooks _hooks;// = new Hooks();
        private KinopoiskHomePage _homePage;
        private RegistrationPage _registrationPage;

        public LoginAndLogoutSteps()
        {
            _hooks = new Hooks();
        }

        [Given(@"I have opened home kinopoisk page")]
        public void GivenIHaveOpenedHomeKinopoiskPage()
        {
            //Tests.SetUp();
           

            //var htmlReporter = new ExtentHtmlReporter(@"G:\Automation\Lab2019\KinopoiskForSpecFlow\Kinopoisk\SpecflowKinopoisk\ExtentReport.html");
            //htmlReporter.Config.Theme = AventStack.ExtentReports.Reporter.Configuration.Theme.Dark;

            //var _extent = new ExtentReports();
            //_extent.AttachReporter(htmlReporter);

            //var _featureName = _extent.CreateTest<Feature>(FeatureContext.Current.FeatureInfo.Title);

            //_extent.Flush();

            _homePage = new KinopoiskHomePage(_hooks.ConciseApi);
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

        /*[When(@"I login with valid credentials")]
        public void WhenILoginWithValidCredentials()
        {
            string[] colHeaders = {"Login", "Password"};
            string[] row = {"test.selenium2002", "selenium123"};

            var table = new Table(colHeaders);
            table.AddRow(row);

            Given("I login with credentials", table);
        }*/


        [Then(@"I should see invalid password message")]
        public void ThenIShouldSeeInvalidPasswordMessage()
        {
            Assert.AreEqual(_registrationPage.InvalidPasswordMessageText,
                _registrationPage.GetInvalidPasswordMessage());

            Console.WriteLine("Assert step is passed");

            //Tests.TearDown();
        }

        [Then(@"I should see avatar button on reloaded home page")]
        public void ThenIShouldSeeAvatarButtonOnReloadedHomePage()
        {
            Assert.Multiple(() =>
            {
                Assert.IsTrue(_hooks.ConciseApi.AssertThat(ExpectedConditions.TitleIs(_homePage.Title)));
                Assert.IsTrue(_homePage.IsLoginAvatarButtonDisplayed());
            });

            //Tests.TearDown();
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

            //Tests.TearDown();
        }



    }
}
