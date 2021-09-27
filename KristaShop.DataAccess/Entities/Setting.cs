using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KristaShop.DataAccess.Entities
{
    [Table("dict_settings")]
    public class Setting
    {
        [Key]
        public Guid id { get; set; }
        [Required, MaxLength(64)]
        public string key { get; set; }
        [Required, MaxLength(1000)]
        public string value { get; set; }
    }
}
