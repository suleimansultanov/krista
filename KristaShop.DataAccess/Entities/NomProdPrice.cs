using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KristaShop.DataAccess.Entities
{
    [Table("nom_prod_price")]
    public class NomProdPrice
    {
        [Key]
        public Guid id { get; set; }
        public Guid product_id { get; set; }
        public Guid nom_id { get; set; }
        [ForeignKey("nom_id")]
        public Nomenclature Nomenclature { get; set; }
        public double price { get; set; }
    }
}
