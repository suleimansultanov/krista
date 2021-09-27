using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KristaShop.DataReadOnly.Models
{
    [Table("dict_cities")]
    public class City
    {
        [Key]
        public Guid id { get; set; }
        public string name { get; set; }
    }
}
