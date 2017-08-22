using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LinkedInAbs2
{
    class Program
    {
        static void Main(string[] args)
        {
      
            ChromeOptions options = new ChromeOptions();
            //string rootPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            var chromeDriver = new ChromeDriver();
            chromeDriver.Manage().Window.Maximize();
            Console.WriteLine(chromeDriver.Manage().Window.Size);
            chromeDriver.Navigate().GoToUrl("https://www.linkedin.com");
            chromeDriver.SwitchTo().Window(chromeDriver.WindowHandles[0]);
            IWebElement emailinput = chromeDriver.FindElement(By.CssSelector("input.login-email"));
            IWebElement passinput = chromeDriver.FindElement(By.CssSelector("input.login-password"));
            IWebElement loginb = chromeDriver.FindElement(By.Id("login-submit"));
            emailinput.SendKeys("sofia@abstracta.us");
            passinput.SendKeys("abstracta.sv_2015");
            Thread.Sleep(1000);
            loginb.Click();
            Thread.Sleep(2000);
            chromeDriver.Navigate().GoToUrl("https://www.linkedin.com/sales?trk=d_flagship3_nav");
            WebDriverWait wait = new WebDriverWait(chromeDriver,new TimeSpan(0,5,30));
            IWebElement searchdone = wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("a.view-all-filters")));

            var userurl = chromeDriver.FindElements(By.CssSelector("a.name-link.profile-link"));
            for (int i = 0; i < userurl.Count; i++)
            {
                String currentURL = chromeDriver.Url;
                IWebElement searchdone2 = wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("a.view-all-filters")));
                Lead_page_salesnav lead = new Lead_page_salesnav(userurl[i].GetAttribute("href"), chromeDriver);
                /*Console.WriteLine(lead.Name);
                Console.WriteLine(lead.Location);
                Console.WriteLine(lead.Position);
                Console.WriteLine(lead.Title);*/
                //chromeDriver.Navigate().GoToUrl(currentURL);
                
                //Console.WriteLine(userurl[i].GetAttribute("href"));
            }
        }
    }
}
