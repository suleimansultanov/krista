using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KristaShop.DataAccess.Entities
{
    [Table("menu_items")]
    public class MenuItem
    {
        [Key]
        public Guid id { get; set; }
        public int menu_type { get; set; }
        [Required, MaxLength(64)]
        public string title { get; set; }
        [Required, MaxLength(64)]
        public string controller_name { get; set; }
        [Required, MaxLength(64)]
        public string action_name { get; set; }
        [MaxLength(256)]
        public string url { get; set; }
        public string parameters { get; set; }
        public string icon { get; set; }
        public int order { get; set; }
    }
}
