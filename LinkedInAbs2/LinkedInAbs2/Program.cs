using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.IO;
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
            String conn_message = "Hi %name%, I was wondering if we could set up a call to chat about your QA needs and explore how I can help you with the challenges you might be facing.";

            //ChromeOptions options = new ChromeOptions();
            //string rootPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            var chromeDriver = new ChromeDriver();
            chromeDriver.Manage().Window.Maximize();
            Console.WriteLine(chromeDriver.Manage().Window.Size);
            LinkedinLogin loginPage = new LinkedinLogin(chromeDriver);
            loginPage.login("sofia@abstracta.us", "abstracta.sv_2015");
            SalesNavIterationEngine engine = new SalesNavIterationEngine(chromeDriver, conn_message);
            engine.startIterationSalesNav();
        }
    }
}
