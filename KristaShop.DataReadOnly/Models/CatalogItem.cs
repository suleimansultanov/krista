using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KristaShop.DataReadOnly.Models
{
    [Table("catalog_item")]
    public class CatalogItem
    {
        [Key]
        public Guid id { get; set; }
        public int level { get; set; }
        public Guid product_id { get; set; }
        public string item_name { get; set; }
        public double volume { get; set; }
        public double weight { get; set; }
        public double parts_count { get; set; }

        public Guid options_id { get; set; }
        [ForeignKey("options_id")]
        public Option Option { get; set; }

        public Guid nomenclature_id { get; set; }
        [ForeignKey("nomenclature_id")]
        public Nomenclature Nomenclature { get; set; }
    }
}
