using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KristaShop.DataAccess.Entities
{
    [Table("menu_contents")]
    public class MenuContent
    {
        [Key]
        public Guid id { get; set; }
        
        [Required, MaxLength(256)]
        public string url { get; set; }
        [Required, MaxLength(64)]
        public string title { get; set; }
        [Required]
        public string body { get; set; }
        [Required, MaxLength(64)]
        public string layout { get; set; }
        [MaxLength(500)]
        public string meta_title { get; set; }
        [MaxLength(500)]
        public string meta_description { get; set; }
        [MaxLength(500)]
        public string meta_keywords { get; set; }
    }
}
