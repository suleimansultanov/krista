using System.ComponentModel.DataAnnotations;

namespace KristaShop.Common.Enums
{
    public enum UserStatus
    {
        [Display(Name = "Гость")]
        None = -1,
        [Display(Name = "В ожидании")]
        Await = 0,
        [Display(Name = "Активный")]
        Active = 1,
        [Display(Name = "Забанненый")]
        Banned = 2,
        [Display(Name = "Удаленный")]
        Deleted = 3
    }
}
