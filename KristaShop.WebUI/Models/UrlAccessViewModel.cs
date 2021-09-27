using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KristaShop.WebUI.Models
{
    public class UrlAccessViewModel
    {
        public Guid Id { get; set; }
        [Display(Name = "Алиас")]
        [Required(ErrorMessage = "Заполните поле {0}")]
        public string URL { get; set; }
        [Display(Name = "Описание")]
        public string Description { get; set; }
        [Display(Name = "Уровень доступа")]
        [Required(ErrorMessage = "Заполните поле {0}")]
        public int Acl { get; set; }
        [Display(Name = "Доступ на группы")]
        public List<Guid> AccessGroupsJson { get; set; }
        [Display(Name = "Отказ на группы")]
        public List<Guid> DeniedGroupsJson { get; set; }
    }
}
