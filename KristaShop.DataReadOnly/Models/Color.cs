using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KristaShop.DataReadOnly.Models
{
    [Table("dict_colors")]
    public class Color
    {
        [Key]
        public Guid id { get; set; }
        public string color_name { get; set; }
        public string color_code { get; set; }
        public string color_image { get; set; }

        public virtual ICollection<NomFilterColor> NomFilterColors { get; set; }
    }
}
