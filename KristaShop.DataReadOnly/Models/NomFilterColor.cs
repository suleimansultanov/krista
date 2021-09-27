using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace KristaShop.DataReadOnly.Models
{
    [Table("nom_filter_color")]
    public class NomFilterColor
    {
        public Guid color_id { get; set; }
        [ForeignKey("color_id")]
        public virtual Color Color { get; set; }
        public Guid option_id { get; set; }
        [ForeignKey("option_id")]
        public virtual Option Option { get; set; }
    }
}
