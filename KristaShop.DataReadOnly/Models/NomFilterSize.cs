using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace KristaShop.DataReadOnly.Models
{
    [Table("nom_filter_size")]
    public class NomFilterSize
    {
        public Guid size_id { get; set; }
        [ForeignKey("size_id")]
        public virtual Size Size { get; set; }
        public Guid option_id { get; set; }
        [ForeignKey("option_id")]
        public virtual Option Option { get; set; }
    }
}
