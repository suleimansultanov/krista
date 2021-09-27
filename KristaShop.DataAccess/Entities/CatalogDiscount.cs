using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KristaShop.DataAccess.Entities
{
    [Table("catalog_discounts")]
    public class CatalogDiscount
    {
        [Key]
        public Guid id { get; set; }
        public Guid catalog_id { get; set; }
        [ForeignKey("catalog_id")]
        public Catalog Catalog { get; set; }
        public double discount_price { get; set; }
        public bool is_active { get; set; }
        public DateTime? start_date { get; set; }
        public DateTime? end_date { get; set; }
    }
}
