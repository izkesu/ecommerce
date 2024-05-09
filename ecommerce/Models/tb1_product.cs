using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ecommerce.Models
{
    public class tb1_product
    {
        public int pro_id { get; set; }
        ////[Required(ErrorMessage = "*")]
        public string pro_name { get; set; }
        //[Required(ErrorMessage = "*")]
        public string pro_des { get; set; }
        public string pro_price { get; set; }

        public string pro_image { get; set; }

        public string u_contact { get; set; }

        public Nullable<int> pro_fk_cat { get; set; }
    }
}