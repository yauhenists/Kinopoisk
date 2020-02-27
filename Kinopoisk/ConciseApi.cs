using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;

namespace Kinopoisk
{
    public class ConciseApi
    {
        private IWebDriver Driver { get; }

        public ConciseApi(IWebDriver driver)
        {
            Driver = driver;
        }

        public void OpenPage(string url)
        {
            Driver.Navigate().GoToUrl(url);
        }

        public void ClickOnElement(By locator)
        {
            GetElement(locator).Click();
        }

        public void ClickOnElementJs(By locator)
        {
            var element = GetElement(locator);
            IJavaScriptExecutor jse = (IJavaScriptExecutor) Driver;
            jse.ExecuteScript("arguments[0].click();", element);
        }

        public void ClearField(By locator)
        {
            GetElement(locator).Clear();
        }

        public void InputText(string text, By element)
        {
            GetElement(element).SendKeys(text);
        }

        public void InputTextAndExecute(string text, By element)
        {
            InputText(text, element);
            if (GetTextOfAttributeOfElement(element, "value") != text)
            {
                InputText(text, element);
            }
            GetElement(element).SendKeys(Keys.Enter);
        }

        public IWebElement GetElement(By locator)
        {
            return AssertThat(ExpectedConditions.ElementIsVisible(locator));
        }

        public string GetTextOfElement(By element)
        {
            return GetElement(element).Text;
        }

        public string GetTextOfAttributeOfElement(By element, string attribute)
        {
            return GetElement(element).GetAttribute(attribute);
        }

        public bool IsElementDisplayed(By locator)
        {
            return GetElement(locator).Displayed;
        }

        public void MoveCursorToElement(By locator)
        {
            Actions builder = new Actions(Driver);
            builder.MoveToElement(GetElement(locator)).Perform();
        }

        public void DragAndDrop(By dragElement, By dropElement)
        {
            Actions builder = new Actions(Driver);
            var drag = GetElement(dragElement);
            var drop = GetElement(dropElement);
            builder.DragAndDrop(drag, drop).Perform();
        }

        public void DragAndDrop(By dragElement, int x, int y)
        {
            Actions builder = new Actions(Driver);
            var drag = GetElement(dragElement);
            builder.DragAndDropToOffset(drag, x, y).Perform();
        }

        public void SelectElementFromList(By list, string text)
        {
            SelectElement select = new SelectElement(GetElement(list));
            select.SelectByText(text);
        }

        public List<string> GetListOfColumns(int table)
        {
            List<IWebElement> columnElements = new List<IWebElement>(Driver.FindElements(By.XPath($"//table[{table}]//th")));

            return new List<string>(columnElements.Select(x => x.Text));
        }

        public string GetFullRowOfTable(int row, int table)
        {
            List<string> listOfColumns = GetListOfColumns(table);
            StringBuilder builder = new StringBuilder();
            List<IWebElement> elementsInRow = new List<IWebElement>(Driver.FindElements(By.XPath($"//table[{table}]//tbody/tr[{row}]/td")));

            for (int i = 0; i < listOfColumns.Count; i++)
            {
                builder.AppendLine($"{listOfColumns[i]} : {elementsInRow[i].Text} \n");
            }

            return builder.ToString();
        }

        public T AssertThat<T>(Func<IWebDriver, T> condition)
        {
            DefaultWait<IWebDriver> wait = new DefaultWait<IWebDriver>(Driver);
            wait.PollingInterval = TimeSpan.FromMilliseconds(250);
            wait.Timeout = TimeSpan.FromSeconds(4);
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));

            return wait.Until(condition);
        }
    }
}
