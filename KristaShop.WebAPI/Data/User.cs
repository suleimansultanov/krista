using KristaShop.Common.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KristaShop.WebAPI.Data
{
    [Table("users")]
    public class User
    {
        [Key]
        public Guid id { get; set; }
        public int acl { get; set; }
        [Required]
        public string login { get; set; }
        [Required]
        public string password { get; set; }
        public int status { get; set; } = (int)UserStatus.Await;
        public bool is_root { get; set; } = false;
        public string ban_reason { get; set; } = string.Empty;
        public DateTime registration_date { get; set; }
        public Guid? counterparty_id { get; set; }
    }
}
