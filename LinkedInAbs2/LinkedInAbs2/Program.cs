using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
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
      
            //ChromeOptions options = new ChromeOptions();
            //string rootPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            var chromeDriver = new ChromeDriver();
            chromeDriver.Manage().Window.Maximize();
            Console.WriteLine(chromeDriver.Manage().Window.Size);
            chromeDriver.Navigate().GoToUrl("https://www.linkedin.com");
            //chromeDriver.SwitchTo().Window(chromeDriver.WindowHandles[0]);
            IWebElement emailinput = chromeDriver.FindElement(By.CssSelector("input.login-email"));
            IWebElement passinput = chromeDriver.FindElement(By.CssSelector("input.login-password"));
            IWebElement loginb = chromeDriver.FindElement(By.Id("login-submit"));
            emailinput.SendKeys("sofia@abstracta.us");
            passinput.SendKeys("abstracta.sv_2015");
            //Thread.Sleep(1000);
            loginb.Click();
            Thread.Sleep(2000);
            chromeDriver.Navigate().GoToUrl("https://www.linkedin.com/sales?trk=d_flagship3_nav");
            WebDriverWait wait = new WebDriverWait(chromeDriver,new TimeSpan(0,5,30));
            IWebElement searchdone = wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("a.view-all-filters")));
            
            ((IJavaScriptExecutor)chromeDriver).ExecuteScript("window.scrollTo(0, document.body.scrollHeight - 150)");
            Thread.Sleep(2000);
            ((IJavaScriptExecutor)chromeDriver).ExecuteScript("window.scrollTo(0, 0)");
            Thread.Sleep(2000);

            var userurl = chromeDriver.FindElements(By.CssSelector("a.name-link.profile-link"));
            for (int i = 0; i < userurl.Count; i++)
            {
                IWebElement url = userurl[i];
                if(url.Text != "LinkedIn Member") { 
                new Actions(chromeDriver)
                    .KeyDown(Keys.Control)
                    .Click(url)
                    .KeyUp(Keys.Control)
                    .Perform();
                Thread.Sleep(300);
                chromeDriver.SwitchTo().Window(chromeDriver.WindowHandles[1]);
                String currentURL = chromeDriver.Url;
                Lead_page_salesnav lead = new Lead_page_salesnav(chromeDriver);
                Console.WriteLine(lead.Lead.Name);
                Console.WriteLine(lead.Lead.Title);
                Console.WriteLine(lead.Lead.Location);
                Console.WriteLine(lead.Lead.Current_Position);
                Console.WriteLine(lead.Lead.Company_Name);
                Console.WriteLine(lead.Lead.Company_Size);
                Console.WriteLine(lead.Lead.Company_Page);
                Console.WriteLine("");

                lead.close();
                chromeDriver.SwitchTo().Window(chromeDriver.WindowHandles[0]);
                    //Console.WriteLine(userurl[i].GetAttribute("href"));
                }
            }

        }
    }
}
