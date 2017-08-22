using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedInAbs2
{
    class Company_page_salesnav
    {
        private IWebDriver driver;

        Dictionary<String, String> selectors = new Dictionary<string, string>(){
            { "css_company_name", "#main > div.top-bar.with-wide-image.with-nav > div > div.left-entity > div > h1" },
            { "css_company_size", "#main > div.top-bar.with-wide-image.with-nav > div > div.left-entity > div > ul > li.size.detail" },
            { "css_company_page", "#account-introduction > section.about-account > ul > li:nth-child(3) > p > a" }
        };

        private IWebElement elem_company_name;
        private IWebElement elem_company_size;
        private IWebElement elem_company_page;

        public Company_page_salesnav(IWebDriver driver)
        {
            this.driver = driver;

            WebDriverWait wait = new WebDriverWait(driver, new TimeSpan(0, 0, 30));

            elem_company_size = wait.Until(ExpectedConditions.ElementExists(By.CssSelector(selectors["css_company_size"])));
            elem_company_name = driver.FindElement(By.CssSelector(selectors["css_company_name"]));
            elem_company_page = driver.FindElement(By.CssSelector(selectors["css_company_page"]));
        }

        public String get_company_name()
        {
            if (elem_company_name != null)
            {
                return elem_company_name.Text;
            }
            else
            {
                return null;
            }
        }
        public String get_company_size()
        {
            if (elem_company_size != null)
            {
                return elem_company_size.Text;
            }
            else
            {
                return null;
            }
        }
        public String get_company_page()
        {
            if (elem_company_page != null)
            {
                return elem_company_page.Text;
            }
            else
            {
                return null;
            }
        }
        public void close()
        {
            driver.SwitchTo().Window(driver.CurrentWindowHandle).Close();
        }
    }
}
