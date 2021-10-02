using OpenQA.Selenium;
using System.Collections.Generic;

namespace AddBusinessOnXero
{
    class DefaultValues
    {
        public string url = "https://www.xero.com/nz/";
        public string emailAddress = "taekim4788@gmail.com";
        public string password = "kimtae47";
        public string bankSearchText = "anz";

        public Dictionary<string, string> securityQuestions = new Dictionary<string, string>()
        {
            { "As a child, what did you want to be when you grew up?", "Scientist" },
            { "What is the phone number you remember best from your childhood?", "096340691" },
            { "Who is your favourite person from history?", "Thomas Edison" },
        };
    }
}
