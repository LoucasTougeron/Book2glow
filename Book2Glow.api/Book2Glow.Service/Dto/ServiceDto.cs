using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book2Glow.Service.Dto
{
    public class ServiceDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public float Amount { get; set; }
        public int Duration { get; set; }
        public string BusinessName { get; set; }
        public string City { get; set; }
    }
}
