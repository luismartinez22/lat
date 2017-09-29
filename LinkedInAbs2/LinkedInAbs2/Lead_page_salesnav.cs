using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;

namespace LinkedInAbs2
{
    class Lead_page_salesnav
    {
        private ChromeDriver driver;

        Dictionary<String, String> selectors = new Dictionary<string, string>(){
            { "css_elem_name", "#topcard .info-container .member-name" },
            { "css_elem_title", "#topcard .info-container .title" },
            { "css_elem_location", "#topcard .info-container .location-industry .location" },
            { "css_elem_current_position", "#topcard > div.module-body > div > div.profile-info > ul:nth-child(5) > li:nth-child(1)" },
            { "css_elem_company_link", "#experience .positions .position:nth-child(1) .position-info .company-name a" },
            { "css_elem_carret_open_connect", "#topcard > div.module-body > div > div.profile-actions > div > button > span" },
            { "css_elem_btn_connect_with_carret", "#topcard > div.module-body > div > div.profile-actions > div > div > dl > dd > button" },
            { "css_elem_btn_connect", "#topcard > div.module-body > div > div.profile-actions > button" },
            { "id_elem_message_connect", "connect-message-content" },
            { "css_elem_btn_connect_submit", "#connect-dialog .submit-button" }
        };

        private String _apiKey = "396912b4475db918c13259b73fb34dfca469a83d";

        private IWebElement elem_name;
        private IWebElement elem_title;
        private IWebElement elem_location;
        private IWebElement elem_current_position;
        private IWebElement elem_company_name;
        private IWebElement elem_btn_save_as_lead;
        private IWebElement elem_carret_open_connect;
        private IWebElement elem_btn_connect;
        private IWebElement elem_btn_connect_submit;

        private String lead_url;
        public Lead Lead { get; set; }
        public Lead_page_salesnav(ChromeDriver driver)
        {
            this.driver = driver;
            //this.lead_url = lead_url;
            this.Lead = new Lead();

            //driver.SwitchTo().Window(driver.WindowHandles[1]);
            //driver.Navigate().GoToUrl(lead_url);

            WebDriverWait wait = new WebDriverWait(driver, new TimeSpan(0, 0, 30));

            elem_name = wait.Until(ExpectedConditions.ElementExists(By.CssSelector(selectors["css_elem_name"])));
            elem_title = driver.FindElement(By.CssSelector(selectors["css_elem_title"]));
            elem_location = driver.FindElement(By.CssSelector(selectors["css_elem_location"]));
            elem_current_position = Helper.TryFindElement(driver,By.CssSelector(selectors["css_elem_current_position"]));

            Lead.Name = elem_name.Text;
            Lead.Title = elem_title.Text;
            Lead.Location = elem_location.Text;
            if(elem_current_position != null) { 
                Lead.Current_Position = elem_current_position.Text;
            }
            Lead.Profile_Url = driver.Url;

            IWebElement aux_c = checkIfElementExists(driver, By.CssSelector(selectors["css_elem_company_link"]));
            if (aux_c != null)
            {
                Helper.ScrollToView(driver,aux_c);
                new Actions(driver)
                    .KeyDown(Keys.Control)
                    .Click(aux_c)
                    .KeyUp(Keys.Control)
                    .Perform();
                driver.SwitchTo().Window(driver.WindowHandles[2]);
                Company_page_salesnav company_page = new Company_page_salesnav(driver);
                Lead.Company_Name = company_page.get_company_name();
                Lead.Company_Size = company_page.get_company_size();
                Lead.Company_Page = company_page.get_company_page();
                Lead.Company_Industry = company_page.get_company_industry();
                company_page.close();
                driver.SwitchTo().Window(driver.WindowHandles[1]);
            }
        }
        private IWebElement checkIfElementExists(IWebDriver driver, By selector)
        {
            IWebElement elem;
            try
            {
                elem = driver.FindElement(selector);
            }
            catch (Exception e)
            {
                elem = null;
            }
            return elem;
        }
        public void send_connection(String message)
        {
            WebDriverWait wait = new WebDriverWait(driver, new TimeSpan(0, 0, 10));
            try { 
                elem_carret_open_connect = driver.FindElement(By.CssSelector(selectors["css_elem_carret_open_connect"]));
                Helper.ScrollToView(driver, elem_carret_open_connect);
                Actions action = new Actions(driver);
                action.MoveToElement(elem_carret_open_connect).Perform();
                elem_btn_connect = wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(selectors["css_elem_btn_connect_with_carret"])));
                elem_btn_connect.Click();
                Thread.Sleep(2000);
            }
            catch(Exception ex)
            {
                elem_btn_connect = driver.FindElement(By.CssSelector(selectors["css_elem_btn_connect"]));
                Helper.ScrollToView(driver, elem_btn_connect);
                elem_btn_connect.Click();
                Thread.Sleep(2000);
            }
            IWebElement elem_message_connect = wait.Until(ExpectedConditions.ElementExists(By.Id(selectors["id_elem_message_connect"])));
            String processed_message = replace_variables_message(message);
            elem_message_connect.Clear();
            elem_message_connect.SendKeys(processed_message);
            elem_btn_connect_submit = driver.FindElement(By.CssSelector(selectors["css_elem_btn_connect_submit"]));
            elem_btn_connect_submit.Click();
        }
        
        public String replace_variables_message(String message)
        {
            String result = message;

            //Brian me sugirio que hiciera esa cosa chancha
            String lead_name = Lead.Name.Split()[0];
            if (message.IndexOf("%name%")>0)
            {
                message = message.Replace("%name%", lead_name);
            }
            if (message.IndexOf("%company%") > 0)
            {
                message = message.Replace("%company%", Lead.Company_Name);
            }
            if (message.IndexOf("%title%") > 0)
            {
                message = message.Replace("%title%", Lead.Title);
            }
            return message;
        }
        public void close()
        {
            driver.SwitchTo().Window(driver.CurrentWindowHandle).Close();
        }
    }

}
