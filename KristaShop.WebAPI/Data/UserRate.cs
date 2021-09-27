using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace KristaShop.WebAPI.Data
{
    [Table("user_manager_rate")]
    public class UserRate
    {
        public Guid user_id { get; set; }
        [ForeignKey("user_id")]
        public User User { get; set; }
        public double rate { get; set; }
    }
}
