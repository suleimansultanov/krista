using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KristaShop.DataReadOnly.Models
{
    [Table("dict_size_lines")]
    public class SizeLine
    {
        [Key]
        public Guid id { get; set; }
        public string size_line_name { get; set; }
        public int size_line_pos { get; set; }

        public virtual ICollection<NomFilterSizeLine> NomFilterSizeLines { get; set; }
    }
}
