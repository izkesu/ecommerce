using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace ecommerce.Models
{
    public class tb1_user1
    {
        public int u_id { get; set; }
        ////[Required(ErrorMessage = "*")]
        public string u_name { get; set; }
        //[Required(ErrorMessage = "*")]
        public string u_email { get; set; }
        public string u_password { get; set; }

        public string u_image { get; set; }

        public string u_contact { get; set; }

    }
}