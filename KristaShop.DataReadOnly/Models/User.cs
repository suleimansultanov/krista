using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KristaShop.DataReadOnly.Models
{
    [Table("users")]
    public class User
    {
        [Key]
        public Guid id { get; set; }
        public string login { get; set; }
        public string password { get; set; }
        public int status { get; set; }
        public DateTime? ban_expire_date { get; set; }
        public string ban_reason { get; set; }
        public bool is_root { get; set; }
        public Guid? counterparty_id { get; set; }
        [ForeignKey("counterparty_id")]
        public Counterparty Counterparty { get; set; }
    }
}
