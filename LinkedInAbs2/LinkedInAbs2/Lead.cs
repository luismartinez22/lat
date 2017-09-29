using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedInAbs2
{
    class Lead
    {
        public String Name { get; set; }
        public String Title { get; set; }
        public String Current_Position { get; set; }
        public String Location { get; set; }
        public String Email { get; set; }
        public String Email_Score { get; set; }
        public String Company_Name { get; set; }
        public String Company_Size { get; set; }
        public String Company_Page { get; set; }
        public String Company_Industry { get; set; }
        public String Profile_Url { get; set; }

        public String getCSVLine()
        {
            return Name + "," + Title + "," + Current_Position + "," + Location + "," + Email + ","+ Email_Score + "," + Company_Name + "," + Company_Size + "," + Company_Page + "," + Company_Industry + ",'" + Profile_Url + "'";
        }
    }
}
