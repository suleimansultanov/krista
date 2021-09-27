using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KristaShop.DataReadOnly.Models
{
    [Table("storehouse_rests")]
    public class StoreHouseRest
    {
        [Key]
        public Guid id { get; set; }
        public int level { get; set; }
        public Guid storehouse_id { get; set; }
        [ForeignKey("storehouse_id")]
        public StoreHouse StoreHouse { get; set; }
        public Guid product_id { get; set; }
        public double count { get; set; }
        public Guid reservation_doc_id { get; set; }
    }
}
