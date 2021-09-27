using System;
using System.Collections.Generic;
using System.Text;

namespace KristaShop.Business.DTOs
{
    public class DiscountDTO
    {
        public Guid Id { get; set; }
        public Guid DiscountId { get; set; }
        public int DiscountType { get; set; }
        public double DiscountPrice { get; set; }
        public bool IsActive { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
