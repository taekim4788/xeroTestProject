using System;
using System.Collections.Generic;
using System.IO;

namespace AddBusinessOnXero
{
    class DefaultValues
    {
        public string url = "https://www.xero.com/nz/";
        public string emailAddress = "taekim4788@gmail.com";
        public string password = "kimtae47";
        public string bankSearchText = "anz";
        public int timeout = 5;

        public Dictionary<string, string> securityQuestions = new Dictionary<string, string>()
        {
            { "As a child, what did you want to be when you grew up?", "Scientist" },
            { "What is the phone number you remember best from your childhood?", "096340691" },
            { "Who is your favourite person from history?", "Thomas Edison" },
        };

        public string GetFileName()
        {
            string currentPath = AppDomain.CurrentDomain.BaseDirectory;
            string path = currentPath.Substring(0, currentPath.LastIndexOf("bin")) + @"Screenshots";
            var currentTime = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fileName = path + @"\TestScreenshot" + currentTime + ".png";

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            return fileName;
        }
    }

    
}
