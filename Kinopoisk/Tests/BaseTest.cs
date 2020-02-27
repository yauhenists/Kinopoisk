using System;
using System.IO;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Kinopoisk.Tests
{
    public abstract class BaseTest
    {
        protected static readonly string DownloadPath = 
            Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent?.FullName + @"\Download";
        protected IWebDriver Driver { get; }
        protected ConciseApi ConciseApi { get; }

        public BaseTest()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddUserProfilePreference("download.default_directory", DownloadPath);
            Driver = new ChromeDriver(options);
            ConciseApi = new ConciseApi(Driver);
        }

        [SetUp]
        public void SetUp()
        {
            Driver.Manage().Window.Maximize(); 
        }

        [TearDown]
        public void TearDown()
        {
            Driver.Quit();
        }
    }
}
