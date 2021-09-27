using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KristaShop.DataAccess.Entities
{
    [Table("nom_category")]
    public class NomCategory
    {
        [Key]
        public Guid id { get; set; }
        public Guid nom_id { get; set; }
        [ForeignKey("nom_id")]
        public Nomenclature Nomenclature { get; set; }

        public Guid category_id { get; set; }
        [ForeignKey("category_id")]
        public Category Category { get; set; }
    }
}
