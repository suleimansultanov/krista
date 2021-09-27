using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KristaShop.WebAPI.Data
{
    [Table("dict_counterparties")]
    public class Counterparty
    {
        [Key]
        public Guid id { get; set; }
        public string title { get; set; }
        public int type { get; set; } = 0;
        public string person { get; set; }
        public string person_phone { get; set; }
        public string person_email { get; set; }
        public string mall_address { get; set; }
        public Guid city_id { get; set; }
        public Guid manager_id { get; set; }
    }
}
