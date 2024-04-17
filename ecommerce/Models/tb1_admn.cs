using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ecommerce.Models
{
    using System.ComponentModel.DataAnnotations;
    public class tbl_admn
    {
        public int ad_id { get; set; }
        [Required(ErrorMessage = "*")]
        public string ad_username { get; set; }
        [Required(ErrorMessage = "*")]
        public string ad_password { get; set; }
        
    }
}