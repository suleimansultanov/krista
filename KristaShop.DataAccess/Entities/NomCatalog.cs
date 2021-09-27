using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KristaShop.DataAccess.Entities
{
    [Table("nom_catalog")]
    public class NomCatalog
    {
        [Key]
        public Guid id { get; set; }
        public Guid nom_id { get; set; }
        [ForeignKey("nom_id")]
        public Nomenclature Nomenclature { get; set; }

        public Guid catalog_id { get; set; }
        [ForeignKey("catalog_id")]
        public Catalog Catalog { get; set; }

        public int order { get; set; }
    }
}
