using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KristaShop.DataReadOnly.Models
{
    [Table("user_groups")]
    public class UserGroup
    {
        [Key]
        public Guid id { get; set; }
        public string ugroup_name { get; set; }
        public Guid acl_id { get; set; }
        [ForeignKey("acl_id")]
        public AccessControl AccessControl { get; set; }
        public int type { get; set; }
    }
}
