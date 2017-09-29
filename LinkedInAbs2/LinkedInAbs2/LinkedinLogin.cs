using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LinkedInAbs2
{
    class LinkedinLogin
    {
        Dictionary<String, String> selectors = new Dictionary<string, string>(){
            { "css_elem_email", "input.login-email" },
            { "css_elem_pass", "input.login-password" },
            { "id_elem_btn_login", "login-submit" }
        };

        private ChromeDriver driver;

        public LinkedinLogin(ChromeDriver driver)
        {
            this.driver = driver;
        }
        public void login(String email, String password)
        {
            driver.Navigate().GoToUrl("https://www.linkedin.com");
            //chromeDriver.SwitchTo().Window(chromeDriver.WindowHandles[0]);
            IWebElement emailinput = driver.FindElement(By.CssSelector(selectors["css_elem_email"]));
            IWebElement passinput = driver.FindElement(By.CssSelector(selectors["css_elem_pass"]));
            IWebElement loginb = driver.FindElement(By.Id(selectors["id_elem_btn_login"]));
            emailinput.SendKeys(email);
            passinput.SendKeys(password);
            //Thread.Sleep(1000);
            loginb.Click();
            Thread.Sleep(2000);
        }
    }
}
