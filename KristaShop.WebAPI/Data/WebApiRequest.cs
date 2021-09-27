using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KristaShop.WebAPI.Data
{
    [Table("web_api_requests")]
    public class WebApiRequest
    {
        [Key]
        public Guid id { get; set; }
        [Required, MaxLength(64)]
        public string request_hash { get; set; }
        public DateTime record_time_stamp { get; set; } = DateTime.Now;
    }
}
