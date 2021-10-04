using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.Extensions;
using OpenQA.Selenium.Support.UI;
using System;

namespace AddBusinessOnXero.SeleniumDriver
{
    interface ISeleniumDriver
    {
        IWebDriver GetChromeDriver(string url);
        bool IsElementVisible(By locator, int timeout);
        IWebElement FindVisibleElement(By locator, int timeout);
        void ClickAfterLoad(By locator, int timeout);
        void EnterText(By locator, string text, int timeout);
        void ScreenCapture();
    }

    class SeleniumWebDriver : ISeleniumDriver
    {
        IWebDriver driver;
        DefaultValues defaultValues = new DefaultValues();

        public IWebDriver GetChromeDriver(string url)
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("start-maximized");

            driver = new ChromeDriver(options);
            driver.Url = url;
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
            return driver;
        }

        public bool IsElementVisible(By locator, int timeOut)
        {
            try
            {
                new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut)).Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(locator));
                return true;
            }
            catch (WebDriverTimeoutException ex)
            {
                ScreenCapture();
                throw new Exception("Unable to find element", ex);
            }
        }

        public IWebElement FindVisibleElement(By locator, int timeOut)
        {
            try
            {
                new WebDriverWait(driver, TimeSpan.FromSeconds(timeOut)).Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(locator));
                return driver.FindElement(locator);
            }
            catch (WebDriverTimeoutException ex)
            {
                ScreenCapture();
                throw new Exception("Unable to find visible element", ex);
            }
        }

        public void ClickAfterLoad(By locator, int timeout)
        {
            try
            {
                new WebDriverWait(driver, TimeSpan.FromSeconds(timeout)).Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(locator));
                IWebElement buttonToClick = driver.FindElement(locator);
                IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
                js.ExecuteScript("arguments[0].click()", buttonToClick);
            }
            catch (WebDriverTimeoutException ex)
            {
                ScreenCapture();
                throw new Exception("Unable to click the element", ex);
            }
        }

        public void EnterText(By locator, string text, int timeout)
        {
            try
            {
                new WebDriverWait(driver, TimeSpan.FromSeconds(timeout)).Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(locator));
                driver.FindElement(locator).SendKeys(text);
            }
            catch (Exception ex)
            {
                ScreenCapture();
                throw new Exception("Unable to enter text", ex);
            }
        }

        public void ScreenCapture()
        {
            var screenshot = driver.TakeScreenshot();
            screenshot.SaveAsFile(defaultValues.GetFileName(), ScreenshotImageFormat.Png);
        }
    }
}
