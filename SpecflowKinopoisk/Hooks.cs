using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using AventStack.ExtentReports.Reporter;
using BoDi;
using Io.Cucumber.Messages;
using Kinopoisk;
using Kinopoisk.Tests;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace SpecflowKinopoisk
{
    [Binding]
    public sealed class Hooks
    {
        public static KinopoiskTests Tests = new KinopoiskTests();
        public ConciseApi ConciseApi { get; set; } = Tests.ConciseApi;
        private readonly IObjectContainer _objectContainer;

        private static ExtentTest _featureName;
        private static ExtentTest _scenario;
        private static ExtentReports _extent;

        //private IWebDriver _driver;

        /*public Hooks(IObjectContainer objectContainer)
        {
            _objectContainer = objectContainer;
        }*/

        [BeforeTestRun]
        public static void InitializeReport()
        {
            var htmlReporter = new ExtentHtmlReporter(@"G:\Automation\Lab2019\KinopoiskForSpecFlow\Kinopoisk\SpecflowKinopoisk\ExtentReport.html");
            htmlReporter.Config.Theme = AventStack.ExtentReports.Reporter.Configuration.Theme.Dark;

            _extent = new ExtentReports();
            _extent.AttachReporter(htmlReporter);

        }

        [AfterTestRun]
        public static void TearDownReport()
        {
            _extent.Flush();
        }

        [BeforeFeature]
        public static void BeforeFeature()
        {
            _featureName = _extent.CreateTest<Feature>(FeatureContext.Current.FeatureInfo.Title);
        }

        [AfterStep]
        public void InsertReportingSteps()
        {
            _scenario.CreateNode<Given> (ScenarioStepContext.Current.StepInfo.Text);
        }

        [BeforeScenario]
        public void BeforeScenario()
        {
            /*Tests = new KinopoiskTests();
            ConciseApi = Tests.ConciseApi;
            _driver = Tests.Driver;
            _objectContainer.RegisterInstanceAs(_driver);*/

            /*Tests = new KinopoiskTests();
            ConciseApi = Tests.ConciseApi;*/
            _scenario = _featureName.CreateNode<Scenario>(ScenarioContext.Current.ScenarioInfo.Title);
            Tests.SetUp();
        }

        [AfterScenario]
        public void AfterScenario()
        {
            //_extent.Flush();
            Tests.TearDown();
        }
    }
}
