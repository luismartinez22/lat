using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LinkedInAbs2
{
    class Helper
    {
        public static IWebElement TryFindElement(ChromeDriver driver, By by)
        {
            try
            {
                IWebElement result;
                result = driver.FindElement(by);
                return result;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.StackTrace);
                return null;
            }
        }
        public static void ScrollTo(ChromeDriver driver, int xPosition = 0, int yPosition = 0)
        {
            var js = String.Format("window.scrollTo({0}, {1})", xPosition, yPosition);
            driver.ExecuteScript(js);
        }
        public static void ScrollToView(ChromeDriver driver ,IWebElement element)
        {
            if (element.Location.Y > 200)
            {
                ScrollTo(driver ,0, element.Location.Y - 100); // Make sure element is in the view but below the top navigation pane
                Thread.Sleep(500);
            }
        }
        public static void WriteToCSV(Lead lead)
        {
            String lead_string = "";
            String final_string = "";
            String read = "";
            try { 
                read = File.ReadAllText("leads.csv");
            }
            catch (Exception ex)
            {
                read = "";
            }
            lead_string = lead.getCSVLine() + Environment.NewLine;
            
            //Hardcoded heading for now
            if (read == "" || read == null)
            {
                final_string += "fullname,Title,currdesignation,location,email,Email Score,company,Company Size,Company Page,industry, linkedinid" + Environment.NewLine;
                final_string += lead_string;
            }
            else
            {
                final_string = read + lead_string;
            }
            File.WriteAllText("leads.csv", final_string);
        }
        public static void fullPageScroll(ChromeDriver driver)
        {
            ((IJavaScriptExecutor)driver).ExecuteScript("window.scrollTo(0, document.body.scrollHeight - 150)");
            Thread.Sleep(2000);
            ((IJavaScriptExecutor)driver).ExecuteScript("window.scrollTo(0, 0)");
            Thread.Sleep(2000);
        }
        public static void openNewTab(ChromeDriver driver, String url)
        {
            String script = "window.open('" + url + "')";
            driver.ExecuteScript(script);
        }
        
    }
}
