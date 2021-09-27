using System;
using System.ComponentModel.DataAnnotations;

namespace KristaShop.WebUI.Models
{
    public class DiscountViewModel
    {
        public Guid Id { get; set; }
        public Guid DiscountId { get; set; }
        public int DiscountType { get; set; }
        [Display(Name = "Скидка в $")]
        [Required(ErrorMessage = "Заполните поле {0}")]
        [Range(0.1, double.MaxValue, ErrorMessage = "{0} должно быть больше 0$")]
        public double DiscountPrice { get; set; }
        [Display(Name = "Активность")]
        public bool IsActive { get; set; }
        [Display(Name = "Действует от")]
        public DateTime? StartDate { get; set; }
        [Display(Name = "Действует до")]
        public DateTime? EndDate { get; set; }
    }
}
