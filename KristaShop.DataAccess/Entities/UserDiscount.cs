using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace KristaShop.DataAccess.Entities
{
    [Table("user_discounts")]
    public class UserDiscount
    {
        [Key]
        public Guid id { get; set; }
        public Guid user_id { get; set; }
        public double discount_price { get; set; }
        public bool is_active { get; set; }
        public DateTime? start_date { get; set; }
        public DateTime? end_date { get; set; }
    }
}
