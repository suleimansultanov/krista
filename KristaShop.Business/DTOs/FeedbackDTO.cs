using System;
using System.Collections.Generic;
using System.Text;

namespace KristaShop.Business.DTOs
{
    public class FeedbackDTO
    {
        public Guid Id { get; set; }
        public string Person { get; set; }
        public string Phone { get; set; }
        public string Message { get; set; }
        public string Email { get; set; }
        public bool Viewed { get; set; }
        public DateTime RecordTimeStamp { get; set; }
    }
}
