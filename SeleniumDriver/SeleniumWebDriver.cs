using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;

namespace AddBusinessOnXero.SeleniumDriver
{
    interface ISeleniumDriver
    {
        IWebDriver GetChromeDriver(string url);
        bool IsElementVisible(By locator, int timeout);
        void Click(By locator);
        void EnterText(By locator, string text);
        void Close(IWebDriver driver);
    }

    class SeleniumWebDriver : ISeleniumDriver
    {
        IWebDriver driver;

        public IWebDriver GetChromeDriver(string url)
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("start-maximized");

            driver = new ChromeDriver(options);
            driver.Url = url;
            return driver;
        }

        public bool IsElementVisible(By locator, int timeOut)
        {
            try
            {
                new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut)).Until(x => x.FindElement(locator));
                return true;
            }
            catch (WebDriverTimeoutException)
            {
                return false;
            }
        }

        public void Click(By locator)
        {
            driver.FindElement(locator).Click();
        }

        public void EnterText(By locator, string text)
        {
            driver.FindElement(locator).SendKeys(text);
        }

        public void Close(IWebDriver driver)
        {
            driver.Close();
        }
    }
}
