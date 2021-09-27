using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KristaShop.DataReadOnly.Models
{
    [Table("price_types")]
    public class PriceType
    {
        [Key]
        public Guid id { get; set; }
        public string name { get; set; }
        public int price_type { get; set; }
    }
}
