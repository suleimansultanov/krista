using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KristaShop.DataReadOnly.Models
{
    [Table("group_acl")]
    public class AccessControl
    {
        [Key]
        public Guid id { get; set; }
        public string acl_name { get; set; }
        public int acl { get; set; }
    }
}
