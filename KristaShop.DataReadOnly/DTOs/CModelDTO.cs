using System;
using System.Collections.Generic;
using System.Text;

namespace KristaShop.DataReadOnly.DTOs
{
    public class CModelDTO
    {
        public Guid Id { get; set; }
        public int Order { get; set; }
        public string PhotoPath { get; set; }
        public string Articul { get; set; }
        public string ItemName { get; set; }
        public List<string> Colors { get; set; }
        public List<string> Sizes { get; set; }
        public List<string> Catalogs { get; set; }
        public List<string> Categories { get; set; }
        public decimal ItemPrice { get; set; }
        public bool IsVisible { get; set; }
    }
    
    public class CModelGridDTO
    {
        public Guid ProductId { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }
        public bool NotVisible { get; set; }
    }
}
