using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KristaShop.DataAccess.Entities
{
    [Table("catalogs")]
    public class Catalog
    {
        [Key]
        public Guid id { get; set; }
        [Required, MaxLength(64)]
        public string name { get; set; }
        [Required, MaxLength(64)]
        public string uri { get; set; }
        public int order_form { get; set; }
        public string description { get; set; }
        public string meta_title { get; set; }
        public string meta_keywords { get; set; }
        public string meta_description { get; set; }
        public int order { get; set; }
        public bool is_discount { get; set; }
        public bool is_visible { get; set; }

        public virtual ICollection<NomCatalog> NomCatalogs { get; set; }
    }
}
