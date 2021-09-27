using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KristaShop.DataReadOnly.Models
{
    [Table("catalog_item_price")]
    public class CatalogItemPrice
    {
        [Key]
        public Guid id { get; set; }
        public Guid catalog_item_id { get; set; }
        public decimal price { get; set; }
        public Guid price_type_id { get; set; }
    }
}
