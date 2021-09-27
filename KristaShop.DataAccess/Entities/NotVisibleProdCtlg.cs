using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KristaShop.DataAccess.Entities
{
    [Table("not_visible_prod_ctlgs")]
    public class NotVisibleProdCtlg
    {
        [Key]
        public Guid id { get; set; }
        public Guid product_id { get; set; }
        public Guid nom_id { get; set; }
        [ForeignKey("nom_id")]
        public Nomenclature Nomenclature { get; set; }
        public Guid catalog_id { get; set; }
        [ForeignKey("catalog_id")]
        public Catalog Catalog { get; set; }
    }
}
