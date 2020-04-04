using System;
using System.IO;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using AventStack.ExtentReports.Reporter;
using BoDi;
using Kinopoisk.Tests;
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Bindings;

namespace SpecflowKinopoisk
{
    [Binding]
    public sealed class Hooks
    {
        private readonly IObjectContainer _objectContainer;
        private KinopoiskTests _tests;
        private IWebDriver _driver;
        private readonly ScenarioContext _scenarioContext;
        private static ExtentTest _featureName;
        private static ExtentTest _scenario;
        private static ExtentReports _extent;

        public Hooks(IObjectContainer objectContainer, ScenarioContext scenarioContext)
        {
            _objectContainer = objectContainer;
            _scenarioContext = scenarioContext;
        }
        
        [BeforeTestRun]
        public static void InitializeReport()
        {
            string reportPath =
            Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent?.FullName + "ExtentReport.html";

            var htmlReporter = new ExtentHtmlReporter(reportPath);
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
        public static void BeforeFeature(FeatureContext featureContext)
        {
            _featureName = _extent.CreateTest<Feature>(featureContext.FeatureInfo.Title);
        }     

        [BeforeScenario]
        public void BeforeScenario()
        {
            _scenario = _featureName.CreateNode<Scenario>(_scenarioContext.ScenarioInfo.Title);
            _tests = new KinopoiskTests();
            _driver = _tests.Driver;
            _objectContainer.RegisterInstanceAs(_driver);
            _tests.SetUp();
        }

        [AfterStep]
        public void InsertReportingSteps()
        {
            var stepType = ScenarioStepContext.Current.StepInfo.StepInstance.StepDefinitionKeyword;
            if (_scenarioContext.TestError == null)
            {
                switch (stepType)
                {

                    case StepDefinitionKeyword.Given:
                        _scenario.CreateNode<Given>(_scenarioContext.CurrentScenarioBlock.ToString(), ScenarioStepContext.Current.StepInfo.Text);
                        _scenario.CreateNode<Given>(ScenarioStepContext.Current.StepInfo.Text);
                        break;
                    case StepDefinitionKeyword.And:
                        _scenario.CreateNode<And>(ScenarioStepContext.Current.StepInfo.StepInstance.Keyword);
                        _scenario.CreateNode<And>(ScenarioStepContext.Current.StepInfo.Text);
                        break;
                    case StepDefinitionKeyword.When:
                        _scenario.CreateNode<When>(ScenarioStepContext.Current.StepInfo.StepInstance.Keyword);
                        _scenario.CreateNode<When>(ScenarioStepContext.Current.StepInfo.Text);
                        break;
                    case StepDefinitionKeyword.Then:
                        _scenario.CreateNode<Then>(ScenarioStepContext.Current.StepInfo.StepInstance.Keyword);
                        _scenario.CreateNode<Then>(ScenarioStepContext.Current.StepInfo.Text);
                        break;
                }
            }
            else if (_scenarioContext.TestError != null)
            {
                switch (stepType)
                {

                    case StepDefinitionKeyword.Given:
                        _scenario.CreateNode<Given>(ScenarioStepContext.Current.StepInfo.StepInstance.Keyword);
                        _scenario.CreateNode<Given>(ScenarioStepContext.Current.StepInfo.Text).Fail(_scenarioContext.TestError.InnerException);
                        break;
                    case StepDefinitionKeyword.And:
                        _scenario.CreateNode<And>(ScenarioStepContext.Current.StepInfo.StepInstance.Keyword);
                        _scenario.CreateNode<And>(ScenarioStepContext.Current.StepInfo.Text).Fail(_scenarioContext.TestError.InnerException);
                        break;
                    case StepDefinitionKeyword.When:
                        _scenario.CreateNode<When>(ScenarioStepContext.Current.StepInfo.StepInstance.Keyword);
                        _scenario.CreateNode<When>(ScenarioStepContext.Current.StepInfo.Text).Fail(_scenarioContext.TestError.InnerException);
                        break;
                    case StepDefinitionKeyword.Then:
                        _scenario.CreateNode<Then>(ScenarioStepContext.Current.StepInfo.StepInstance.Keyword);
                        _scenario.CreateNode<Then>(ScenarioStepContext.Current.StepInfo.Text).Fail(_scenarioContext.TestError.Message);
                        break;
                }
            }
           
        }


        [AfterScenario]
        public void AfterScenario()
        {
            _tests.TearDown();
        }
    }
}
