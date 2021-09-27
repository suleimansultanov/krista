using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KristaShop.DataAccess.Entities
{
    [Table("nom_photos")]
    public class NomPhoto
    {
        [Key]
        public Guid id { get; set; }
        public Guid nom_id { get; set; }
        [ForeignKey("nom_id")]
        public Nomenclature Nomenclature { get; set; }
        public string photo_path { get; set; }
        public Guid? color_id { get; set; }
    }
}
