using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace KristaShop.DataReadOnly.Models
{
    [Table("nom_options")]
    public class Option
    {
        [Key]
        public Guid id { get; set; }

        public virtual ICollection<NomFilterColor> NomFilterColors { get; set; }
        public virtual ICollection<NomFilterSize> NomFilterSizes { get; set; }
        public virtual ICollection<NomFilterSizeLine> NomFilterSizeLines { get; set; }
    }
}
