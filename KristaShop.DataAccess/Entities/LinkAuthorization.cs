using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KristaShop.DataAccess.Entities
{
    [Table("authorization_link")]
    public class AuthorizationLink
    {
        [Key]
        public Guid id { get; set; }
        [Required, MaxLength(64)]
        public string random_code { get; set; }
        public Guid user_id { get; set; }
        public DateTime record_time_stamp { get; set; }
        public DateTime? valid_to { get; set; }
        public DateTime? login_date { get; set; }
    }
}
