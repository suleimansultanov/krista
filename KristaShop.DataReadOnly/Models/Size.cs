using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KristaShop.DataReadOnly.Models
{
    [Table("dict_sizes")]
    public class Size
    {
        [Key]
        public Guid id { get; set; }
        public string size_name { get; set; }
        public int size_value { get; set; }

        public virtual ICollection<NomFilterSize> NomFilterSizes { get; set; }
    }
}
