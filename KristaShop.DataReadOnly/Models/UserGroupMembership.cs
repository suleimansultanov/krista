using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace KristaShop.DataReadOnly.Models
{
    [Table("user_group_membership")]
    public class UserGroupMembership
    {
        public Guid user_id { get; set; }
        [ForeignKey("user_id")]
        public User User { get; set; }

        public Guid group_id { get; set; }
        [ForeignKey("group_id")]
        public UserGroup UserGroup { get; set; }
    }
}
