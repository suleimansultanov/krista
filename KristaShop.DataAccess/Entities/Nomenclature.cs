using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KristaShop.DataAccess.Entities
{
    [Table("nomenclatures")]
    public class Nomenclature
    {
        [Key]
        public Guid id { get; set; }
        public string articul { get; set; }
        public string name { get; set; }
        public decimal default_price { get; set; }
        public string description { get; set; }
        public string youtube_link { get; set; }
        public string meta_title { get; set; }
        public string link_name { get; set; }
        public string meta_keywords { get; set; }
        public string meta_description { get; set; }
        public bool is_visible { get; set; }
        public string image_path { get; set; }

        public virtual ICollection<NomCatalog> NomCatalogs { get; set; }
        public virtual ICollection<NomCategory> NomCategories { get; set; }
        public virtual ICollection<VisibleNomUser> VisibleNomUsers { get; set; }
    }
}
