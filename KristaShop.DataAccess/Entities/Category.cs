using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KristaShop.DataAccess.Entities
{
    [Table("dict_category")]
    public class Category
    {
        [Key]
        public Guid id { get; set; }
        public string name { get; set; }
        public bool is_visible { get; set; }

        public virtual ICollection<NomCategory> NomCategories { get; set; }
    }
}
