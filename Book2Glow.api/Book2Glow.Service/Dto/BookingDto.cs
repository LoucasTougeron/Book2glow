using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book2Glow.Service.Dto
{
    public class BookingDto
    {
        public DateOnly StartDate { get; set; }
        public string StartTime { get; set; }
        public string Service { get; set; }
        public string Business { get; set; }

        public Guid ServiceId {  get; set; }

        public Guid BusinessId { get; set; }
    }
}
