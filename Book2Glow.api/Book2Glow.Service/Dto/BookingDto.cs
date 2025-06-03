using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book2Glow.Service.Dto
{
    public class BookingDto
    {
        public DateOnly Date { get; set; }
        public string Heure { get; set; }
        public string Service { get; set; }
        public string Business { get; set; }
    }
}
