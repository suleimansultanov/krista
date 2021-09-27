using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KristaShop.DataReadOnly.Models
{
    [Table("nom_list")]
    public class Nomenclature
    {
        [Key]
        public Guid id { get; set; }
        public string name { get; set; }
        public int nom_type { get; set; }
        public string articul { get; set; }
        public bool is_set { get; set; }
        public bool visible_on_site { get; set; }

        public virtual ICollection<CatalogItem> CatalogItems { get; set; }
    }
}
