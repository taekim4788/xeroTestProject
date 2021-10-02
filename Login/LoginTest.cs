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

        // Further chagnes to do:
        // Add appropriate exceptions
        // Tidy up of architecture

        [Test]
        public void AddBusinessTest()
        {
            int timeout = 15;
            string testAccountName = "testAccount" + Guid.NewGuid().ToString("n").Substring(0, 4);

            Random r = new Random();
            string accountNumberValue = r.Next(0, 10000000).ToString("D7");

            
            //Load Browser
            driver = webDriver.GetChromeDriver(defaultValues.url);

            //Click Login button if element is found
            ClickAfterLoad(webDriver, pageElements.loginButtonLocation, timeout);

            //Enter details on LoginPage
            var secondLoginClickable = webDriver.IsElementVisible(pageElements.secondLogin, timeout);
            if (secondLoginClickable == true)
            {
                webDriver.EnterText(pageElements.email, defaultValues.emailAddress);
                webDriver.EnterText(pageElements.password, defaultValues.password);
                webDriver.Click(pageElements.secondLogin);
            }

            //Use Backup Method
            ClickAfterLoad(webDriver, pageElements.useBackupMethod, timeout);

            //Use Security Questions
            ClickAfterLoad(webDriver, pageElements.questionsSelection, timeout);

            //Answer Security Questions
            EnterSecurityAnswers(driver, webDriver, pageElements.firstAnswer, pageElements.secondAnswer, pageElements.confirmButton, timeout);

            //Add new Bank Account
            ClickAfterLoad(webDriver, pageElements.accountringButton, timeout);
            ClickAfterLoad(webDriver, pageElements.bankAccountsButton, timeout);
            ClickAfterLoad(webDriver, pageElements.addBankAccount, timeout);

            //BankSelection
            if (webDriver.IsElementVisible(pageElements.bankSearch, timeout) == true)
            {
                webDriver.EnterText(pageElements.bankSearch, defaultValues.bankSearchText);
                ClickAfterLoad(webDriver, pageElements.searchResult, timeout);
            }

            //Enter Bank Account Details
            if (webDriver.IsElementVisible(pageElements.accountName, timeout) == true)
            {
                webDriver.EnterText(pageElements.accountName, testAccountName);
                webDriver.Click(pageElements.accountType);
                ClickAfterLoad(webDriver, pageElements.selectLoan, timeout);
                webDriver.EnterText(pageElements.accountNumber, accountNumberValue);
                webDriver.Click(pageElements.continueButton);
            }

            ClickAfterLoad(webDriver, pageElements.gotForm, timeout);
            ClickAfterLoad(webDriver, pageElements.laterButton, timeout);
            ClickAfterLoad(webDriver, pageElements.goToDashboard, timeout);

            //Validate Correct Bank Details have been added
            if (webDriver.IsElementVisible(pageElements.logo, timeout) == true)
            {
                var newAccountName = driver.FindElement(pageElements.newAccountName);
                var newAccountNumber = driver.FindElement(pageElements.newAccountNumber);
                var newLogo = driver.FindElement(pageElements.logo);

                newAccountName.Text.Should().Be(testAccountName);
                newAccountNumber.Text.Should().Be(accountNumberValue);
                newLogo.Displayed.Should().BeTrue();
            }
            else
            {
                driver.Quit();
            }
            driver.Close();
        }

        public void ClickAfterLoad(ISeleniumDriver driver, By locator, int timeout)
        {
            if (driver.IsElementVisible(locator, timeout) == true)
            {
                driver.Click(locator);
            }
            else
            {
                //throw exception
                //close driver
            }
        }

        public (string answer1, string answer2) GetSecurityAnswer(IWebDriver driver)
        {
            var questionSet = defaultValues.securityQuestions;
            string question1 = driver.FindElement(pageElements.firstQuestion).Text;
            string question2 = driver.FindElement(pageElements.secondQuestion).Text;
            string answer1 = null;
            string answer2 = null;

            if (questionSet.ContainsKey(question1) && questionSet.ContainsKey(question2))
            {
                answer1 = questionSet.Where(x => x.Key == question1).Select(y => y.Value).FirstOrDefault(); 
                answer2 = questionSet.Where(x => x.Key == question2).Select(y => y.Value).FirstOrDefault();
            }

            else
            {
                //Throw Exception for answer not found
                driver.Quit();
            }

            return (answer1, answer2);
        }

        public void EnterSecurityAnswers(IWebDriver driver, ISeleniumDriver webdriver, By firstAnswerBox, By secondAnswerBox, By confirm, int timeout)
        {
            if (webdriver.IsElementVisible(confirm, timeout) == true)
            {
                var (answer1, answer2) = GetSecurityAnswer(driver);
                webDriver.EnterText(firstAnswerBox, answer1);
                webDriver.EnterText(secondAnswerBox, answer2);
                webdriver.Click(confirm);
            }
            else
            {
                //Throw Exception
                driver.Quit();
            }
        }

    }
}



