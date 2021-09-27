using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KristaShop.DataAccess.Entities
{
    [Table("feedbacks")]
    public class Feedback
    {
        [Key]
        public Guid id { get; set; }
        [Required, MaxLength(64)]
        public string person { get; set; }
        [Required, MaxLength(64)]
        public string phone { get; set; }
        public string message { get; set; }
        [MaxLength(64)]
        public string email { get; set; }
        public bool viewed { get; set; }
        public DateTime record_time_stamp { get; set; }
        public Guid user_id { get; set; }
        public DateTime? view_time_stamp { get; set; }
    }
}
