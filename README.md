# xeroTestProject
Performs test on Xero production to add new ANZ bank account on existing Xero Organisation

## Setup
This test has been implemented with NUnit test framework with Selenium WebDriver
Test will run on Chrome Browser

## File structure
* DefaultValues.cs: contains default variables
* PageElements.cs: contains list of element locators used
* SeleniumWebDriver.cs: contais all methods for webdriver
* LoginTest.cs: contain actual test method

## Installation
Clone the repo
   git clone https://github.com/taekim4788/xeroTestProject.git

## Steps to run
1. Open "AddBusinessOnXero.sln" file with Visual Studio
2. Build the Solution
3. Run the test from Test Explorer
4. If test fails, screenshot will be stored in "xeroTestProject/Screenshots" folder
