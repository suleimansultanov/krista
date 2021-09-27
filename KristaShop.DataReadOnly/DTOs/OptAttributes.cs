using System;

namespace KristaShop.DataReadOnly.DTOs
{
    public class OptAttributes
    {
        public OptAttributes(Guid optId, bool visibility = false, double price = 0)
        {
            ProductId = optId;
            NotVisible = visibility;
            Price = price;
        }
        public Guid ProductId { get; set; }
        public bool NotVisible { get; set; }
        public double Price { get; set; }
    }
}
