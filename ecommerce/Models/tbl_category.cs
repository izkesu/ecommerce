using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ecommerce.Models
{
    public class tb1_category
    {
        public int cat_id { get; set; }
        [Required(ErrorMessage = "*")]
        public string cat_name { get; set; }
        [Required(ErrorMessage = "*")]
        public string cat_image { get; set; }
        public Nullable<int> cat_fk_ad { get; set; }
    }
}