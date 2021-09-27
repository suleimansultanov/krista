using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KristaShop.WebAPI.Data
{
    [Table("client_counter")]
    public class ClientCounter
    {
        [Key]
        public Guid id { get; set; }
        public long counter { get; set; }
        public DateTime update_time_stamp { get; set; }
    }
}
