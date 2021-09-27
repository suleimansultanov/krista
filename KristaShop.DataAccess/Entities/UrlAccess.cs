using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KristaShop.DataAccess.Entities
{
    [Table("url_access")]
    public class UrlAccess
    {
        [Key]
        public Guid id { get; set; }
        public string url { get; set; }
        public string description { get; set; }
        public int acl { get; set; }
        public string access_groups_json { get; set; }
        public string denied_groups_json { get; set; }
    }
}
