using System;
using System.Collections.Generic;
using System.Text;

namespace KristaShop.Business.DTOs
{
    public class CategoryDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsVisible { get; set; }
        public int NomCount { get; set; }
    }
}
