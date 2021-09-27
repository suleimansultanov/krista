using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KristaShop.DataAccess.Entities
{
    [Table("visible_nom_users")]
    public class VisibleNomUser
    {
        [Key]
        public Guid id { get; set; }
        public Guid nom_id { get; set; }
        [ForeignKey("nom_id")]
        public Nomenclature Nomenclature { get; set; }

        public Guid user_id { get; set; }
    }
}
