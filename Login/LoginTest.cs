using AddBusinessOnXero.SeleniumDriver;
using FluentAssertions;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Linq;

namespace AddBusinessOnXero.Login
{
    class LoginTest
    {
        IWebDriver driver;
        PageElements pageElements = new PageElements();
        DefaultValues defaultValues = new DefaultValues();
        ISeleniumDriver webDriver = new SeleniumWebDriver();

        [SetUp]
        public void StartDriver()
        {
            //Load Browser
            driver = webDriver.GetChromeDriver(defaultValues.url);
        }

        // This test logins to Xero Production then Add Test Business
        [Test]
        public void AddBusinessTest()
        {
            int timeout = defaultValues.timeout;
            string testAccountName = "testAccount" + Guid.NewGuid().ToString("n").Substring(0, 4);
            var logo = By.XPath($"//a[@data-automationid = 'bankWidget']/h3[text() = '{testAccountName}']/ancestor::header/following-sibling::div/img");

            Random r = new Random();
            string accountNumberValue = r.Next(0, 10000000).ToString("D7");

            //Click Login button if element is found
            webDriver.ClickAfterLoad(pageElements.loginButtonLocation, timeout);

            //Enter details on LoginPage
            webDriver.EnterText(pageElements.email, defaultValues.emailAddress, timeout);
            webDriver.EnterText(pageElements.password, defaultValues.password, timeout);
            webDriver.ClickAfterLoad(pageElements.secondLogin, timeout);

            //Use Backup Method
            webDriver.ClickAfterLoad(pageElements.useBackupMethod, timeout);

            //Use Security Questions
            webDriver.ClickAfterLoad(pageElements.questionsSelection, timeout);

            //Answer Security Questions
            EnterSecurityAnswers(webDriver, pageElements.firstAnswer, pageElements.secondAnswer, pageElements.confirmButton, timeout);

            //Add new Bank Account
            webDriver.ClickAfterLoad(pageElements.accountingButton, timeout);
            webDriver.ClickAfterLoad(pageElements.bankAccountsButton, timeout);
            webDriver.ClickAfterLoad(pageElements.addBankAccount, timeout);

            //BankSelection
            webDriver.EnterText(pageElements.bankSearch, defaultValues.bankSearchText, timeout);
            webDriver.ClickAfterLoad(pageElements.searchResult, timeout);

            //Enter Bank Account Details
            webDriver.EnterText(pageElements.accountName, testAccountName, timeout);
            webDriver.ClickAfterLoad(pageElements.accountType, timeout);
            webDriver.ClickAfterLoad(pageElements.selectLoan, timeout);
            webDriver.EnterText(pageElements.accountNumber, accountNumberValue, timeout);

            if (webDriver.FindVisibleElement(pageElements.continueButton, timeout).Displayed == true)
                webDriver.ClickAfterLoad(pageElements.continueButton, timeout);

            webDriver.ClickAfterLoad(pageElements.gotForm, timeout);
            webDriver.ClickAfterLoad(pageElements.laterButton, timeout);
            webDriver.ClickAfterLoad(pageElements.goToDashboard, timeout);

            //Validate Correct Bank Details have been added
            if (webDriver.IsElementVisible(logo, timeout) == true)
            {
                var newAccountNames = driver.FindElements(pageElements.newAccountName).ToList();
                var newAccountNumbers = driver.FindElements(pageElements.newAccountNumber).ToList();
                var newLogo = driver.FindElement(logo);

                newAccountNames.Should().ContainSingle(x => x.Text == testAccountName);
                newAccountNumbers.Should().ContainSingle(x => x.Text == accountNumberValue);
                newLogo.Displayed.Should().BeTrue();
            }
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }

        public (string answer1, string answer2) GetSecurityAnswer(ISeleniumDriver webdriver, int timeout)
        {
            string answer1 = null;
            string answer2 = null;

            var questionSet = defaultValues.securityQuestions;
            string question1 = webdriver.FindVisibleElement(pageElements.firstQuestion, timeout).Text;
            string question2 = webdriver.FindVisibleElement(pageElements.secondQuestion, timeout).Text;

            if (questionSet.ContainsKey(question1) && questionSet.ContainsKey(question2))
            {
                answer1 = questionSet.Where(x => x.Key == question1).Select(y => y.Value).FirstOrDefault(); 
                answer2 = questionSet.Where(x => x.Key == question2).Select(y => y.Value).FirstOrDefault();
            }

            return (answer1, answer2);
        }

        public void EnterSecurityAnswers(ISeleniumDriver webdriver, By firstAnswerBox, By secondAnswerBox, By confirm, int timeout)
        {
            if (webdriver.IsElementVisible(confirm, timeout) == true)
            {
                var (answer1, answer2) = GetSecurityAnswer(webdriver, timeout);
                webDriver.EnterText(firstAnswerBox, answer1, timeout);
                webDriver.EnterText(secondAnswerBox, answer2, timeout);
                webdriver.ClickAfterLoad(confirm, timeout);
            }
        }

    }
}



