using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KristaShop.DataAccess.Entities
{
    [Table("nom_discounts")]
    public class NomDiscount
    {
        [Key]
        public Guid id { get; set; }
        public Guid nom_id { get; set; }
        [ForeignKey("nom_id")]
        public Nomenclature Nomenclature { get; set; }
        public double discount_price { get; set; }
        public bool is_active { get; set; }
        public DateTime? start_date { get; set; }
        public DateTime? end_date { get; set; }
    }
}
