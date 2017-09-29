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
    class SalesNavIterationEngine
    {
        private ChromeDriver driver;
        private String conn_message;
        private List<String> target_list;
        List<Lead> lead_list;

        public SalesNavIterationEngine(ChromeDriver driver, String conn_message)
        {
            this.driver = driver;
            this.conn_message = conn_message;
            lead_list = new List<Lead>();
            driver.Navigate().GoToUrl("https://www.linkedin.com/sales?trk=d_flagship3_nav");
            WebDriverWait wait = new WebDriverWait(driver, new TimeSpan(0, 5, 30));
            IWebElement searchdone = wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("a.view-all-filters")));
            Helper.fullPageScroll(driver);
            var userurls = driver.FindElements(By.CssSelector("a.name-link.profile-link"));
            //List<IWebElement> source = userurls.ToList();
            //var rnd = new Random();
            //var userurl = source.OrderBy(item => rnd.Next()).ToList();
            target_list = new List<string>();
            
            for (var i = 0; i < userurls.Count; i++)
            {
                target_list.Add(userurls[i].GetAttribute("href"));
            }
        }
        public SalesNavIterationEngine(ChromeDriver driver, String csv_path, String conn_message)
        {
            this.driver = driver;
            this.conn_message = conn_message;
        }

        public void startIterationSalesNav()
        {
            for (int i = 0; i < target_list.Count; i++)
            {
                String url = target_list[i];
                if (url != "LinkedIn Member")
                {
                    Lead lead = getLead(url);
                    lead_list.Add(lead);
                    Helper.WriteToCSV(lead);
                    //Console.WriteLine(userurl[i].GetAttribute("href"));
                    Random rand = new Random();
                    int random_time = rand.Next(5000, 20000);
                    //Thread.Sleep(random_time);
                }
            }
            var nextpage = goNextPage();
        }
        private Lead getLead(String url)
        {
            //Actions act = new Actions(chromeDriver);
            //act.MoveToElement(userurl[i]);
            //act.Perform();
            //Helper.ScrollToView(driver, url);
            //new Actions(driver)
            //.KeyDown(Keys.Control)
            //.Click(url)
            //.KeyUp(Keys.Control)
            //.Perform();
            //Thread.Sleep(300);
            //driver.SwitchTo().Window(driver.WindowHandles[1]);

            Helper.openNewTab(driver, url);
            driver.SwitchTo().Window(driver.WindowHandles.Last());

            String currentURL = driver.Url;
            Lead_page_salesnav lead = new Lead_page_salesnav(driver);
            lead.send_connection(conn_message);
            Console.WriteLine(lead.Lead.Name);
            Console.WriteLine(lead.Lead.Title);
            Console.WriteLine(lead.Lead.Location);
            Console.WriteLine(lead.Lead.Current_Position);
            Console.WriteLine(lead.Lead.Company_Name);
            Console.WriteLine(lead.Lead.Company_Size);
            Console.WriteLine(lead.Lead.Company_Page);
            Console.WriteLine(lead.Lead.Company_Industry);
            Console.WriteLine("");
            Hunter hunter = new Hunter(lead.Lead);
            String email = hunter.getEmail();
            lead.close();
            driver.SwitchTo().Window(driver.WindowHandles[0]);
            return lead.Lead;
        }
        public List<Lead> getLeadList()
        {
            return lead_list;
        }
        public bool goNextPage()
        {
            var next = false;

            IWebElement nextbutton = Helper.TryFindElement(driver, By.CssSelector("a.next-pagination.page-link"));
            if(nextbutton != null)
            {
                nextbutton.Click();
                Thread.Sleep(2000);
                next = true;
            }
            else { 
                next = false;
            }
            return next;
        }
        
    }
}
