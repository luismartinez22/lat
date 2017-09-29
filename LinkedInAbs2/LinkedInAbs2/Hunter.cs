using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace LinkedInAbs2
{
    class Hunter
    {
        private Lead Lead { get; set; }
        public String lead_email { get; set; }
        public String email_chance { get; set; }
        
        public Hunter(Lead lead)
        {
            this.Lead = lead;           
        }
        public String getEmail()
        {
            string result = "";
            var split_name = Lead.Name.Split();
            if(split_name.Length <= 2 && Lead.Company_Page != null) { 
                String first_name = split_name[0];
                String last_name = split_name[1];
                String c = Lead.Company_Page;
                //var d = c.Split(new Char[] {'.'});
                //String domain = domain[domain.Length - 3];

                String domain = GetDomain(c);

                String url_string = "https://api.hunter.io/v2/email-finder?domain=" + domain + "&first_name=" + first_name + "&last_name=" + last_name + "&api_key=396912b4475db918c13259b73fb34dfca469a83d";
                var request = (HttpWebRequest)WebRequest.Create(url_string);
                var response = (HttpWebResponse)request.GetResponse();
                var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                Console.WriteLine("Logging the request response");
                Console.WriteLine(responseString);
                result = responseString;
                JObject r = JObject.Parse(result);
                IList<JToken> results = r["data"].Children().ToList();
                Console.WriteLine(results);
                if(results[0] != null)
                {
                    Console.WriteLine(results[0]);
                    result = results[0].ToString();
                }
            }
            return result;
        }
        private String GetDomain(String dom)
        {
            var res = "";
            if(dom.IndexOf("www.") > 0)
            {
                res = dom.Substring(dom.IndexOf("www."));
            }
            else if (dom.IndexOf("http://") > 0)
            {
                res = dom.Substring(dom.IndexOf("http://"));
            }
            else if (dom.IndexOf("https://") > 0)
            {
                res = dom.Substring(dom.IndexOf("https://"));
            }
            return res;
        }
    }
}
