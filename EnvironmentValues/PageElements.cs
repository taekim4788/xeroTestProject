using OpenQA.Selenium;

namespace AddBusinessOnXero
{
    class PageElements
    {
        //Main Page
        public By loginButtonLocation = By.XPath("(//a[contains(@href, 'https://login.xero.com/identity/user/login')])[last()]");
        
        // Login Page
        public By email = By.Id("xl-form-email");
        public By password = By.Id("xl-form-password");
        public By secondLogin = By.Id("xl-form-submit");

        //Authentication Page
        public By useBackupMethod = By.XPath("//button[text() = 'Use a backup method instead']");
        public By questionsSelection = By.XPath("//button[@data-automationid = 'auth-authwithsecurityquestionsbutton']");

        //Secuirty Questions Page
        public By firstQuestion = By.XPath("//label[@data-automationid = 'auth-firstanswer--label']");
        public By secondQuestion = By.XPath("//label[@data-automationid = 'auth-secondanswer--label']");
        public By firstAnswer = By.XPath("//input[@data-automationid = 'auth-firstanswer--input']");
        public By secondAnswer = By.XPath("//input[@data-automationid = 'auth-secondanswer--input']");
        public By confirmButton = By.XPath("//button[@data-automationid = 'auth-submitanswersbutton']");

        //Application
        public By accountingButton = By.XPath("//button[text() = 'Accounting']");
        public By bankAccountsButton = By.XPath("//a[text() = 'Bank accounts']");
        public By addBankAccount = By.XPath("//a[contains(@href, '/app/!rlBmk/bank-search')]");
        public By bankSearch = By.Id("bankSearch-input");
        public By searchResult = By.XPath("//span[text() = 'ANZ (NZ)']/ancestor::a[@data-automationid = 'searchResultItem-0']");

        //Bank Account Details
        public By accountName = By.Id("accountname-1025-inputEl");
        public By accountType = By.Id("accounttype-1027-inputEl");
        public By selectLoan = By.XPath("//ul/li[text() = 'Loan']");
        public By accountNumber = By.Id("accountnumber-1056-inputEl");
        public By continueButton = By.XPath("//a[@data-automationid = 'continueButton']");

        public By gotForm = By.XPath("//a[@data-automationid = 'connectbank-buttonIHaveAForm']");
        public By laterButton = By.XPath("//a[@data-automationid = 'uploadForm-uploadLaterButton']");
        public By goToDashboard = By.XPath("//a[@data-automationid = 'uploadFormLater-goToDashboardButton']");

        //Returned AccountName and AccountNumber
        public By newAccountName = By.XPath("//a[@data-automationid = 'bankWidget']/h3");
        public By newAccountNumber = By.XPath("//a[@data-automationid = 'bankWidget']/div");
    }
}
