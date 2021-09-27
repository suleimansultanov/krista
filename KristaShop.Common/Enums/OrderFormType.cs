using System.ComponentModel.DataAnnotations;

namespace KristaShop.Common.Enums
{
    public enum OrderFormType
    {
        [Display(Name = "В наличие")]
        InStock = 1,
        [Display(Name = "Предзаказ")]
        Preorder = 2
    }
}
