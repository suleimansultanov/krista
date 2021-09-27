using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace KristaShop.DataReadOnly.Models
{
    [Table("nom_filter_size_lines")]
    public class NomFilterSizeLine
    {
        public Guid size_line_id { get; set; }
        [ForeignKey("size_line_id")]
        public virtual SizeLine SizeLine { get; set; }
        public Guid option_id { get; set; }
        [ForeignKey("option_id")]
        public virtual Option Option { get; set; }
    }
}
