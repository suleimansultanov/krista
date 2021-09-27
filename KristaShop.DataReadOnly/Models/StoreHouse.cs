using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KristaShop.DataReadOnly.Models
{
    [Table("storehouses")]
    public class StoreHouse
    {
        [Key]
        public Guid id { get; set; }
        public bool is_for_online_store { get; set; }
    }
}
